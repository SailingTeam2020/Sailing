using Common;
using Sailing.SingletonObject;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing
{

    public class ResultMenuUI : MonoBehaviour
    {

        [SerializeField]
        private ShipManager shipManager;
        [SerializeField]
        private GameTimer gameTimer;
        [SerializeField]
        private Transform goalUI;
        [SerializeField]
        private Transform resultUI;
        [SerializeField]
        private Text timeUI;

        private bool isShowResult;

        private void Awake()
        {

            isShowResult = false;

        }

        // Start is called before the first frame update
        private void Start()
        {

            goalUI = ObjectFind.ChildFind("GoalUI", transform);
            goalUI.gameObject.SetActive(false);
            resultUI = ObjectFind.ChildFind("ResultUI", transform);
            resultUI.gameObject.SetActive(false);

        }

        // Update is called once per frame
        private void Update()
        {

            if (!isShowResult && shipManager.MainShipObject.IsGoal) {
                isShowResult = true;
                goalUI.gameObject.SetActive(true);
                StartCoroutine(ViewResult()); 
            }

        }

        IEnumerator ViewResult()
        {

            yield return new WaitUntil(() => !SoundManager.Instance.CheckPlaySE());

            Server.RegisterTimeRecode register = gameObject.AddComponent<Server.RegisterTimeRecode>();
            register.RecodeTime = gameTimer.GameTime;
            //登録完了か失敗を待つ
            yield return StartCoroutine(register.Push());

            goalUI.gameObject.SetActive(false);
            resultUI.gameObject.SetActive(true);
            timeUI.text = TimeTextTransport(gameTimer.GameTime);

            yield break;
        }

        string TimeTextTransport(float time)
        {

            int m = (int)(time / 60);
            int s = (int)(time % 60);
            int ss = (int)((time - Mathf.Floor(time)) * 100);

            StringBuilder sb = new StringBuilder();

            sb.Append(m.ToString("D2"));
            sb.Append(":");
            sb.Append(s.ToString("D2"));
            sb.Append(":");
            sb.Append(ss.ToString("D2"));

            return sb.ToString();
        }

    }

}