using UnityEngine;

namespace Sailing
{

    public class WindFactory : MonoBehaviour
    {

        public GameObject Create()
        {

            GameObject obj = new GameObject();

            obj.AddComponent<WindObject>();

            return obj;
        }

    }

}