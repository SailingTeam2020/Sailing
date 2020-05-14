using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing
{

	public class TimerUI : MonoBehaviour
	{

		[SerializeField]
		private GameTimer gameTimer;

		private Text timeText;

		// Start is called before the first frame update
		private void Awake()
		{

			timeText = GetComponent<Text>();
		}

		// Update is called once per frame
		private void Update()
		{

			timeText.text = TimeTextTransport(gameTimer.GameTime);

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