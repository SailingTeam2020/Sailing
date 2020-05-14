using UnityEngine;
using UnityEngine.UI;

namespace Sailing.Online
{

    public class MatchingTimerUI : MonoBehaviour
    {

        [SerializeField]
        private MatchingCountdown countTimer;

        private Text timerText;

        // Start is called before the first frame update
        private void Start()
        {

            timerText = GetComponent<Text>();

        }

        // Update is called once per frame
        private void Update()
        {

            //小数点を切り上げて数値合わせしている
            timerText.text = Mathf.Ceil(countTimer.LeftCountTime).ToString("00");

        }

    }

}