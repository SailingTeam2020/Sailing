using Photon.Pun;
using UnityEngine;

namespace Sailing
{

    public class ShipFactory : MonoBehaviour
    {

        private const string ShipPrefabName = "Ship";

        public GameObject Create()
        {

            //生成位置をファクトリーに書くのはよくないから直す予定
            Vector3 start = Vector3.zero;
            float x = 3.0f * PhotonNetwork.LocalPlayer.ActorNumber;
            Vector3 vec = new Vector3(x + start.x, start.y, start.z);

            GameObject obj = PhotonNetwork.Instantiate(ShipPrefabName, vec, Quaternion.identity);

            if (!obj)
            {
                Debug.Log("Playerの生成に失敗しました");
                return null;
            }

            obj.AddComponent<ShipObject>();

            return obj;

        }

    }

}