using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

namespace Sailing.Online
{

    public class OnlineMainShip : MonoBehaviour
    {

        [SerializeField]
        private List<GameObject> asynchronousObject;

        private PhotonView photonView;

        private void Awake()
        {

            photonView = PhotonView.Get(this);

        }

        // Start is called before the first frame update
        void Start()
        {

            if (!photonView.IsMine)
            {
                return;
            }

            foreach(GameObject obj in asynchronousObject)
            {
                obj.SetActive(true);
            }

        }

    }

}