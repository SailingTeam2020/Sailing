using System.Collections.Generic;
using UnityEngine;

namespace Sailing
{

    public class MakerManager : MonoBehaviour
    {

        public int MakerNum {
            get;
            private set;
        }

        public Dictionary<int, MakerObject> MakerObjectList { 
            get;
            private set;
        }

        private void Awake()
        {

            MakerNum = 0;
            MakerObjectList = new Dictionary<int, MakerObject>();

        }

        // Start is called before the first frame update
        void Start()
        {

            MakerFactory makerFactory = gameObject.AddComponent<MakerFactory>();
            CourseData course = GetComponent<CourseManager>().CourseData;
            int maxMakerNum = course.makerTransformList.Count;
            for (int i = 0; i < maxMakerNum; i++)
            {

                GameObject obj;

                MakerNum++;
                bool isGoal = false;
                if(i == maxMakerNum - 1)
                {
                    obj = makerFactory.Create(true, course.makerTransformList[i]);
                    
                    isGoal = true;
                }
                else
                {
                    obj = makerFactory.Create(false, course.makerTransformList[i]);
                }

                MakerObject maker = obj.GetComponent<MakerObject>();

                obj.name = "Maker_" + MakerNum;
                obj.transform.parent = transform;

                maker.IsGoalMaker = isGoal;
                maker.MakerNumber = MakerNum;

                MakerObjectList.Add(MakerNum, obj.GetComponent<MakerObject>());
            }

        }

        public bool PassMaker(int nextMakerNum, GameObject obj)
        {

            int maker = obj.GetComponent<MakerObject>().MakerNumber;

            if (nextMakerNum != maker)
            {
                Debug.Log("パス条件を満たしていません");
                return false;
            }

            return true;
        }

    }

}