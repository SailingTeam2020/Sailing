using UnityEngine;

namespace Sailing
{

    public class MakerFactory : MonoBehaviour
    {

        private GameObject makerPrefab;
        private GameObject goalMakerPrefab;

        private void Awake()
        {

            makerPrefab = (GameObject)Resources.Load("Prefabs/Maker");
            goalMakerPrefab = (GameObject)Resources.Load("Prefabs/GoalMaker");

            if (!makerPrefab) { 
                Debug.Log("MakerPrefabが読み込めません"); 
            }
            if (!goalMakerPrefab) { 
                Debug.Log("GoalmakerPrefabが読み込めません"); 
            }

        }

        public GameObject Create(bool isLast, MakerTransform makerTransform)
        {

            GameObject obj;

            if (isLast)
            {
                obj = CreateGoalMaker(makerTransform);
            }
            else
            {
                obj = CreateMaker(makerTransform);
            }
            
            obj.AddComponent<MakerObject>();

            return obj;
        }

        private GameObject CreateMaker(MakerTransform makerTransform)
        {

            return Instantiate(makerPrefab, makerTransform.position, makerTransform.rotation);
        }

        private GameObject CreateGoalMaker(MakerTransform makerTransform)
        {

            return Instantiate(goalMakerPrefab, makerTransform.position, makerTransform.rotation);
        }

    }

}