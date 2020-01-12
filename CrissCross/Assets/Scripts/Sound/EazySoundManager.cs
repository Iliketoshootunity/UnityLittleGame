using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

namespace LFrameWork.Sound
{
    /// <summary>
    /// ���𲥷ź͹�����Ƶ�������ľ�̬��
    /// </summary>
    public class EazySoundManager : MonoBehaviour
    {
        /// <summary>
        /// �������������ӵ�����Ϸ����
        /// </summary>
        public static GameObject Gameobject { get { return Instance.gameObject; } }

        /// <summary>
        /// ������Ϊ��ʱ�����κ�����������Ƶ������ͬ��Ƶ��������������Ƶ�������ԡ�
        /// </summary>
        public static bool IgnoreDuplicateMusic { get; set; }

        /// <summary>
        /// ������Ϊ��ʱ�����κ�����������Ƶ������ͬ��Ƶ��������������Ƶ��������
        /// </summary>
        public static bool IgnoreDuplicateSounds { get; set; }

        /// <summary>
        /// ������Ϊtrueʱ�����κ�����UI������Ƶ������ͬ��Ƶ��������UI������Ƶ��������
        /// </summary>
        public static bool IgnoreDuplicateUISounds { get; set; }

        /// <summary>
        /// ȫ������
        /// </summary>
        public static float GlobalVolume { get; set; }

        /// <summary>
        /// ȫ����������
        /// </summary>
        public static float GlobalMusicVolume { get; set; }

        /// <summary>
        /// ȫ����������
        /// </summary>
        public static float GlobalSoundsVolume { get; set; }

        /// <summary>
        /// ȫ��UI��������
        /// </summary>
        public static float GlobalUISoundsVolume { get; set; }

        private static EazySoundManager instance = null;

        private static Dictionary<int, Audio> musicAudio;
        private static Dictionary<int, Audio> soundsAudio;
        private static Dictionary<int, Audio> UISoundsAudio;
        private static Dictionary<int, Audio> audioPool;

        private static bool initialized = false;

        private static EazySoundManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (EazySoundManager)FindObjectOfType(typeof(EazySoundManager));
                    if (instance == null)
                    {
                        // Create gameObject and add component
                        instance = (new GameObject("EazySoundManager")).AddComponent<EazySoundManager>();
                    }
                }
                return instance;
            }
        }

        static EazySoundManager()
        {
            Instance.Init();
        }

        /// <summary>
        /// ��ʼ�����ֹ�����
        /// </summary>
        private void Init()
        {
            if (!initialized)
            {
                musicAudio = new Dictionary<int, Audio>();
                soundsAudio = new Dictionary<int, Audio>();
                UISoundsAudio = new Dictionary<int, Audio>();
                audioPool = new Dictionary<int, Audio>();

                GlobalVolume = 1;
                GlobalMusicVolume = 1;
                GlobalSoundsVolume = 1;
                GlobalUISoundsVolume = 1;

                IgnoreDuplicateMusic = false;
                IgnoreDuplicateSounds = false;
                IgnoreDuplicateUISounds = false;

                initialized = true;
                DontDestroyOnLoad(this);
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        /// <summary>
        /// �������ں����
        /// </summary>
        /// <param name="scene">The scene that is loaded</param>
        /// <param name="mode">The scene load mode</param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Stop and remove all non-persistent audio
            RemoveNonPersistAudio(musicAudio);
            RemoveNonPersistAudio(soundsAudio);
            RemoveNonPersistAudio(UISoundsAudio);
        }

        private void Update()
        {
            UpdateAllAudio(musicAudio);
            UpdateAllAudio(soundsAudio);
            UpdateAllAudio(UISoundsAudio);
        }

        /// <summary>
        /// ����������Ͷ�Ӧ���ֵ�
        /// </summary>
        /// <param name="audioType">The audio type of the dictionary to return</param>
        /// <returns>An audio dictionary</returns>
        private static Dictionary<int, Audio> GetAudioTypeDictionary(Audio.AudioType audioType)
        {
            Dictionary<int, Audio> audioDict = new Dictionary<int, Audio>();
            switch (audioType)
            {
                case Audio.AudioType.Music:
                    audioDict = musicAudio;
                    break;
                case Audio.AudioType.Sound:
                    audioDict = soundsAudio;
                    break;
                case Audio.AudioType.UISound:
                    audioDict = UISoundsAudio;
                    break;
            }

            return audioDict;
        }

        /// <summary>
        /// Retrieves the IgnoreDuplicates setting of audios of a specified audio type
        /// </summary>
        /// <param name="audioType">The audio type that the returned IgnoreDuplicates setting affects</param>
        /// <returns>An IgnoreDuplicates setting (bool)</returns>
        private static bool GetAudioTypeIgnoreDuplicateSetting(Audio.AudioType audioType)
        {
            switch (audioType)
            {
                case Audio.AudioType.Music:
                    return IgnoreDuplicateMusic;
                case Audio.AudioType.Sound:
                    return IgnoreDuplicateSounds;
                case Audio.AudioType.UISound:
                    return IgnoreDuplicateUISounds;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Updates the state of all audios of an audio dictionary
        /// </summary>
        /// <param name="audioDict">The audio dictionary to update</param>
        private static void UpdateAllAudio(Dictionary<int, Audio> audioDict)
        {
            // Go through all audios and update them
            List<int> keys = new List<int>(audioDict.Keys);
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                audio.Update();

                // Remove it if it is no longer active (playing)
                if (!audio.IsPlaying && !audio.Paused)
                {
                    Destroy(audio.AudioSource);

                    // Add it to the audio pool in case it needs to be referenced in the future
                    audioPool.Add(key, audio);
                    audio.Pooled = true;
                    audioDict.Remove(key);
                }
            }
        }

        /// <summary>
        /// ɾ�����ڳ���ת���б���������
        /// </summary>
        /// <param name="audioDict"></param>
        private static void RemoveNonPersistAudio(Dictionary<int, Audio> audioDict)
        {
            List<int> keys = new List<int>(audioDict.Keys);
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                if (!audio.Persist && audio.Activated)
                {
                    Destroy(audio.AudioSource);
                    audioDict.Remove(key);
                }
            }

            keys = new List<int>(audioPool.Keys);
            foreach (int key in keys)
            {
                Audio audio = audioPool[key];
                if (!audio.Persist && audio.Activated)
                {
                    audioPool.Remove(key);
                }
            }
        }

        /// <summary>
        /// �ӳ�����ȡ���뵽�����б���
        /// </summary>
        /// <param name="audioType"></param>
        /// <param name="audioID"></param>
        /// <returns>.</returns>
        public static bool RestoreAudioFromPool(Audio.AudioType audioType, int audioID)
        {
            if (audioPool.ContainsKey(audioID))
            {
                Dictionary<int, Audio> audioDict = GetAudioTypeDictionary(audioType);
                audioDict.Add(audioID, audioPool[audioID]);
                audioPool.Remove(audioID);

                return true;
            }

            return false;
        }

        #region GetAudio Functions

        /// <summary>
        /// ��ȡAudio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetAudio(int audioID)
        {
            Audio audio;

            audio = GetMusicAudio(audioID);
            if (audio != null)
            {
                return audio;
            }

            audio = GetSoundAudio(audioID);
            if (audio != null)
            {
                return audio;
            }

            audio = GetUISoundAudio(audioID);
            if (audio != null)
            {
                return audio;
            }

            return null;
        }
        /// <summary>
        /// ��ȡAudio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetAudio(AudioClip audioClip)
        {
            Audio audio = GetMusicAudio(audioClip);
            if (audio != null)
            {
                return audio;
            }

            audio = GetSoundAudio(audioClip);
            if (audio != null)
            {
                return audio;
            }

            audio = GetUISoundAudio(audioClip);
            if (audio != null)
            {
                return audio;
            }

            return null;
        }

        /// <summary>
        /// ��ȡ����Audio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetMusicAudio(int audioID)
        {
            return GetAudio(Audio.AudioType.Music, true, audioID);
        }

        /// <summary>
        /// ��ȡ����Audio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetMusicAudio(AudioClip audioClip)
        {
            return GetAudio(Audio.AudioType.Music, true, audioClip);
        }

        /// <summary>
        /// ��ȡ��ЧAudio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetSoundAudio(int audioID)
        {
            return GetAudio(Audio.AudioType.Sound, true, audioID);
        }

        /// <summary>
        /// ��ȡ��ЧAudio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetSoundAudio(AudioClip audioClip)
        {
            return GetAudio(Audio.AudioType.Sound, true, audioClip);
        }

        /// <summary>
        /// ��ȡUI��ЧAudio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetUISoundAudio(int audioID)
        {
            return GetAudio(Audio.AudioType.UISound, true, audioID);
        }

        /// <summary>
        /// ��ȡUI��ЧAudio
        /// </summary>
        /// <param name="audioID"></param>
        /// <returns></returns>
        public static Audio GetUISoundAudio(AudioClip audioClip)
        {
            return GetAudio(Audio.AudioType.UISound, true, audioClip);
        }

        private static Audio GetAudio(Audio.AudioType audioType, bool usePool, int audioID)
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary(audioType);

            if (audioDict.ContainsKey(audioID))
            {
                return audioDict[audioID];
            }

            if (usePool && audioPool.ContainsKey(audioID) && audioPool[audioID].Type == audioType)
            {
                return audioPool[audioID];
            }

            return null;
        }

        private static Audio GetAudio(Audio.AudioType audioType, bool usePool, AudioClip audioClip)
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary(audioType);

            List<int> audioTypeKeys = new List<int>(audioDict.Keys);
            List<int> poolKeys = new List<int>(audioPool.Keys);
            List<int> keys = usePool ? audioTypeKeys.Concat(poolKeys).ToList() : audioTypeKeys;
            foreach (int key in keys)
            {
                if (audioDict.ContainsKey(key))
                {
                    Audio audio = audioDict[key];
                    if (audio.Clip == audioClip && audio.Type == audioType)
                    {
                        return audio;
                    }
                }

            }

            return null;
        }

        #endregion

        #region Prepare Function


        public static int PrepareMusic(AudioClip clip)
        {
            return PrepareAudio(Audio.AudioType.Music, clip, 1f, false, false, 1f, 1f, -1f, null);
        }


        public static int PrepareMusic(AudioClip clip, float volume)
        {
            return PrepareAudio(Audio.AudioType.Music, clip, volume, false, false, 1f, 1f, -1f, null);
        }


        public static int PrepareMusic(AudioClip clip, float volume, bool loop, bool persist)
        {
            return PrepareAudio(Audio.AudioType.Music, clip, volume, loop, persist, 1f, 1f, -1f, null);
        }


        public static int PrepareMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds)
        {
            return PrepareAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, -1f, null);
        }

        public static int PrepareMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
        {
            return PrepareAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform);
        }

        public static int PrepareSound(AudioClip clip)
        {
            return PrepareAudio(Audio.AudioType.Sound, clip, 1f, false, false, 0f, 0f, -1f, null);
        }


        public static int PrepareSound(AudioClip clip, float volume)
        {
            return PrepareAudio(Audio.AudioType.Sound, clip, volume, false, false, 0f, 0f, -1f, null);
        }


        public static int PrepareSound(AudioClip clip, bool loop)
        {
            return PrepareAudio(Audio.AudioType.Sound, clip, 1f, loop, false, 0f, 0f, -1f, null);
        }


        public static int PrepareSound(AudioClip clip, float volume, bool loop, Transform sourceTransform)
        {
            return PrepareAudio(Audio.AudioType.Sound, clip, volume, loop, false, 0f, 0f, -1f, sourceTransform);
        }

        public static int PrepareUISound(AudioClip clip)
        {
            return PrepareAudio(Audio.AudioType.UISound, clip, 1f, false, false, 0f, 0f, -1f, null);
        }

        public static int PrepareUISound(AudioClip clip, float volume)
        {
            return PrepareAudio(Audio.AudioType.UISound, clip, volume, false, false, 0f, 0f, -1f, null);
        }

        private static int PrepareAudio(Audio.AudioType audioType, AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
        {
            if (clip == null)
            {
                Debug.LogError("[Eazy Sound Manager] Audio clip is null", clip);
            }

            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary(audioType);
            bool ignoreDuplicateAudio = GetAudioTypeIgnoreDuplicateSetting(audioType);

            if (ignoreDuplicateAudio)
            {
                Audio duplicateAudio = GetAudio(audioType, true, clip);
                if (duplicateAudio != null)
                {
                    return duplicateAudio.AudioID;
                }
            }


            Audio audio = new Audio(audioType, clip, loop, persist, volume, fadeInSeconds, fadeOutSeconds, sourceTransform);


            audioDict.Add(audio.AudioID, audio);

            return audio.AudioID;
        }

        #endregion

        #region Play Functions


        public static int PlayMusic(AudioClip clip)
        {
            return PlayAudio(Audio.AudioType.Music, clip, 1f, false, false, 1f, 1f, -1f, null);
        }


        public static int PlayMusic(AudioClip clip, float volume)
        {
            return PlayAudio(Audio.AudioType.Music, clip, volume, false, false, 1f, 1f, -1f, null);
        }

        /// <summary>
        /// ���ű�������
        /// </summary>

        public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist)
        {
            return PlayAudio(Audio.AudioType.Music, clip, volume, loop, persist, 1f, 1f, -1f, null);
        }

        /// <summary>
        ///���ű�������
        /// </summary>

        public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds)
        {
            return PlayAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, -1f, null);
        }

        /// <summary>
        /// ���ű�������
        /// </summary>
        /// <param name="clip">����Ƭ��</param>
        /// <param name="volume"> ����</param>
        /// <param name="loop">�Ƿ�ѭ��</param>
        /// <param name="persist"> Whether the audio persists in between scene changes</param>
        /// <param name="fadeInSeconds">��Ƶ��Ҫ��������ܽ���/�ﵽĿ������)</param>
        /// <param name="fadeOutSeconds"> ��Ƶ��Ҫ��������ܵ���/�ﵽĿ��������������ڵ�ǰ������</param>
        /// <param name="currentMusicfadeOutSeconds"> ��ǰ������Ƶ��Ҫ��������ܵ��������������Լ��ĵ����롣���-- 1ͨ������ǰ���ֽ������Լ��ĵ����롣</param>
        /// <param name="sourceTransform">The transform that is the source of the music (will become 3D audio). If 3D audio is not wanted, use null</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayMusic(AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
        {
            return PlayAudio(Audio.AudioType.Music, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform);
        }

        /// <summary>
        /// ������Ч
        /// </summary>

        public static int PlaySound(AudioClip clip)
        {
            return PlayAudio(Audio.AudioType.Sound, clip, 1f, false, false, 0f, 0f, -1f, null);
        }

        /// <summary>
        ///  ������Ч
        /// </summary>

        public static int PlaySound(AudioClip clip, float volume)
        {
            return PlayAudio(Audio.AudioType.Sound, clip, volume, false, false, 0f, 0f, -1f, null);
        }

        /// <summary>
        /// P ������Ч
        /// </summary>

        public static int PlaySound(AudioClip clip, bool loop)
        {
            return PlayAudio(Audio.AudioType.Sound, clip, 1f, loop, false, 0f, 0f, -1f, null);
        }

        /// <summary>
        ///  ������Ч
        /// </summary>
        public static int PlaySound(AudioClip clip, float volume, bool loop, Transform sourceTransform)
        {
            return PlayAudio(Audio.AudioType.Sound, clip, volume, loop, false, 0f, 0f, -1f, sourceTransform);
        }

        /// <summary>
        ///  ����UI��Ч
        /// </summary>
        /// <param name="clip">The audio clip to play</param>
        /// <returns>The ID of the created Audio object</returns>
        public static int PlayUISound(AudioClip clip)
        {
            return PlayAudio(Audio.AudioType.UISound, clip, 1f, false, false, 0f, 0f, -1f, null);
        }

        /// <summary>
        ///  ����UI��Ч
        /// </summary>

        public static int PlayUISound(AudioClip clip, float volume)
        {
            return PlayAudio(Audio.AudioType.UISound, clip, volume, false, false, 0f, 0f, -1f, null);
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="clip">����Ƭ��</param>
        /// <param name="volume"> ����</param>
        /// <param name="loop">�Ƿ�ѭ��</param>
        /// <param name="persist"> Whether the audio persists in between scene changes</param>
        /// <param name="fadeInSeconds">��Ƶ��Ҫ��������ܽ���/�ﵽĿ������)</param>
        /// <param name="fadeOutSeconds"> ��Ƶ��Ҫ��������ܵ���/�ﵽĿ��������������ڵ�ǰ������</param>
        /// <param name="currentMusicfadeOutSeconds"> ��ǰ������Ƶ��Ҫ��������ܵ��������������Լ��ĵ����롣���-- 1ͨ������ǰ���ֽ������Լ��ĵ����롣</param>
        /// <param name="sourceTransform">The transform that is the source of the music (will become 3D audio). If 3D audio is not wanted, use null</param>
        /// <returns>The ID of the created Audio object</returns>
        private static int PlayAudio(Audio.AudioType audioType, AudioClip clip, float volume, bool loop, bool persist, float fadeInSeconds, float fadeOutSeconds, float currentMusicfadeOutSeconds, Transform sourceTransform)
        {
            int audioID = PrepareAudio(audioType, clip, volume, loop, persist, fadeInSeconds, fadeOutSeconds, currentMusicfadeOutSeconds, sourceTransform);

            // Stop all current music playing
            if (audioType == Audio.AudioType.Music)
            {
                StopAllMusic(currentMusicfadeOutSeconds);
            }

            GetAudio(audioType, false, audioID).Play();

            return audioID;
        }

        #endregion

        #region Stop Functions

        /// <summary>
        /// ֹͣ��������
        /// </summary>
        public static void StopAll()
        {
            StopAll(-1f);
        }

        /// <summary>
        /// ֹͣ�����������������ֵ���
        /// </summary>
        public static void StopAll(float musicFadeOutSeconds)
        {
            StopAllMusic(musicFadeOutSeconds);
            StopAllSounds();
            StopAllUISounds();
        }

        /// <summary>
        /// ֹͣ���б�������
        /// </summary>
        public static void StopAllMusic()
        {
            StopAllAudio(Audio.AudioType.Music, -1f);
        }

        /// <summary>
        /// ֹͣ���б�������
        /// </summary>
        public static void StopAllMusic(float fadeOutSeconds)
        {
            StopAllAudio(Audio.AudioType.Music, fadeOutSeconds);
        }


        public static void StopAllSounds()
        {
            StopAllAudio(Audio.AudioType.Sound, -1f);
        }


        public static void StopAllUISounds()
        {
            StopAllAudio(Audio.AudioType.UISound, -1f);
        }

        private static void StopAllAudio(Audio.AudioType audioType, float fadeOutSeconds)
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary(audioType);

            List<int> keys = new List<int>(audioDict.Keys);
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                if (fadeOutSeconds >= 0)
                {
                    audio.FadeOutSeconds = fadeOutSeconds;
                }
                audio.Stop();
            }
        }

        #endregion

        #region Pause Functions


        public static void PauseAll()
        {
            PauseAllMusic();
            PauseAllSounds();
            PauseAllUISounds();
        }


        public static void PauseAllMusic()
        {
            PauseAllAudio(Audio.AudioType.Music);
        }


        public static void PauseAllSounds()
        {
            PauseAllAudio(Audio.AudioType.Sound);
        }


        public static void PauseAllUISounds()
        {
            PauseAllAudio(Audio.AudioType.UISound);
        }

        private static void PauseAllAudio(Audio.AudioType audioType)
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary(audioType);

            List<int> keys = new List<int>(audioDict.Keys);
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                audio.Pause();
            }
        }

        #endregion

        #region Resume Functions


        public static void ResumeAll()
        {
            ResumeAllMusic();
            ResumeAllSounds();
            ResumeAllUISounds();
        }


        public static void ResumeAllMusic()
        {
            ResumeAllAudio(Audio.AudioType.Music);
        }


        public static void ResumeAllSounds()
        {
            ResumeAllAudio(Audio.AudioType.Sound);
        }


        public static void ResumeAllUISounds()
        {
            ResumeAllAudio(Audio.AudioType.UISound);
        }

        private static void ResumeAllAudio(Audio.AudioType audioType)
        {
            Dictionary<int, Audio> audioDict = GetAudioTypeDictionary(audioType);

            List<int> keys = new List<int>(audioDict.Keys);
            foreach (int key in keys)
            {
                Audio audio = audioDict[key];
                audio.Resume();
            }
        }

        #endregion
    }
}
