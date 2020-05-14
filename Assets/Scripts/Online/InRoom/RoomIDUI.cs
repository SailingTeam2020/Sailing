using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Online
{

    public class RoomIDUI : BaseNetworkObject
    {

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            GetComponent<Text>().text = "ルームID : " + PhotonNetwork.CurrentRoom.Name;

        }

    }

}