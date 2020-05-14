using UnityEngine;
using UnityEngine.UI;

// NOTE:タブの切り替え部。ひどい。それに尽きる。

public class ChangeTab : MonoBehaviour
{
	[SerializeField] private GameObject gametabswichbutton = null;
	[SerializeField] private GameObject gametab = null;
	[SerializeField] private GameObject usertabswichbutton = null;
	[SerializeField] private GameObject usertab = null;

	void Start()
	{
		gametabswichbutton.GetComponent<Button>().onClick.AddListener(() => GameTabSwith());
		usertabswichbutton.GetComponent<Button>().onClick.AddListener(() => UserTabSwith());
	}

	private void GameTabSwith()
	{
		gametab.transform.SetAsFirstSibling();
	}
	private void UserTabSwith()
	{
		usertab.transform.SetAsFirstSibling();
	}
}
