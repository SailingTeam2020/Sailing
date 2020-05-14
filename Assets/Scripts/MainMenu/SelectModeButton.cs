using UnityEngine;
using Common;
using Photon.Pun;

namespace Sailing
{

    public class SelectModeButton : MonoBehaviour
    {

        [SerializeField]
        private CourseData courseData = null; // 読み込むコースデータ

        public void OfflineSceneSwitch()
        {

            if (!courseData)
            {
                courseData = (CourseData)Resources.Load("Scriptable/SoloPlayData");
            }

            PlayCorseData.CourseData = courseData;

            PhotonNetwork.OfflineMode = true;
            PhotonNetwork.JoinRoom("Offline Room");
            Debug.Log("オフラインモードで開始");
            FadeManager.FadeOut(SceneNameString.InGame);
        
        }

        public void OnlineSceneSwitch()
        {

            PhotonNetwork.OfflineMode = false;
            Debug.Log("オンラインモードで開始");
            FadeManager.FadeOut(SceneNameString.Lobby);

        }

    }

}