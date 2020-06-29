using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Online
{

    public class RoomIDUI : BaseNetworkObject
    {

        [SerializeField]
        Text RoomID;

        public string roomID
        {
            get;
            private set;
        }

        private void Awake()
        {
            //roomID = FindObjectOfType<Sailing.Online.RoomFactory>().RoomIDReturn();
            //OnJoinedRoom();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();

            RoomID.text = "ルームID : " + PhotonNetwork.CurrentRoom.Name;
            //RoomID.text = "ルームID : " + roomID;
            //Debug.Log("RoomID : " + roomID);

        }

    }

}