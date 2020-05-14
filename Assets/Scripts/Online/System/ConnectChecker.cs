using UnityEngine;
using Photon.Pun;

namespace Sailing.Online
{

    public class ConnectChecker : BaseNetworkObject
    {

        public void Awake()
        {

            if (!PhotonNetwork.IsConnected)
            {
                Debug.LogError("Photonに接続していません。タイトルに戻ります");

                SceneSwitch(Sailing.SceneNameString.Title);
                return;
            }

        }


    }

}