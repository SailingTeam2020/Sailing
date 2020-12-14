using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
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
        private int MarkerValueRandom;//マーカーの生成箇所
        private int MarkerNumberRandom;//コースの長さ
        // Start is called before the first frame update
        void Start()
        {

            MakerFactory makerFactory = gameObject.AddComponent<MakerFactory>();
            CourseData course = GetComponent<CourseManager>().CourseData;
            List<MakerTransform> makerList = course.makerTransformList;
            int maxMakerNum = makerList.Count;

            //最小値が3つ最大はマーカー座標の登録数(制限をかけるかも)
            MarkerNumberRandom = UnityEngine.Random.Range(3, makerList.Count);
            //Debug.Log("マーカー数 " + MarkerNumberRandom);

            //Randomにシャッフルして、上からMarkerNumberRandom分取り出す。
            var RandomList = Enumerable.Range(0, makerList.Count).OrderBy(n => Guid.NewGuid()).Take(MarkerNumberRandom).ToArray();
               //マーカーの順番の確認用
            
                for (int s = 0; s < RandomList.Length; s++)
                {
                    //Debug.Log(RandomList[s]);
                }
             
                for (int i = 0; i < MarkerNumberRandom; i++)
            {

                GameObject obj;
                MakerNum++;
                bool isGoal = false;
                if(i == maxMakerNum - 1)
                {
                    obj = makerFactory.Create(true, makerList[RandomList[i]]);
                    
                    isGoal = true;
                }
                else
                {
                    obj = makerFactory.Create(false, makerList[RandomList[i]]);
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