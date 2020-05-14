using System.Collections;
using UnityEngine;
using Common;
using Sailing.SingletonObject;

namespace Sailing
{

	public class TitleManager : MonoBehaviour
	{

		private bool flg = true;

		private void Start()
		{
			flg = true;
			SoundManager.Instance.PlayBGM("Water");
			Debug.Log(IDCreater.IDOutPut());
		}

		private void Update()
		{
			if (Input.GetMouseButtonDown(0) && flg)
			{
				flg = false;
				StartCoroutine("ChangeScene");
			}
		}

		IEnumerator ChangeScene()
		{

			SoundManager.Instance.PlaySE("StartTap");

			yield return new WaitUntil(() => !SoundManager.Instance.CheckPlaySE());
			FadeManager.FadeOut("MainMenu");

			yield break;
		}

	}

}