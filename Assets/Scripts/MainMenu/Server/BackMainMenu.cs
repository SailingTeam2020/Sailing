using UnityEngine;
using UnityEngine.UI;
using Common;

namespace Sailing.Server
{

    public class BackMainMenu : MonoBehaviour
    {

        private void Awake()
        {

            gameObject.GetComponent<Button>().onClick.AddListener(() => FadeManager.FadeOut(SceneNameString.MainMenu));

        }

        public void SecenSwitch()
        {

            FadeManager.FadeOut(SceneNameString.MainMenu);

        }

    }

}