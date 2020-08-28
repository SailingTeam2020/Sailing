using Sailing.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Sailing.SingletonObject;

namespace Sailing
{

	public class JudgeUI : MonoBehaviour
	{

		[SerializeField]
		private ShipManager shipManager;
		[SerializeField]
		private Sprite[] judgeImage;
		[SerializeField]
		private GameObject judgeUI = null;
		private Coroutine coroutine = null;
		[SerializeField]
		private float windInfluence = 0.0f;
		private bool isStartSkip = true;        // 一番最初の表示処理を飛ばす

		public StateMachine<JudgeState.Condition> condition {
			get;
			private set;
		}

		private void Awake()
		{

			condition = new StateMachine<JudgeState.Condition>();
			condition.changeStateEvent = (x) =>
			{
				if (isStartSkip)
				{
					isStartSkip = false;
					return;
				}

				if (judgeImage[(int)x] == null) {
					Debug.LogError(x + "には画像が割り当てられていません。");
				}
				else
				{
					judgeUI.GetComponent<Image>().sprite = judgeImage[(int)x];
					switch (x)
					{
						case JudgeState.Condition.Poor:
							SoundManager.Instance.PlaySE("Poor");
							break;
						case JudgeState.Condition.Good:
							SoundManager.Instance.PlaySE("Good");
							break;
						case JudgeState.Condition.Great:
							SoundManager.Instance.PlaySE("Great");
							break;
						case JudgeState.Condition.Excellent:
							SoundManager.Instance.PlaySE("Excellent");
							break;
					}

					judgeUI.SetActive(true);
					Debug.Log(x);
					if (coroutine != null)
					{
						StopCoroutine(coroutine);
						coroutine = null;
					}
					coroutine = StartCoroutine(IntarvalImage(1.5f));
				}
			};

		}
		private IEnumerator IntarvalImage(float interval)
		{

			yield return new WaitForSeconds(interval);

			judgeUI.SetActive(false);

		}

		void Update()
		{
            //速度メーターの値
            // WindInfluence.UI 内の windSlider.value に値を代入
            windInfluence = shipManager.MainShipObject.ShipMove.WindInfluence * 100;

			//0 ~45
			if (windInfluence <= 25.0f)
			{
				condition.ChangeState(JudgeState.Condition.Poor);
			}
			//80 ~100
			else if (40 < windInfluence && windInfluence <= 65)
			{
				condition.ChangeState(JudgeState.Condition.Good);
			}
			//165 ~180
			else if (65 < windInfluence && windInfluence <= 95)
			{
				condition.ChangeState(JudgeState.Condition.Great);
			}
			else if (95 < windInfluence)
			{
				condition.ChangeState(JudgeState.Condition.Excellent);
			}

		}
	}

}