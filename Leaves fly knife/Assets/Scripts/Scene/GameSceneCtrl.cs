using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using EasyFrameWork.UI;
using UnityEngine.EventSystems;
using System;

namespace EasyFrameWork
{
    //层级设定 最低黑边-1 背景0 场景物体1 特效2
    public class GameSceneCtrl : MonoSingleton<GameSceneCtrl>
    {
        public enum GameStae
        {
            Play,
            Pause,
            Over
        }
        [SerializeField]
        private Emitter m_CurEmitter;
        [SerializeField]
        private Missile m_CurKnife;
        private Missile m_PreKnife;
        [SerializeField]
        private Star m_Star;
        [SerializeField]
        private List<Monster> m_MonsterList;
        [SerializeField]
        private int m_KnifeCount;
        private int m_KillMonsterCount;
        private GameStae m_GameStae;
        private UIGameSceneView m_View;
        private Coroutine m_C;
        [HideInInspector]
        public int StarCount;
        [SerializeField]
        private float m_CanOverTimer;

        // Use this for initialization
        void Start()
        {
            MissilePath.Init();
            m_GameStae = GameStae.Play;
            m_View = UISceneCtrl.Instance.Load(UISceneType.Game).GetComponent<UIGameSceneView>();
            ChangeSceneCtrl.Instance.Show(ChangeSceneType.Open);
            UIRockerView.Instance.AddEventOnRocker("rocker", OnRockerEvent);
            CreateKnife();
            RefreshEmitter(m_CurEmitter);
            m_CurEmitter.Fixed(m_CurKnife);
            StarCount = -1;
            m_View.SetUI(m_KnifeCount, Global.CurLevel);
            m_Star.CollectEvent += OnStarHit;
            for (int i = 0; i < m_MonsterList.Count; i++)
            {
                m_MonsterList[i].DeadEvent += OnMonsterDead;
            }
        }


        private void OnMonsterDead()
        {
            StartCoroutine(OnMonsterDeadIE());
        }


        private IEnumerator OnMonsterDeadIE()
        {

            if (m_GameStae == GameStae.Over) yield break;
            m_KillMonsterCount++;
            if (m_KillMonsterCount >= m_MonsterList.Count)
            {
                if (m_C != null)
                {
                    StopCoroutine(m_C);
                }
                m_GameStae = GameStae.Over;
                yield return new WaitForSeconds(1);
                UIViewMgr.Instance.OpenView(UIViewType.Win);
            }
        }
        private void OnStarHit()
        {
            m_View.GetStar(m_Star.transform.position);
            StarCount = 1;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_GameStae != GameStae.Play) return;

                if (m_PreKnife != null)
                {
                    Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(r.origin, Vector2.up, 0.1f);

                    if (hit.collider != null)
                    {
                        Emitter e = hit.collider.GetComponent<Emitter>();
                        if (e != null && e == m_CurEmitter) return;
                    }
                    m_PreKnife.SetUniformMotion();
                }
            }
            if (m_GameStae == GameStae.Play)
            {
                m_CanOverTimer -= Time.deltaTime;
                if (m_CanOverTimer < 0)
                {
                    if (m_KnifeCount <= 0)
                    {
                        m_GameStae = GameStae.Over;
                        UIViewMgr.Instance.OpenView(UIViewType.Fail);
                    }
                }
            }
        }

        private void OnRockerEvent(Vector2 arg1, float arg2, bool arg3)
        {
            if (m_GameStae != GameStae.Play || m_KnifeCount <= 0) return;
            if (m_CurKnife != null)
            {
                m_CurEmitter.ShowOrHideInstructions(false);
                m_CurKnife.InputFireParameter(-arg1, arg2, arg3);
            }
        }
        public void RefreshEmitter(Emitter emitter)
        {
            m_CurEmitter.ShowOrHideInstructions(false);
            m_CurEmitter = emitter;
            m_CurEmitter.ShowOrHideInstructions(true);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(this.m_CurEmitter.transform.position);
            m_View.SetRockerPos(screenPos);
            EmitterFixed();
        }
        public void EmitterFixed()
        {
            if (m_CurKnife != null)
            {
                m_CurEmitter.Fixed(m_CurKnife);
            }
        }

        private IEnumerator OnShootIE()
        {

            yield return new WaitForSeconds(5);
            if (m_GameStae == GameStae.Over) yield break;
            if (m_KnifeCount <= 0)
            {
                m_GameStae = GameStae.Over;
                UIViewMgr.Instance.OpenView(UIViewType.Fail);
                yield break;
            }

        }

        private void OnShoot()
        {
            m_CanOverTimer = 5;
            m_KnifeCount--;
            m_View.SetUI(m_KnifeCount,Global.CurLevel);
            CreateKnife();
            m_CurEmitter.Fixed(m_CurKnife);

        }

        public void SetCanOverRime(float time)
        {
            m_CanOverTimer = time;
        }
        int index = 0;
        private void CreateKnife()
        {
            m_PreKnife = m_CurKnife;
            GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Item, "Item1/Knife", isCache: true);
            go.name = (++index).ToString();
            m_CurKnife = go.GetComponent<Missile>();
            m_CurKnife.OnShoot = OnShoot;
        }
        //private void OnGUI()
        //{
        //    if (GUI.Button(new Rect(30, 50, 50, 50), "胜利"))
        //    {
        //        UIViewMgr.Instance.OpenView(UIViewType.Win);
        //    }
        //    if (GUI.Button(new Rect(30, 100, 50, 50), "失败"))
        //    {
        //        UIViewMgr.Instance.OpenView(UIViewType.Fail);
        //    }
        //}

    }
}
