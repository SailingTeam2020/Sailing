using UnityEngine;

namespace Sailing
{

    public class CourseManager : MonoBehaviour
    {

        public CourseData CourseData {
            get;
            set;
        }

        public MakerManager MakerManager {
            get;
            private set;
        }

        public WindManager WindManager {
            get;
            private set;
        }

        private void Awake()
        {

            MakerManager = gameObject.AddComponent<MakerManager>();
            WindManager = gameObject.AddComponent<WindManager>();

            CourseData = PlayCorseData.CourseData;

            if (!CourseData)
            {
                CourseData = (CourseData)Resources.Load("Scriptable/SoloPlayData");
                Debug.Log("コースデータが存在しないため追加します");
                return;
            }

        }

    }

}