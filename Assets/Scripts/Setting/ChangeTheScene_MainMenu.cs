using UnityEngine;
using UnityEngine.UI;
using Common;


namespace Sailing
{
    public class ChangeTheScene_MainMenu : MonoBehaviour
    {
        //[SerializeField] private string str = null;

        private void Start()
        {

            //if(str == null) { Debug.LogError("未設定です。"); }
            GetComponent<Button>().onClick.AddListener(() => SceneSwitchMainMenu(SceneNameString.MainMenu));

        }

        public void SceneSwitchMainMenu(string str)
        {
            FadeManager.FadeOut(str);
        }

    }

}