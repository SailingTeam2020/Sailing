/*
 * 
 * 2020/06/29 小林更新
 *
 */
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Online
{

    public class RoomIDUI : BaseNetworkObject
    {

        [SerializeField]
        Text RoomID;

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            //RoomID.text = "ルームID : " + PhotonNetwork.CurrentRoom.Name;
            //Debug.Log("RoomID : " + roomID);

        }

    }

}