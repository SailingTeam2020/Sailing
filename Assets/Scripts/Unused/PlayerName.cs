using Photon.Pun;
using UnityEngine;

namespace Sailing.Online
{

    public class PlayerName : MonoBehaviourPunCallbacks
    {

        [SerializeField]
        private GameObject playerName;  // 名前のオブジェクトをアタッチする用

        private void Start()
        {

            playerName.GetComponent<TextMesh>().text = photonView.Owner.NickName;

        }

    }

}