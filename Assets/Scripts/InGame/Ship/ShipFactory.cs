using Photon.Pun;
using UnityEngine;

namespace Sailing
{

    public class ShipFactory : MonoBehaviour
    {

        private const string ShipPrefabName = "Ship";
        private const string CPUShipPrefabName = "CpuShip";

        public GameObject Create()
        {

            //生成位置をファクトリーに書くのはよくないから直す予定
            Vector3 start = Vector3.zero;
            float x = 3.0f * PhotonNetwork.LocalPlayer.ActorNumber;
            Vector3 vec = new Vector3(x + start.x, start.y, start.z);

            GameObject obj = PhotonNetwork.Instantiate(ShipPrefabName, vec, Quaternion.identity) as GameObject;
            obj.name = "Ship";

            GameObject obj2 = PhotonNetwork.Instantiate(CPUShipPrefabName, vec, Quaternion.identity) as GameObject;
            obj2.name = "CPUShip";

            
            if (!obj)
            {
                Debug.Log("Playerの生成に失敗しました");
                return null;
            }

            obj.AddComponent<ShipObject>();

            if (!obj2)
            {
                Debug.Log("CPUの生成に失敗しました");
                return null;
            }
            obj2.AddComponent<CPUShipObject>();
            

            return obj;

        }

    }

}