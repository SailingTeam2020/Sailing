
// 2020/05/15 小林更新

using UnityEngine;
using Photon.Pun;
using Sailing.SingletonObject;
using System.Text;
using UnityEngine.UI;

namespace Sailing
{

    public class CPUShipObject : MonoBehaviour
    {

        private PhotonView photonView2;
        private GameObject Splashesobj2;
        private GameObject Splashes2;
        public GameObject Player2;
        public bool Cpu_Moves;

        public CpushipMove Cpu_Move
        {
            get;
            private set;
        }
        private void Awake()
        {

            photonView2 = PhotonView.Get(this);

            if (photonView2.IsMine)
            {

                GameObject child = new GameObject();
                child.transform.parent = gameObject.transform;

                Splashes2 = (GameObject)Resources.Load("SplashesEffect");
                Player2 = GameObject.Find("CPUShip");
                Splashesobj2 = Instantiate(Splashes2, this.transform.position, Quaternion.identity);
                Splashesobj2.transform.SetParent(Player2.transform, true);
                Splashesobj2.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                gameObject.tag = "Opponent";
            }
        }

    }
}
