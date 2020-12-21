using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sailing
{

    public class ShipFactory : MonoBehaviour
    {

        [SerializeField] int CPUFactory = 8;

        private const string ShipPrefabName = "Ship";
        private const string CPUShipPrefabName = "CPUShip";
        public GameObject Create()
        {

            //生成位置をファクトリーに書くのはよくないから直す予定
            Vector3 start = Vector3.zero;
            float x = 3.0f * PhotonNetwork.LocalPlayer.ActorNumber;
            Vector3 vec = new Vector3(x + start.x, start.y, start.z);

            GameObject obj = PhotonNetwork.Instantiate(ShipPrefabName, vec, Quaternion.identity) as GameObject;
            obj.name = "Ship";

            if (!obj)
            {
                Debug.Log("Playerの生成に失敗しました");
                return null;
            }

            obj.AddComponent<ShipObject>();

            //オンライン時最大6船(プレイヤー2人+CPU6船)/オフライン時固定3船(プレイヤー1人+CPU3船)
            float CPUx = x;
            float CPUz = 0;
            if (SceneManager.GetActiveScene().name == "InGame")
            {
                if (PhotonNetwork.LocalPlayer.ActorNumber == 1) CPUFactory = 3;
                else CPUFactory = CPUFactory - PhotonNetwork.LocalPlayer.ActorNumber;
                if (PhotonNetwork.IsMasterClient) {
                    for (int i = 0; i < CPUFactory; i++)
                    {
                        CPUx += 6;//CPU生成時のX座標を6ずつずらす
                        CPUz += 5;
                        Vector3 CPUvec = new Vector3(CPUx + start.x, start.y, start.z + CPUz);
                        GameObject CPUobj = PhotonNetwork.Instantiate(CPUShipPrefabName, CPUvec, Quaternion.identity) as GameObject;
                        CPUobj.name = "CPUShip";
                        CPUobj.AddComponent<CPUShipObject>();
                        if (!CPUobj)
                        {
                            Debug.Log("CPU_" + i + "の生成に失敗しました");
                            return null;
                        }

                    }
                }
            }

            return obj;

        }

    }

}