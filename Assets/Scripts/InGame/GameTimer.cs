/*
 * 
 * 長嶋
 * 
 */

using Sailing.SingletonObject;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Sailing
{

	public class GameTimer : MonoBehaviour
	{

		[SerializeField]
		private GameObject countdownUI;
		[SerializeField]
		private List<Sprite> numberSprite;

		private SoundManager soundManager;
		private Image countdownImage;

		public bool IsCountStart {
			get;
			set;
		}

		public float GameTime {
			get;
			private set;
		}

		private void Awake()
		{

			soundManager = SoundManager.Instance;
			countdownImage = countdownUI.GetComponentInChildren<Image>();
			GameTime = 0.0f;
			IsCountStart = false;

		}

		public void Update()
		{

			if (!IsCountStart)
			{
				return;
			}

			GameTime += Time.deltaTime;

		}

		public void Initialize()
		{

			StartCoroutine("StartFanfare");

		}

		IEnumerator StartFanfare()
		{

			soundManager.PlaySE("Fanfare");
			yield return new WaitUntil(() => !soundManager.CheckPlaySE());

			countdownUI.SetActive(true);
			countdownImage.sprite = numberSprite[3];
			soundManager.PlaySE("Countdown");
			yield return new WaitForSeconds(1.0f);

			countdownImage.sprite = numberSprite[2];
			soundManager.PlaySE("Countdown");
			yield return new WaitForSeconds(1.0f);

			countdownImage.sprite = numberSprite[1];
			SoundManager.Instance.PlaySE("Countdown");
			yield return new WaitForSeconds(1.0f);

			countdownUI.SetActive(false);
			SoundManager.Instance.PlaySE("Startcall");
			IsCountStart = true;

			yield break;
		}

		public void TimerStop()
		{

			Debug.Log("タイマーストップ：" + TimeTextTransport(GameTime));
			IsCountStart = false;

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