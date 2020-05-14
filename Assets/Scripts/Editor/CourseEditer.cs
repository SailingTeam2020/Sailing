using UnityEngine;
using UnityEditor;
using Sailing.SingletonObject;

namespace Sailing
{

    [CanEditMultipleObjects]
    [CustomEditor(typeof(CourseData))]
    public class DataStageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (!EditorApplication.isPlaying) return;   // エディタプレビュー中以外は作動させない

            var transformData = target as CourseData;

            GameObject course = GameObject.Find("CourseManager");
            MakerManager makerManager = course.GetComponent<MakerManager>();
            WindManager windManager = course.GetComponent<WindManager>();

            if (GUILayout.Button("Save"))
            {
                int n = makerManager.MakerNum;
                for (int i = 1; i <= n; i++)
                {
                    transformData.makerTransformList[i - 1].position = makerManager.MakerObjectList[i].transform.position;
                    transformData.makerTransformList[i - 1].rotation = makerManager.MakerObjectList[i].transform.rotation;
                }
                n = windManager.WindNum;
                for (int i = 1; i <= n; i++)
                {
                    transformData.windTransformList[i - 1].position = windManager.WindObjectList[i].transform.position;
                    transformData.windTransformList[i - 1].speed = windManager.WindObjectList[i].GetComponent<WindObject>().WindSpeed;
                }
            }
        }
    }

}