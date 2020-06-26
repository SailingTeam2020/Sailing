﻿
// 2020/05/15 小林更新

using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

namespace Sailing
{

    public class ShipManager : MonoBehaviour
    {

        [SerializeField]
        private GameTimer gameTimer;

        private PhotonView photonView;
        private bool isShipStart;
        private bool isShipFinish;

        public ShipObject MainShipObject {
            get;
            private set;
        }
        public CPUShipObject CPUShipObject
        {
            get;
            private set;
        }

        public CpushipMove cpushipMove
        {
            get;
            private set;
        }

        public List<GameObject> ShipObjectList {
            get;
            private set;
        }

        private void Awake()
        {

            photonView = PhotonView.Get(this);

            ShipObjectList = new List<GameObject>();
            GameObject obj = gameObject.AddComponent<ShipFactory>().Create();
            MainShipObject = obj.GetComponent<ShipObject>();
            MainShipObject.GameTimer = gameTimer;
            isShipStart = false;
            isShipFinish = false;

            ShipObjectList.Add(obj);

        }

        private void Start()
        {

            gameTimer.Initialize();

        }

        private void FixedUpdate()
        {
            
            //カウントダウンが終了し、まだ船をスタートさせていない場合のみ
            if(gameTimer.IsCountStart && !isShipStart)
            {
                ShipStart();
                isShipStart = true;
            }

            if (MainShipObject.IsGoal && !isShipFinish)
            {
                photonView.RPC("GetGoalRank", RpcTarget.AllViaServer);
                isShipFinish = true;
            }

        }

        private void ShipStart()
        {

            MainShipObject.ChangeShipControlFlag(true, true,true);
            Debug.Log("レースが始まりました");

        }

        #region 順位

        [PunRPC]
        private void GetGoalRank()
        {

            if (photonView.IsMine)
            {
                Debug.Log("ゴール");
            }
            else
            {
                Debug.Log("誰かがゴールしました");
            }

        }

        #endregion

    }

}