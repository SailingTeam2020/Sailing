/*
 * 長嶋
 */

using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

namespace Sailing.Online
{

    public class MatchingCountdown : MonoBehaviour
    {

        [SerializeField]
        private float gameStartTime = 30.0f;

        private double gameStartCount;

        public bool IsTimeCount {
            get;
            set;
        }

        public float LeftCountTime {
            get;
            private set;
        }

        private void Awake()
        {

            IsTimeCount = false;
            LeftCountTime = gameStartTime;

        }

        public void Initialized()
        {

            IsTimeCount = true;
            gameStartCount = (double)PhotonNetwork.CurrentRoom.CustomProperties[RoomPropertyKey.InRoomLimitTime];

        }

        // Update is called once per frame
        private void Update()
        {

            if (!IsTimeCount)
            {
                return;
            }

            //制限時間を過ぎた場合
            if (LeftCountTime <= 0.0f)
            {
                LeftCountTime = 0.0f;
                return;
            }

            double elapsedTime = PhotonNetwork.Time - gameStartCount;
            LeftCountTime = gameStartTime - (float)elapsedTime;

        }

    }

}