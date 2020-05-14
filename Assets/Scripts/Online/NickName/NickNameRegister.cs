/*
 * 長嶋
 */

using UnityEngine;
using Photon.Pun;

namespace Sailing.Online
{

    public class NickNameRegister : BaseNetworkObject
    {

        [SerializeField]
        private string playerName;
        private const string DefaultPlayerName = "ゲストさん";

        private void Start()
        {

            if(playerName == "")
            {
                SetDefaultNickName();
                return;
            }

            PhotonNetwork.LocalPlayer.NickName = playerName;

        }

        private void SetDefaultNickName()
        {

            if(PhotonNetwork.LocalPlayer.NickName == "")
            {
                PhotonNetwork.LocalPlayer.NickName = DefaultPlayerName;
            }

        }

    }

}