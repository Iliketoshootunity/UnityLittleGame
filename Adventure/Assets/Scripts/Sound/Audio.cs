using UnityEngine;

namespace LFrameWork.Sound
{
    /// <summary>
    /// audio
    /// </summary>
    public class Audio
    {

        public int AudioID { get; private set; }

        public AudioType Type { get; private set; }

        public bool IsPlaying { get; private set; }

        public bool Paused { get; private set; }

        public bool Stopping { get; private set; }

        public bool Activated { get; private set; }


        public bool Pooled { get; set; }


        public float Volume { get; private set; }


        public AudioSource AudioSource { get; private set; }


        public Transform SourceTransform
        {
            get { return sourceTransform; }
            set
            {
                if(value == null)
                {
                    sourceTransform = EazySoundManager.Gameobject.transform;
                }
                else
                {
                    sourceTransform = value;
                }
            }
        }


        public AudioClip Clip
        {
            get { return clip; }
            set
            {
                clip = value;
                if (AudioSource != null)
                {
                    AudioSource.clip = clip;
                }
            }
        }


        public bool Loop
        {
            get { return loop; }
            set
            {
                loop = value;
                if(AudioSource != null)
                {
                    AudioSource.loop = loop;
                }
            }
        }


        public bool Mute
        {
            get { return mute; }
            set
            {
                mute = value;
                if (AudioSource != null)
                {
                    AudioSource.mute = mute;
                }
            }
        }


        public int Priority
        {
            get { return priority; }
            set
            {
                priority = Mathf.Clamp(value, 0, 256);
                if (AudioSource != null)
                {
                    AudioSource.priority = priority;
                }
            }
        }


        public float Pitch
        {
            get { return pitch; }
            set
            {
                pitch = Mathf.Clamp(value, -3, 3);
                if (AudioSource != null)
                {
                    AudioSource.pitch = pitch;
                }
            }
        }


        public float StereoPan
        {
            get { return stereoPan; }
            set
            {
                stereoPan = Mathf.Clamp(value, -1, 1);
                if (AudioSource != null)
                {
                    AudioSource.panStereo = stereoPan;
                }
            }
        }


        public float SpatialBlend
        {
            get { return spatialBlend; }
            set
            {
                spatialBlend = Mathf.Clamp01(value);
                if (AudioSource != null)
                {
                    AudioSource.spatialBlend = spatialBlend;
                }
            }
        }


        public float ReverbZoneMix
        {
            get { return reverbZoneMix; }
            set
            {
                reverbZoneMix = Mathf.Clamp(value, 0, 1.1f);
                if (AudioSource != null)
                {
                    AudioSource.reverbZoneMix = reverbZoneMix;
                }
            }
        }


        public float DopplerLevel
        {
            get { return dopplerLevel; }
            set
            {
                dopplerLevel = Mathf.Clamp(value, 0, 5);
                if (AudioSource != null)
                {
                    AudioSource.dopplerLevel = dopplerLevel;
                }
            }
        }

        public float Spread
        {
            get { return spread; }
            set
            {
                spread = Mathf.Clamp(value, 0, 360);
                if (AudioSource != null)
                {
                    AudioSource.spread = spread;
                }
            }
        }

        /// <summary>
        /// How the audio attenuates over distance
        /// </summary>
        public AudioRolloffMode RolloffMode
        {
            get { return rolloffMode; }
            set
            {
                rolloffMode = value;
                if (AudioSource != null)
                {
                    AudioSource.rolloffMode = rolloffMode;
                }
            }
        }

        public float Max3DDistance
        {
            get { return max3DDistance; }
            set
            {
                max3DDistance = Mathf.Max(value, 0.01f);
                if(AudioSource != null)
                {
                    AudioSource.maxDistance = max3DDistance;
                }
            }
        }


        public float Min3DDistance
        {
            get { return min3DDistance; }
            set
            {
                min3DDistance = Mathf.Max(value, 0);
                if (AudioSource != null)
                {
                    AudioSource.minDistance = min3DDistance;
                }
            }
        }


        public bool Persist { get; set; }


        public float FadeInSeconds { get; set; }

        /// <summary>
        /// How many seconds it needs for the audio to fade out/ reach target volume (if lower than current)
        /// </summary>
        public float FadeOutSeconds { get; set; }

        /// <summary>
        /// Enum representing the type of audio
        /// </summary>
        public enum AudioType
        {
            Music,
            Sound,
            UISound
        }

        private static int audioCounter = 0;

        private AudioClip clip;
        private bool loop;
        private bool mute;
        private int priority;
        private float pitch;
        private float stereoPan;
        private float spatialBlend;
        private float reverbZoneMix;
        private float dopplerLevel;
        private float spread;
        private AudioRolloffMode rolloffMode;
        private float max3DDistance;
        private float min3DDistance;
        
        private float targetVolume;
        private float initTargetVolume;
        private float tempFadeSeconds;
        private float fadeInterpolater;
        private float onFadeStartVolume;
        private Transform sourceTransform;

        public Audio(AudioType audioType, AudioClip clip, bool loop, bool persist, float volume, float fadeInValue, float fadeOutValue, Transform sourceTransform)
        {
            // Set unique audio ID
            AudioID = audioCounter;
            audioCounter++;

            // Initialize values
            this.Type = audioType;
            this.Clip = clip;
            this.SourceTransform = sourceTransform;
            this.Loop = loop;
            this.Persist = persist;
            this.targetVolume = volume;
            this.initTargetVolume = volume;
            this.tempFadeSeconds = -1;
            this.FadeInSeconds = fadeInValue;
            this.FadeOutSeconds = fadeOutValue;

            Volume = 0f;
            Pooled = false;

            // Set audiosource default values
            Mute = false;
            Priority = 128;
            Pitch = 1;
            StereoPan = 0;
            if (sourceTransform != null && sourceTransform != EazySoundManager.Gameobject.transform)
            {
                SpatialBlend = 1;
            }
            ReverbZoneMix = 1;
            DopplerLevel = 1;
            Spread = 0;
            RolloffMode = AudioRolloffMode.Logarithmic;
            Min3DDistance = 1;
            Max3DDistance = 500;

            // Initliaze states
            IsPlaying = false;
            Paused = false;
            Activated = false;
        }

        /// <summary>
        /// Creates and initializes the audiosource component with the appropriate values
        /// </summary>
        private void CreateAudiosource()
        {
            if(sourceTransform)
            {
                sourceTransform = EazySoundManager.Gameobject.transform;
            }

            AudioSource = sourceTransform.gameObject.AddComponent<AudioSource>() as AudioSource;
            AudioSource.clip = Clip;
            AudioSource.loop = Loop;
            AudioSource.mute = Mute;
            AudioSource.volume = Volume;
            AudioSource.priority = Priority;
            AudioSource.pitch = Pitch;
            AudioSource.panStereo = StereoPan;
            AudioSource.spatialBlend = SpatialBlend;
            AudioSource.reverbZoneMix = ReverbZoneMix;
            AudioSource.dopplerLevel = DopplerLevel;
            AudioSource.spread = Spread;
            AudioSource.rolloffMode = RolloffMode;
            AudioSource.maxDistance = Max3DDistance;
            AudioSource.minDistance = Min3DDistance;
        }

        /// <summary>
        /// Start playing audio clip from the beginning
        /// </summary>
        public void Play()
        {
            Play(initTargetVolume);
        }

        /// <summary>
        /// Start playing audio clip from the beggining
        /// </summary>
        /// <param name="volume">The target volume</param>
        public void Play(float volume)
        {
            // Check if audio still exists in sound manager
            if (Pooled)
            {
                // If not, restore it from the audioPool
                bool restoredFromPool = EazySoundManager.RestoreAudioFromPool(Type, AudioID);
                if (!restoredFromPool)
                {
                    return;
                }

                Pooled = true;
            }

            // Recreate audiosource if it does not exist
            if (AudioSource == null)
            {
                CreateAudiosource();
            }

            AudioSource.Play();
            IsPlaying = true;

            fadeInterpolater = 0f;
            onFadeStartVolume = this.Volume;
            targetVolume = volume;
        }

        /// <summary>
        ///停止
        /// </summary>
        public void Stop()
        {
            fadeInterpolater = 0f;
            onFadeStartVolume = Volume;
            targetVolume = 0f;

            Stopping = true;
        }

        /// <summary>
        ///暂定
        /// </summary>
        public void Pause()
        {
            AudioSource.Pause();
            Paused = true;
        }

        /// <summary>
        /// 不暂停
        /// </summary>
        public void UnPause()
        {
            AudioSource.UnPause();
            Paused = false;
        }

        /// <summary>
        /// 恢复
        /// </summary>
        public void Resume()
        {
            AudioSource.UnPause();
            Paused = false;
        }

        /// <summary>
        /// 设置音量
        /// </summary>

        public void SetVolume(float volume)
        {
            if (volume > targetVolume)
            {
                SetVolume(volume, FadeOutSeconds);
            }
            else
            {
                SetVolume(volume, FadeInSeconds);
            }
        }

        /// <summary>
        /// 设置音量
        /// </summary>
        public void SetVolume(float volume, float fadeSeconds)
        {
            SetVolume(volume, fadeSeconds, this.Volume);
        }

        /// <summary>
        /// 设置音量
        /// </summary>
        public void SetVolume(float volume, float fadeSeconds, float startVolume)
        {
            targetVolume = Mathf.Clamp01(volume);
            fadeInterpolater = 0;
            onFadeStartVolume = startVolume;
            tempFadeSeconds = fadeSeconds;
        }

        /// <summary>
        /// 设置距离
        /// </summary>
        public void Set3DDistances(float min, float max)
        {
            Min3DDistance = min;
            Max3DDistance = max;
        }


        public void Update()
        {
            if (AudioSource == null)
            {
                return;
            }

            Activated = true;

            // Increase/decrease volume to reach the current target
            if (Volume != targetVolume)
            {
                float fadeValue;
                fadeInterpolater += Time.unscaledDeltaTime;
                if (Volume > targetVolume)
                {
                    fadeValue = tempFadeSeconds != -1 ? tempFadeSeconds : FadeOutSeconds;
                }
                else
                {
                    fadeValue = tempFadeSeconds != -1 ? tempFadeSeconds : FadeInSeconds;
                }

                Volume = Mathf.Lerp(onFadeStartVolume, targetVolume, fadeInterpolater / fadeValue);
            }
            else if (tempFadeSeconds != -1)
            {
                tempFadeSeconds = -1;
            }
            
            // Set the volume, taking into account the global volumes as well.
            switch (Type)
            {
                case AudioType.Music:
                    {
                        AudioSource.volume = Volume * EazySoundManager.GlobalMusicVolume * EazySoundManager.GlobalVolume;
                        break;
                    }
                case AudioType.Sound:
                    {
                        AudioSource.volume = Volume * EazySoundManager.GlobalSoundsVolume * EazySoundManager.GlobalVolume;
                        break;
                    }
                case AudioType.UISound:
                    {
                        AudioSource.volume = Volume * EazySoundManager.GlobalUISoundsVolume * EazySoundManager.GlobalVolume;
                        break;
                    }
            }

            // Completely stop audio if it finished the process of stopping
            if (Volume == 0f && Stopping)
            {
                AudioSource.Stop();
                Stopping = false;
                IsPlaying = false;
                Paused = false;
            }

            // Update playing status
            if (AudioSource.isPlaying != IsPlaying)
            {
                IsPlaying = AudioSource.isPlaying;
            }
        }
    }
}