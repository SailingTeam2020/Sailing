using UnityEngine;
using Sailing.SingletonObject;
using Photon.Pun;

namespace Sailing
{

    public class SelectModeManager : MonoBehaviour
    {

        private void Awake()
        {

            PhotonNetwork.OfflineMode = true;

        }

        private void Start()
        {

            SoundManager.Instance.PlayBGM("TT");

        }

    }

}