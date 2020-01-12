using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System;
using System.Collections.Generic;
using EasyFrameWork.UI;
using LFrameWork.Sound;

namespace EasyFrameWork
{
    public class GameLevelSceneCtrl : GameSceneCtrlBase
    {

        public static GameLevelSceneCtrl Instance;

        [SerializeField]
        private AudioClip m_Music;
        public Transform PlayerBornPos;
        [SerializeField]
        private List<Transform> m_ItemRootList;
        [SerializeField]
        public float m_FloorHeightIntervals;
        private RoleCtrl m_Player;
        [SerializeField]
        private int m_MaxFloor = 3;
        [SerializeField]
        private Fire m_Fire;
        [SerializeField]
        private int m_CurFloor;
        [SerializeField]
        private float m_FireMoveDelay = 4;
        private Floor m_LastFloor;
        private UIGameLevelSceneView m_SceneView;

        public int CurFloor
        {
            get
            {
                return m_CurFloor;
            }
        }

        private void Awake()
        {
            Instance = this;
        }
        // Use this for initialization
        void Start()
        {
            Global.Instance.SetMusic(Global.Instance.IsPlaySound, m_Music);

            DelegateDefine.Instance.OnGetCoin = OnGetCoin;
            m_SceneView = UISceneCtrl.Instance.Load(UISceneType.GameLevel).GetComponent<UIGameLevelSceneView>();
            m_SceneView.SetCoin(Global.Instance.UserInfo.Coin);
            m_SceneView.SetHP(3);
            UIDispatcher.Instance.AddEventListen(ConstDefine.GameLevelScene_Pause, OnClickPauseBtn);
            m_CurFloor = -3;
            OnRoleJumpToNextFloor();
            OnRoleJumpToNextFloor();
            OnRoleJumpToNextFloor();
            CreatePlayer();
        }

        private void OnClickPauseBtn(object[] p)
        {
            UIViewMgr.Instance.OpenView(UIViewType.Pause);
            Time.timeScale = 0;
        }

        private void OnGetCoin()
        {
            Global.Instance.UserInfo.Coin++;
            m_SceneView.SetCoin(Global.Instance.UserInfo.Coin); ;
        }

        private void CreatePlayer()
        {
            string path = string.Format("Player{0}/Player{0}", Global.Instance.UserInfo.CurJob.ToString());
            GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Role, "Player/" + path);
            go.transform.position = PlayerBornPos.transform.position;
            go.transform.rotation = PlayerBornPos.transform.rotation;
            go.transform.localScale = Vector3.one;
            m_Player = go.GetComponent<RoleCtrl>();
            switch (Global.Instance.UserInfo.CurJob)
            {
                default:
                    m_Player.Init(null, 3);
                    break;
            }
            m_Player.OnRoleJumpToNextFloor = OnRoleJumpToNextFloor;
            m_Player.OnRoleDie = OnRoleDead;
            m_Player.OnRoleHurt = OnRoleHurt;
            m_Player.OnRoleAddHp = OnRoleAddHp;
        }

        private void OnRoleAddHp(int obj)
        {
            m_SceneView.SetHP(obj);
        }

        private void OnRoleDead(RoleCtrl obj)
        {
            StartCoroutine("OnRoleDeadIE");
        }

        private IEnumerator OnRoleDeadIE()
        {
            m_Fire.StopMove();
            yield return new WaitForSeconds(1);
            //打开失败界面
            EazySoundManager.StopAllMusic(1);
            UIViewMgr.Instance.OpenView(UIViewType.Fail);


        }
        private void OnRoleHurt(int hp)
        {
            m_SceneView.SetHP(hp);
        }

        private void OnRoleJumpToNextFloor()
        {

            if (m_Player != null && m_Player.CurFloor != null)
            {
                Vector3 firePos = m_Player.transform.TransformPoint(Vector2.up * -4);
                if (m_Fire != null)
                {
                    m_Fire.Init(new Vector3(m_Fire.transform.position.x, firePos.y, m_Fire.transform.position.z), m_FireMoveDelay);
                }
            }
            CreateFloor();
            if (m_CurFloor == -2) return;
            if (m_CurFloor >= m_MaxFloor)
            {
                UIViewMgr.Instance.OpenView(UIViewType.Victory);
                Time.timeScale = 0;
                return;
            }
            int index = UnityEngine.Random.Range(0, m_ItemRootList.Count) + 1;
            GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Item, "ItemRoot/ItemRoot0" + index);
            go.transform.SetParent(m_LastFloor.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            int value = UnityEngine.Random.Range(0, 100);
            if (value < 90) return;
            GameObject hp = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Item, "HpItem");
            hp.transform.SetParent(m_LastFloor.transform);
            hp.transform.localPosition = new Vector3(UnityEngine.Random.Range(2f, 5f), UnityEngine.Random.Range(2.5f, 3.5f));
            hp.transform.localRotation = Quaternion.identity;
            hp.transform.localScale = Vector3.one;

        }

        private void CreateItemRoot()
        {

        }


        private void CreateFloor()
        {
            m_CurFloor++;
            GameObject go = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.Item, "Floor/Floor");
            Floor m_Floor = go.GetComponent<Floor>();
            m_Floor.Init(m_CurFloor + 3);
            if (m_LastFloor == null)
            {
                go.transform.position = Vector3.zero;
            }
            else
            {
                go.transform.position = m_LastFloor.transform.TransformPoint(new Vector3(0, m_FloorHeightIntervals, 0));
            }
            go.transform.rotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            m_LastFloor = m_Floor;
        }
    }
}
