using UnityEngine;
using UnityEngine.UI;


// MEMO: SelectSceneで使用。Canvas内のUIをインスペクター上でアタッチしてください。
public class ChangeUIButton : MonoBehaviour
{
    [SerializeField] private GameObject MainUI = null;
    [SerializeField] private GameObject SettingUI = null;   // MEMO:ちょっとボタンの判定範囲が狭い

    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => ChangeViewUI());
    }

    private void ChangeViewUI()
    {
        MainUI.SetActive(!MainUI.activeSelf);
        SettingUI.SetActive(!SettingUI.activeSelf);
        PlayerPrefs.Save();
    }

}
