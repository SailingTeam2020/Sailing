using UnityEngine;
using UnityEngine.UI;
using Common;


namespace Sailing
{
    public class ChangeTheScene_InGame : MonoBehaviour
    {
        //[SerializeField] private string str = null;

        private void Start()
        {

            //if(str == null) { Debug.LogError("未設定です。"); }
            GetComponent<Button>().onClick.AddListener(() => SceneSwitch(SceneNameString.InGame));

        }

        public void SceneSwitch(string str)
        {
            FadeManager.FadeOut(str);
        }

    }

}