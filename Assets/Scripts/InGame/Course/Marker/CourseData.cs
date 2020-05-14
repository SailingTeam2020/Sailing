using UnityEngine;
using System.Collections.Generic;

// NOTE: 使い方
/*  1.　CreateからScriptable/MakerTransfromDataを生成
 *  2.  サイズを決めて、Scene/StageEditerを開いて、再生
 *  3.  生成されたマークのTrancfromを編集して、生成したScriptableのInspectorでSaveを押す
*/

namespace Sailing
{

    [CreateAssetMenu(fileName = "", menuName = "ScriptableObject/MakerTransformData", order = 0)]
    public class CourseData : ScriptableObject
    {
        string courseName;
        public List<MakerTransform> makerTransformList = new List<MakerTransform>();
        public List<WindTransform> windTransformList = new List<WindTransform>();
    }

    [System.Serializable]
    public class MakerTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    [System.Serializable]
    public class WindTransform
    {
        public Vector3 position;
        public float speed;
    }

}