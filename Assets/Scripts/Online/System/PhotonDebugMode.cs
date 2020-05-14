using Photon.Pun;
using UnityEngine;

namespace Sailing.Online
{

    public class PhotonDebugMode : BaseNetworkObject
    {

        [SerializeField, Tooltip("Photonに接続するかどうか")]
        private bool photonConnect;
        [SerializeField, Tooltip("Roomに入るかどうか")]
        private bool joinRoom;

        private void Awake()
        {

            if (photonConnect)
            {
                Connected();
            }

        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();

            if (joinRoom)
            {
                RoomFactory room = gameObject.AddComponent<RoomFactory>();
                room.Create();
                PhotonNetwork.JoinRandomRoom();
            }

        }

    }

}