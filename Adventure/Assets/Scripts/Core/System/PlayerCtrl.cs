using UnityEngine;
using System.Collections;
using EasyFrameWork;
using System.Collections.Generic;
using System;
using EasyFrameWork.UI;
using LFramework.Plugins.Astart;

namespace EasyFrameWork
{
    /// <summary>
    /// 玩家信息 和 操纵 等等
    /// </summary>
    public class PlayerCtrl : SystemCtrlBase<PlayerCtrl>, ISystemCtrl
    {
        private UIHeroIntroView m_HeroIntroView;
        private UIDragHeroView m_DragHeroView;
        private DragSkillItem m_DragSkillItem;
        private UISummonView m_SummonView;
        private UITaskView m_TaskView;
        private UIAllHeroView m_AllHeroView;
        private UIHeroInfoView m_HeroInfoView;


        private Vector2 m_DragSkillStartRelaticvePos;
        private Node m_LastDragSkllNode;

        public PlayerCtrl()
        {
            UIDispatcher.Instance.AddEventListen(ConstDefine.Hero_ShowHeroIntro, OnShowHeroIntroCallBak);
            UIDispatcher.Instance.AddEventListen(ConstDefine.Hero_HideHeroIntro, OnHideHeroIntroCallBak);

            UIDispatcher.Instance.AddEventListen(ConstDefine.UIHero_BeginDragTheHeroItem, OnUIBeginDragTheHeroItemCallBak);
            UIDispatcher.Instance.AddEventListen(ConstDefine.UIHero_UpdateDragTheHeroItem, OnUIUpdateDragTheHeroItemCallBak);
            UIDispatcher.Instance.AddEventListen(ConstDefine.UIHero_GoFight, OnUIGoFightCallBak);

            RoleDispatcher.Instance.AddEventListen(ConstDefine.Hero_ShowHeroIntro, OnShowHeroIntroCallBak);
            RoleDispatcher.Instance.AddEventListen(ConstDefine.Hero_HideHeroIntro, OnHideHeroIntroCallBak);

            RoleDispatcher.Instance.AddEventListen(ConstDefine.SceneHero_BeginDragTheHeroItem, OnSceneBeginDragTheHeroItemCallBack);
            RoleDispatcher.Instance.AddEventListen(ConstDefine.SceneHero_UpdateDragTheHeroItem, OnSceneUpdateDragTheHeroCallBack);
            RoleDispatcher.Instance.AddEventListen(ConstDefine.SceneHero_LevelFight, OnSceneLevelFightCallBack);

            RoleDispatcher.Instance.AddEventListen(ConstDefine.SceneHero_BeginDragTheSkillItem, OnBeginDragTheSkillCallBack);
            RoleDispatcher.Instance.AddEventListen(ConstDefine.SceneHero_UpdateDragTheSkillItem, OnUpdateDragTheSkillCallBack);
            RoleDispatcher.Instance.AddEventListen(ConstDefine.SceneHero_HeroAttack, OnHeroAttackCallBack);

            RoleDispatcher.Instance.AddEventListen(ConstDefine.UI_Summon_OneSummon, OnOneSummonCallBack);
            RoleDispatcher.Instance.AddEventListen(ConstDefine.UI_Summon_FiveSummon, OnFiveSummonCallBack);

            UIDispatcher.Instance.AddEventListen(ConstDefine.UI_TaskItem_ClickGetReward, OnClickGetRewardCallBack);


            UIDispatcher.Instance.AddEventListen(ConstDefine.UI_AllHero_ClickSimpleHeroInfo, OnClickSimpleHeroInfoCallBack);
            UIDispatcher.Instance.AddEventListen(ConstDefine.UI_AllHero_ClickHeroUpgradeStar, OnClickHeroUpgradeStarCallBack);

            TaskManager.Instance.TakeTaskEvent += OnTakeTaskCallBack;
            TaskManager.Instance.FinishTaskEvent += OnFinishTaskCallBack;
            TaskManager.Instance.GetRewardEvent += OnGetRewardCallBack;
            TaskManager.Instance.CancalTaskEvent += OnCancalTaskCallBack;
            TaskManager.Instance.UpdateProcessEvent += OnUpdateProcessCallBack;
        }



        public override void Dispose()
        {
            base.Dispose();
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.Hero_ShowHeroIntro, OnShowHeroIntroCallBak);
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.Hero_HideHeroIntro, OnHideHeroIntroCallBak);

            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UIHero_BeginDragTheHeroItem, OnUIBeginDragTheHeroItemCallBak);
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UIHero_UpdateDragTheHeroItem, OnUIUpdateDragTheHeroItemCallBak);
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UIHero_GoFight, OnUIGoFightCallBak);

            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.Hero_ShowHeroIntro, OnShowHeroIntroCallBak);
            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.Hero_HideHeroIntro, OnShowHeroIntroCallBak);

            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.SceneHero_BeginDragTheHeroItem, OnSceneBeginDragTheHeroItemCallBack);
            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.SceneHero_UpdateDragTheHeroItem, OnSceneUpdateDragTheHeroCallBack);
            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.SceneHero_LevelFight, OnSceneLevelFightCallBack);

            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.SceneHero_BeginDragTheSkillItem, OnBeginDragTheSkillCallBack);
            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.SceneHero_UpdateDragTheSkillItem, OnUpdateDragTheSkillCallBack);

            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.UI_Summon_OneSummon, OnOneSummonCallBack);
            RoleDispatcher.Instance.RemoveEventListen(ConstDefine.UI_Summon_FiveSummon, OnFiveSummonCallBack);

            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UI_TaskItem_ClickGetReward, OnClickGetRewardCallBack);

            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UI_AllHero_ClickSimpleHeroInfo, OnClickSimpleHeroInfoCallBack);
            UIDispatcher.Instance.RemoveEventListen(ConstDefine.UI_AllHero_ClickHeroUpgradeStar, OnClickHeroUpgradeStarCallBack);

            TaskManager.Instance.TakeTaskEvent -= OnTakeTaskCallBack;
            TaskManager.Instance.FinishTaskEvent -= OnFinishTaskCallBack;
            TaskManager.Instance.GetRewardEvent -= OnGetRewardCallBack;
            TaskManager.Instance.CancalTaskEvent -= OnCancalTaskCallBack;
            TaskManager.Instance.UpdateProcessEvent -= OnUpdateProcessCallBack;

        }



        public void OpenView(UIViewType type)
        {
            switch (type)
            {
                case UIViewType.SummonView:
                    OpenSummonView();
                    break;
                case UIViewType.TaskView:
                    OpenTaskView();
                    break;
                case UIViewType.AllHeroView:
                    OpenAllHeroView();
                    break;
            }
        }



        public void BindRoleCtrlEvent(List<RoleCtrl> roleList)
        {
            for (int i = 0; i < roleList.Count; i++)
            {
                roleList[i].OnHPChange += OnHPChangeCallBcak;
            }
        }

        private void OnHPChangeCallBcak(ValueChangeType type)
        {
            int totalCurHp = 0;
            int totalMaxHp = 0;
            for (int i = 0; i < Global.FightHeroList.Count; i++)
            {
                HeroInfo info = GameSceneCtrl.Instance.HeroInfoList.Find(x => x.RoleId == Global.FightHeroList[i]);
                totalCurHp += info.CurrentHP;
                totalMaxHp += info.MaxHP;
            }
            //UIPlayerInfoView.Instance.SetHp(totalCurHp, totalMaxHp);
        }


        #region 编辑阵营和操作
        /// <summary>
        /// 更新我获取的卡牌的状态
        /// </summary>
        public void UpdateAllHeroInfo()
        {
            UIAllHeroInfoTransfer t = new UIAllHeroInfoTransfer();
            for (int i = 0; i < Global.HeroInfoList.Count; i++)
            {
                HeroInfo info = Global.HeroInfoList[i];
                if (info == null)
                {
                    Debug.LogError("错啦，全错啦，菜逼!");
                    return;
                }
                UIAllHeroInfoTransfer.UIHeroInfo uinfo = PrepareUIHeroInfo(info);
                long roleId = Global.FightHeroList.Find(x => x == Global.HeroInfoList[i].RoleId);
                if (roleId != 0)
                {
                    uinfo.IsFight = true;
                }
                else
                {
                    uinfo.IsFight = false;
                }
                //配置拥有的所有英雄
                t.HeroInfoList.Add(uinfo);
            }
            ((UIGameSceneView)UISceneCtrl.Instance.CurrentUIScene).SetUI(t);
        }

        private void OnShowHeroIntroCallBak(object[] p)
        {
            //显示简介
            long roleId = (long)p[0];
            ShowHeroIntro(roleId);
        }
        //显示英雄简介
        private void ShowHeroIntro(long roleId)
        {
            if (m_HeroIntroView == null)
            {
                GameObject go = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "HeroIntro");
                m_HeroIntroView = go.GetComponent<UIHeroIntroView>();
                go.transform.SetParent(UISceneCtrl.Instance.CurrentUIScene.MainCanvans.transform);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = Vector3.zero;
            }
            m_HeroIntroView.gameObject.SetActive(true);
            DataTransfer data = new DataTransfer();
            HeroInfo heroInfo = Global.HeroInfoList.Find(x => x.RoleId == roleId);
            if (heroInfo != null)
            {

                if (heroInfo != null)
                {
                    HeroEntity heroEntity = HeroDBModel.Instance.Get(heroInfo.HeroID);
                    data.SetData<string>("HeroName", heroEntity.Name);
                    data.SetData<string>("HeroDesc", heroEntity.Desc);
                    for (int i = 0; i < heroInfo.SkillList.Count; i++)
                    {
                        SkillEntity skill1Entity = SkillDBModel.Instance.Get(heroInfo.SkillList[i].SkillId);
                        data.SetData<string>("Skill" + (i + 1) + "Name", skill1Entity.SkillName);
                        data.SetData<string>("Skill" + (i + 1) + "Desc", skill1Entity.SkillDesc);
                    }
                }
            }
            else
            {
                //敌人
                RoleInfoBase monsterInfo = GameSceneCtrl.Instance.MonsterList.Find(x => x.CurRoleInfo.RoleId == roleId).CurRoleInfo;
                if (monsterInfo != null)
                {
                    SpriteEntity spriteEntity = SpriteDBModel.Instance.GetList().Find(x => x == ((MonsterInfo)monsterInfo).SpriteEntity);
                    if (spriteEntity == null)
                    {
                        Debug.LogError("错误：找不到精灵实体，请查看是否有没有赋值，或者GameSceneCtrl的MonsterList 移除，添加出错了");
                        return;
                    }
                    else
                    {
                        data.SetData<string>("HeroName", spriteEntity.Name);
                        data.SetData<string>("HeroDesc", "");
                        for (int j = 0; j < ((MonsterInfo)monsterInfo).GetSkillList().Count; j++)
                        {
                            SkillEntity skill1Entity = SkillDBModel.Instance.Get(((MonsterInfo)monsterInfo).GetSkillList()[j]);
                            data.SetData<string>("Skill" + (j + 1) + "Name", skill1Entity.SkillName);
                            data.SetData<string>("Skill" + (j + 1) + "Desc", skill1Entity.SkillDesc);
                        }
                    }
                }
                else
                {
                    Debug.Log("错误：没找到英雄信息");
                    return;
                }

            }
            m_HeroIntroView.SetUI(data);


        }
        //开始技能拖拽
        private void OnBeginDragTheSkillCallBack(object[] p)
        {
            long roleId = (long)p[0];
            Vector2 rolePos = (Vector3)p[1];
            Vector2 minPos = Vector2.zero;
            float minDis = Mathf.Infinity;
            //从第一列中找最近的点
            for (int i = 0; i < GridManager.Instance.NumOfRow; i++)
            {
                Node n = GridManager.Instance.GetNode(i, 0);
                float dis = Vector2.Distance(rolePos, n.Position);
                if (dis < minDis)
                {
                    minDis = dis;
                    minPos = n.Position;
                }
            }
            if (m_DragSkillItem == null)
            {
                m_DragSkillItem = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.Scene, "Items/DragSkillItem").GetComponent<DragSkillItem>();
                m_DragSkillItem.transform.SetParent(null);
                m_DragSkillItem.transform.localScale = Vector3.one;

            }
            m_DragSkillStartRelaticvePos = minPos - rolePos;
            m_DragSkillItem.transform.position = minPos;
            RoleCtrl role = GameSceneCtrl.Instance.GetHero(roleId);
            if (role == null)
            {
                Debug.LogError("错误:");
                return;
            }
            HeroInfo heroInfo = Global.HeroInfoList.Find(x => x.RoleId == roleId);

            SkillEntity skillEntity = SkillDBModel.Instance.Get((role.IsRage ? heroInfo.SkillList[1] : heroInfo.SkillList[0]).SkillId);
            m_DragSkillItem.Init(role, skillEntity.AttackArea, skillEntity.AttackRange);
        }
        //更新技能拖拽
        private void OnUpdateDragTheSkillCallBack(object[] p)
        {
            Vector3 screenPos = (Vector3)p[0];
            Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            Node n = GridManager.Instance.GetNode(worldPos + m_DragSkillStartRelaticvePos);
            if (n == null) return;
            if (n != m_LastDragSkllNode)
            {
                //更新技能物体的位置
                m_LastDragSkllNode = n;
                m_DragSkillItem.transform.position = n.Position;
                m_DragSkillItem.AfterRefreshPositio();
            }
        }
        private void OnHeroAttackCallBack(object[] p)
        {
            if (GameSceneCtrl.Instance.GetLevelProcedure.CurState == LevelProcedure.Player)
            {
                RoleCtrl role = (RoleCtrl)p[0];
                int skillId = (role.IsRage ? ((HeroInfo)role.CurRoleInfo).SkillList[1] : ((HeroInfo)role.CurRoleInfo).SkillList[0]).SkillId;
                role.ToAttack(AttackType.SkillAttack, skillId);
                m_DragSkillItem.Hide();
                m_DragSkillItem.transform.position = Vector3.one * 2000;
                GameSceneCtrl.Instance.HeroFaction.Used(role);
            }

        }
        //隐藏英雄简介
        private void OnHideHeroIntroCallBak(object[] p)
        {
            if (m_HeroIntroView != null)
            {
                m_HeroIntroView.gameObject.SetActive(false);
            }
        }
        //拖拽英雄
        private void OnSceneBeginDragTheHeroItemCallBack(object[] p)
        {
            GameObject go = (GameObject)p[0];
            Debug.Log(go.name);
            //变换层级
            SetGameObjectLayer(go, "UI", 500);
            go.transform.SetParent(((UIGameSceneView)UISceneCtrl.Instance.CurrentUIScene).HerosContains);
        }
        //更新拖拽英雄
        private void OnSceneUpdateDragTheHeroCallBack(object[] p)
        {
            GameObject go = (GameObject)p[0];
            Vector3 scenePos = (Vector3)p[1];
            Vector3 worldPos = UISceneCtrl.Instance.CurrentUIScene.UICamera.ScreenToWorldPoint(scenePos);
            go.transform.position = worldPos;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
        }
        //英雄脱离战场
        private void OnSceneLevelFightCallBack(object[] p)
        {

            RoleCtrl ctrl = (RoleCtrl)p[0];
            Vector3 scenePos = (Vector3)p[1];

            HeroPlatform platform = GameSceneCtrl.Instance.GetHeroPlatformByScreenPos(scenePos);
            //依然在平台内
            if (platform != null)
            {
                SetGameObjectLayer(ctrl.gameObject, "Role", 0);
                ctrl.transform.SetParent(null);
                if (platform.RoleId != 0)
                {
                    //替换
                    GameSceneCtrl.Instance.SwapHeroOnPlatfrom(ctrl.CurRoleInfo.RoleId, platform.RoleId);
                }
                else
                {
                    //刷新
                    GameSceneCtrl.Instance.RefreshHeroPlatform(ctrl.CurRoleInfo.RoleId, platform);
                }

            }
            else
            {
                //在UI内
                RectTransform rect = ((UIGameSceneView)UISceneCtrl.Instance.CurrentUIScene).HeroGridRect;
                bool inHeroGrid = RectTransformUtility.RectangleContainsScreenPoint(rect, scenePos, UISceneCtrl.Instance.CurrentUIScene.UICamera);
                if (inHeroGrid)
                {
                    //移除这个战斗英雄
                    SimulatedDatabase.Instance.RemoveFightHero(ctrl.CurRoleInfo.RoleId);
                    GameObject.DestroyImmediate(ctrl.gameObject);
                    UpdateAllHeroInfo();
                }
                else
                {
                    //复原
                    SetGameObjectLayer(ctrl.gameObject, "Role", 0);
                    ctrl.transform.SetParent(null);
                    ctrl.StandOnPlatfrom();
                }
            }


        }


        private void OnUIBeginDragTheHeroItemCallBak(object[] p)
        {
            int heroId = (int)p[0];
            ShowDragHeroItem(heroId);
        }

        private void OnUIUpdateDragTheHeroItemCallBak(object[] p)
        {
            Vector2 pos = (Vector2)p[0];
            UpdateDragHeroItem(pos);
        }
        private void OnUIGoFightCallBak(object[] p)
        {
            long roleId = (long)p[0];
            Vector2 screenPos = (Vector2)p[1];
            HideDragItem();
            //是否有平台
            HeroPlatform platform = GameSceneCtrl.Instance.GetHeroPlatformByScreenPos(screenPos);
            if (platform != null)
            {
                if (platform.RoleId != 0)
                {
                    //替换
                    SimulatedDatabase.Instance.ReplaceFightHero(platform.RoleId, roleId);
                    GameSceneCtrl.Instance.ReplaceHeroOnPlatform(platform.RoleId, roleId, screenPos);
                    UpdateAllHeroInfo();
                }
                else
                {
                    //创建
                    if (GameSceneCtrl.Instance.HasPlatform(screenPos))
                    {
                        GameSceneCtrl.Instance.CreateHero(roleId, screenPos);
                        SimulatedDatabase.Instance.AddFightHero(roleId);
                        UpdateAllHeroInfo();
                    }
                }
            }

        }

        public void ShowDragHeroItem(int heroId)
        {
            if (m_DragHeroView == null)
            {
                GameObject DragHeroItem = ResourcesMrg.Instance.Load(ResourcesMrg.ResourceType.UIWindowChild, "DragHeroItem");
                DragHeroItem.transform.SetParent(UISceneCtrl.Instance.CurrentUIScene.MainCanvans.transform);
                DragHeroItem.transform.localScale = Vector3.one;
                m_DragHeroView = DragHeroItem.GetComponent<UIDragHeroView>();
                HeroInfo info = Global.HeroInfoList.Find(x => x.HeroID == heroId);
                if (info == null)
                {
                    Debug.LogError("错啦，全错啦，菜逼!");
                    return;
                }
                UIAllHeroInfoTransfer.UIHeroInfo uinfo = PrepareUIHeroInfo(info);
                m_DragHeroView.SetUI(uinfo.HeroIcon, uinfo.AttackArea, uinfo.AttackRange, uinfo.JobIcon, uinfo.HeroName);

            }
            m_DragHeroView.gameObject.SetActive(true);
        }

        public void UpdateDragHeroItem(Vector2 screenPos)
        {
            if (m_DragHeroView != null)
            {
                m_DragHeroView.gameObject.SetActive(true);
                Vector2 localPos = Vector2.zero;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(UISceneCtrl.Instance.CurrentUIScene.MainCanvans.GetComponent<RectTransform>(), screenPos, UISceneCtrl.Instance.CurrentUIScene.UICamera, out localPos);
                m_DragHeroView.transform.localPosition = localPos;
            }
        }

        public void HideDragItem()
        {
            if (m_DragHeroView != null)
            {
                m_DragHeroView.gameObject.SetActive(false);
                m_DragHeroView.transform.localPosition = Vector3.one * 1000;
            }
        }

        private UIAllHeroInfoTransfer.UIHeroInfo PrepareUIHeroInfo(HeroInfo info)
        {
            UIAllHeroInfoTransfer.UIHeroInfo uinfo = new UIAllHeroInfoTransfer.UIHeroInfo();

            uinfo.RoleId = info.RoleId;
            uinfo.HeroId = info.HeroID;
            HeroEntity heroEntity = HeroDBModel.Instance.Get(uinfo.HeroId);
            uinfo.HeroIcon = heroEntity.HeadPic;
            uinfo.HeroName = heroEntity.Name;
            SkillEntity skillEntity = SkillDBModel.Instance.Get(info.SkillList[0].SkillId);
            uinfo.AttackArea = skillEntity.AttackArea;
            uinfo.AttackRange = skillEntity.AttackRange;
            JobEntity jobEntity = JobDBModel.Instance.Get(heroEntity.Job);
            uinfo.JobIcon = jobEntity.Icon;

            return uinfo;
        }

        private void SetGameObjectLayer(GameObject go, string layer, int sortOder)
        {
            Transform[] gos = go.GetComponentsInChildren<Transform>();
            for (int i = 0; i < gos.Length; i++)
            {
                gos[i].gameObject.layer = LayerMask.NameToLayer(layer);
                Renderer r = gos[i].GetComponent<Renderer>();
                if (r != null)
                {
                    r.sortingOrder = sortOder;
                }
            }
        }
        #endregion

        #region 召唤
        private void OpenSummonView()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.SummonView);
            m_SummonView = go.GetComponent<UISummonView>();
            int allCoin = SimulatedDatabase.Instance.GetCoin();
            int oneCoin = ConfigDBModel.Instance.GetList()[0].OneSummonCoin;
            int fiveCoin = ConfigDBModel.Instance.GetList()[0].FiveSummonCoin;
            m_SummonView.SetUI(allCoin, oneCoin, fiveCoin);
        }

        private void OnFiveSummonCallBack(object[] p)
        {
            MainSceneCtrl.Instance.StartCoroutine(OnFiveSummonCallBackIE());
        }

        private IEnumerator OnFiveSummonCallBackIE()
        {
            int allCoin = SimulatedDatabase.Instance.GetCoin();
            int fiveCoin = ConfigDBModel.Instance.GetList()[0].FiveSummonCoin;
            if (allCoin >= fiveCoin)
            {
                Stack<SummonInfoTransfer> t = new Stack<SummonInfoTransfer>();
                for (int i = 0; i < 5; i++)
                {
                    t.Push(Summon());
                    yield return null;//为了区分RoleID
                }
                m_SummonView.SummonResult(t);
                SimulatedDatabase.Instance.ReduceCoin(fiveCoin);
                m_SummonView.Refresh(SimulatedDatabase.Instance.GetCoin());
            }
            else
            {
                //提示钱不够
                //TODO
                Debug.Log("钱不够");
            }
        }

        private void OnOneSummonCallBack(object[] p)
        {
            int allCoin = SimulatedDatabase.Instance.GetCoin();
            int oneCoin = ConfigDBModel.Instance.GetList()[0].OneSummonCoin;
            if (allCoin >= oneCoin)
            {
                Stack<SummonInfoTransfer> t = new Stack<SummonInfoTransfer>();
                t.Push(Summon());
                m_SummonView.SummonResult(t);
                SimulatedDatabase.Instance.ReduceCoin(oneCoin);
                m_SummonView.Refresh(SimulatedDatabase.Instance.GetCoin());
            }
            else
            {
                //提示钱不够
                //TODO
                Debug.Log("钱不够");
            }

        }




        private SummonInfoTransfer Summon()
        {
            ConfigEntity configEntity = ConfigDBModel.Instance.GetList()[0];
            float randomRange = UnityEngine.Random.Range(0, 1f);
            List<HeroEntity> heroList = null;
            HeroInfo info = null;
            HeroEntity heroEntity = null;
            int heroStar = 1;
            if (randomRange <= configEntity.TheProbabilityOfLegendHero)
            {
                //抽取传奇英雄
                heroStar = 3;
                heroList = HeroDBModel.Instance.GetList().FindAll(x => x.Quality == 2);

            }
            else if (randomRange <= configEntity.TheProbabilityOfLegendHero + configEntity.TheProbabilityOfeliteHero)
            {
                //抽取精英英雄
                heroStar = 2;
                heroList = HeroDBModel.Instance.GetList().FindAll(x => x.Quality == 1);
            }
            else if (randomRange <= configEntity.TheProbabilityOfLegendHero + configEntity.TheProbabilityOfeliteHero + configEntity.TheProbabilityOfNormalHero)
            {
                //抽取普通英雄
                heroList = HeroDBModel.Instance.GetList().FindAll(x => x.Quality == 0);
            }
            else
            {
                Debug.LogError("错误：配置表填错了");
                return SummonInfoTransfer.Empty();
            }
            if (heroList.Count > 0)
            {
                heroEntity = heroList[UnityEngine.Random.Range(0, heroList.Count)];

            }
            else
            {
                Debug.Log("没有英雄哦");
                return SummonInfoTransfer.Empty();
            }

            SummonInfoTransfer transfer = new SummonInfoTransfer();
            //有没有这个英雄
            if (Global.HeroInfoList.Find(x => x.HeroID == heroEntity.Id) != null)
            {
                transfer.IsDebris = true;
                if (heroEntity.Quality == 0)
                {
                    transfer.DebrisCount = configEntity.TheDebrisOfNormalHero;
                }
                else if (heroEntity.Quality == 1)
                {
                    transfer.DebrisCount = configEntity.TheDebrisOfEliteHero;
                }
                else if (heroEntity.Quality == 2)
                {
                    transfer.DebrisCount = configEntity.TheDebrisOfLegendHero;
                }
                SimulatedDatabase.Instance.AddDebris(transfer.DebrisCount);
            }
            else
            {
                info = SimulatedDatabase.Instance.CreateHero(heroEntity.Id, heroStar);
                transfer.IsDebris = false;
            }
            transfer.NickName = heroEntity.Name;
            transfer.Quality = heroEntity.Quality;
            transfer.HeroPic = heroEntity.RolePic;
            return transfer;
        }
        #endregion

        #region 任务系统

        private void OpenTaskView()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.TaskView);
            m_TaskView = go.GetComponent<UITaskView>();
            DataTransfer data = new DataTransfer();
            List<DataTransfer> datas = new List<DataTransfer>();
            List<PlayerInfo.SimpleTaskInfo> taskList = SimulatedDatabase.Instance.GetAllEveryDayTask();
            for (int i = 0; i < taskList.Count; i++)
            {
                DataTransfer d = new DataTransfer();
                int taskId = taskList[i].TaskId;
                TaskEntity taskEntity = TaskDBModel.Instance.Get(taskId);
                if (taskEntity == null)
                {
                    Debug.LogError("错误：任务实体找不到");
                    continue;
                }
                d.SetData(ConstDefine.UI_Task_Id, taskId);
                d.SetData(ConstDefine.UI_TaskItem_Name, taskEntity.Name);
                d.SetData(ConstDefine.UI_TaskItem_Desc, taskEntity.Desc);
                int targetCount = taskEntity.GetTaskConditionEntityList()[0].TargetCount;//本项目有且只有一个条件
                d.SetData(ConstDefine.UI_TaskItem_TargetCount, targetCount);
                d.SetData(ConstDefine.UI_TaskItem_CurCount, taskList[i].ConditionInfoList[0].CurCount);
                d.SetData(ConstDefine.UI_TaskItem_GetReward, taskList[i].GetReward);
                datas.Add(d);
            }
            data.SetData(ConstDefine.UI_Task_Content, datas);
            m_TaskView.SetUI(data);
        }
        //点击获取奖励按钮
        private void OnClickGetRewardCallBack(object[] p)
        {
            int taskId = (int)p[0];
            TaskManager.Instance.GetReward(taskId);
            SimulatedDatabase.Instance.GetReward(taskId);
        }
        //获取奖励回调
        private void OnGetRewardCallBack(int rewardId)
        {
            TaskRewardEntity entity = TaskRewardDBModel.Instance.Get(rewardId);
            if (entity == null)
            {
                Debug.LogError("错误：奖励实体找不到");
                return;
            }
            switch (entity.RewardType)
            {
                case 0:
                    Debug.Log("加经验：" + entity.Count);
                    break;
                case 1:
                    Debug.Log("加金币：" + entity.Count);
                    SimulatedDatabase.Instance.AddCoin(entity.Count);
                    break;
            }

        }
        private void OnUpdateProcessCallBack(int taskId, int conditionId, int curCount)
        {
            //更新数据库
            SimulatedDatabase.Instance.UpdateEveryDayTask(taskId, conditionId, curCount);
            //任务条件只有一个，这里暂时这样
            if (m_TaskView != null)
            {
                m_TaskView.UpdateTaskView(taskId, conditionId, curCount);
            }
        }

        private void OnCancalTaskCallBack(int taskId)
        {
            //更新任务面板
            //这里什么都做
        }

        private void OnFinishTaskCallBack(int taskId)
        {
            if (m_TaskView != null)
            {
                m_TaskView.FinishTheTask(taskId);
            }
        }

        private void OnTakeTaskCallBack(int taskId)
        {
            //接任务
            //这里什么都做
        }
        #endregion

        #region  英雄系统
        private void OpenAllHeroView()
        {
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.AllHeroView);
            m_AllHeroView = go.GetComponent<UIAllHeroView>();
            DataTransfer data = new DataTransfer();
            List<DataTransfer> datas = new List<DataTransfer>();
            List<HeroInfo> infos = Global.HeroInfoList;
            for (int i = 0; i < infos.Count; i++)
            {
                DataTransfer d = new DataTransfer();
                d.SetData(ConstDefine.UI_AllHero_RoleId, infos[i].RoleId);
                d.SetData(ConstDefine.UI_AllHero_HeroStar, infos[i].HeroStar);
                d.SetData(ConstDefine.UI_AllHero_HeroName, infos[i].RoleNickName);
                RoleInfoSkill skillInfo = infos[i].SkillList[0];//只能一个
                if (skillInfo == null)
                {
                    Debug.LogError("错误：这个英雄没有技能，或者没装载技能");
                    continue;
                }
                SkillEntity skillEntity = SkillDBModel.Instance.Get(skillInfo.SkillId);
                if (skillEntity == null)
                {
                    Debug.LogError("错误：找不到技能实体");
                    continue;
                }
                string skillIcon = Global.Instance.GetSkillIconByRanggeAndType(skillEntity.AttackRange, skillEntity.AttackArea);

                d.SetData(ConstDefine.UI_AllHero_HeroSkillICon, skillIcon);
                HeroEntity heroEntity = HeroDBModel.Instance.Get(infos[i].HeroID);
                if (heroEntity == null)
                {
                    Debug.LogError("错误：找不到英雄实体");
                    continue;
                }
                d.SetData(ConstDefine.UI_AllHero_HeroHead, heroEntity.HeadPic);
                datas.Add(d);
            }
            data.SetData(ConstDefine.UI_AllHero_Content, datas);
            m_AllHeroView.SetUI(data);
        }

        private void OnClickSimpleHeroInfoCallBack(object[] p)
        {
            long roleId = (long)p[0];
            DataTransfer data = GetHeroInfoData(roleId);
            if (data != null)
            {
                m_HeroInfoView.SetUI(data);
            }

        }

        private void OnClickHeroUpgradeStarCallBack(object[] p)
        {
            long roleId = (long)p[0];
            HeroInfo heroInfo = Global.HeroInfoList.Find(x => x.RoleId == roleId);
            if (heroInfo == null)
            {
                Debug.LogError("错误：没有英雄信息");
                return;
            }
            if (heroInfo.HeroStar >= 5)
            {
                //提示
                //TODO
                Debug.Log("此英雄已经是最高星级");
                return;
            }
            HeroStarEntity heroStarEntity = HeroStarDBModel.Instance.GetList().Find((x) => x.Star == heroInfo.HeroStar);
            if (heroStarEntity == null)
            {
                Debug.LogError("错误：找不到英雄星级实体");
                return;
            }
            if (SimulatedDatabase.Instance.GetDebris() >= heroStarEntity.NeedHeroDebris)
            {
                SimulatedDatabase.Instance.UpgradeHeroStart(roleId);
                SimulatedDatabase.Instance.ReduceDebris(heroStarEntity.NeedHeroDebris);
                DataTransfer data = GetHeroInfoData(roleId);
                if (data != null)
                {
                    m_HeroInfoView.RefreshAfterUpgradeStar(data);
                }
            }
            else
            {
                //提示
                //TODO
                Debug.Log("英雄残魂不够");
            }

        }

        private DataTransfer GetHeroInfoData(long roldID)
        {
            long roleId = roldID;
            GameObject go = UIViewUtil.Instance.OpenWindow(UIViewType.HeroInfoView);
            m_HeroInfoView = go.GetComponent<UIHeroInfoView>();
            if (m_HeroInfoView == null)
            {
                Debug.LogError("错误：没有这个界面");
                return null;
            }
            HeroInfo heroInfo = Global.HeroInfoList.Find(x => x.RoleId == roleId);
            if (heroInfo == null)
            {
                Debug.LogError("错误：没有英雄信息");
                return null;
            }
            HeroEntity heroEntity = HeroDBModel.Instance.Get(heroInfo.HeroID);
            if (heroEntity == null)
            {
                Debug.LogError("错误：找不到英雄实体");
                return null;
            }
            string heroName = heroInfo.RoleNickName;
            int heroStar = heroInfo.HeroStar;
            int heroQuality = heroEntity.Quality;
            int heroLevel = SimulatedDatabase.Instance.GetLevel();
            int hp = heroInfo.MaxHP;
            int mgicAtk = heroInfo.MgicAtk;
            int phyAtk = heroInfo.PhyAtk;
            int cri = heroInfo.Cri;
            float criValue = heroInfo.CriValue;
            int phyDef = heroInfo.PhyDef;
            int mgicDef = heroInfo.MgicDef;

            DataTransfer data = new DataTransfer();
            data.SetData(ConstDefine.UI_HeroInfo_RoleId, roleId);
            data.SetData(ConstDefine.UI_HeroInfo_HeroName, heroName);
            data.SetData(ConstDefine.UI_HeroInfo_HeroStar, heroStar);
            data.SetData(ConstDefine.UI_HeroInfo_HeroQuality, heroQuality);
            data.SetData(ConstDefine.UI_HeroInfo_HeroLevel, heroLevel);

            data.SetData(ConstDefine.UI_HeroInfo_HeroHp, hp);
            data.SetData(ConstDefine.UI_HeroInfo_MgicAtk, mgicAtk);
            data.SetData(ConstDefine.UI_HeroInfo_PhyAtk, phyAtk);
            data.SetData(ConstDefine.UI_HeroInfo_Cri, cri);
            data.SetData(ConstDefine.UI_HeroInfo_CriValue, criValue);
            data.SetData(ConstDefine.UI_HeroInfo_PhyDef, phyDef);
            data.SetData(ConstDefine.UI_HeroInfo_MgicDef, mgicDef);

            List<DataTransfer> skillDatas = new List<DataTransfer>();
            for (int i = 0; i < heroInfo.SkillList.Count; i++)
            {
                DataTransfer skillData = new DataTransfer();
                SkillEntity skillEntity = SkillDBModel.Instance.Get(heroInfo.SkillList[i].SkillId);
                skillData.SetData(ConstDefine.UI_HeroInfo_SkillName, skillEntity.SkillName);
                string skillIcno = Global.Instance.GetSkillIconByRanggeAndType(skillEntity.AttackRange, skillEntity.AttackArea);
                skillData.SetData(ConstDefine.UI_HeroInfo_SkillIcon, skillIcno);
                skillData.SetData(ConstDefine.UI_HeroInfo_SkillDesc, skillEntity.SkillDesc);
                skillDatas.Add(skillData);
            }
            data.SetData(ConstDefine.UI_HeroInfo_SkillContent, skillDatas);

            HeroStarEntity heroStarEntity = HeroStarDBModel.Instance.GetList().Find((x) => x.Star == heroStar);
            data.SetData(ConstDefine.UI_HeroInfo_NeedDebris, heroStarEntity.NeedHeroDebris);
            data.SetData(ConstDefine.UI_HeroInfo_OwnedDebris, SimulatedDatabase.Instance.GetDebris());
            return data;
        }

        #endregion

    }
}
