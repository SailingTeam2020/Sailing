/*スクリプト名     ：UserDataDelete.cs
 * 作成者          ：あだちたくみ
 * 作成日          ：2020/12/17
 * 概要            ：Userのデータを消去する。ただし、音量のデータは残す。
 */

using UnityEngine;
using UnityEngine.UI;
using Sailing.SingletonObject;
using Common;
using Sailing.Server;

public class UserDataDelete : SingletonMonoBehaviour<UserDataDelete>
{
    [SerializeField] private GameObject bgmVolumeSlider = null;
    [SerializeField] private GameObject seVolumeSlider = null;

    private void OnApplicationQuit()
    {
        Debug.Log("ゲームが終了されました。");

        Debug.Log("PlayerPrefsのデータを削除します。");

        if (PlayerPresDateDelete())
        {
            Debug.Log("データの削除が完了しました。");
        }
        else
        {
            Debug.Log("データが削除できませんでした。");
        }

    }

    public bool PlayerPresDateDelete()
    {
        //PlayerPrefs.DeleteAll();
        /*PlayerPrefs.DeleteKey(UserDataKey.UserID_Key);
        Debug.Log(UserDataKey.UserID_Key + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserID_Default);
        Debug.Log(UserDataKey.UserID_Default + "削除");*/

        PlayerPrefs.DeleteKey(UserDataKey.UserName_Key);
        Debug.Log(UserDataKey.UserName_Key+"削除");
        
        PlayerPrefs.DeleteKey(UserDataKey.UserName_Default);
        Debug.Log(UserDataKey.UserName_Default + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserPref_Key);
        Debug.Log(UserDataKey.UserPref_Key + "削除");
        
        PlayerPrefs.DeleteKey(UserDataKey.UserPref_Default.ToString());
        Debug.Log(UserDataKey.UserPref_Default + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserBirth_Key);
        Debug.Log(UserDataKey.UserBirth_Key + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserBirth_Default);
        Debug.Log(UserDataKey.UserBirth_Default + "削除");

        
        if (PlayerPrefs.HasKey(UserDataKey.UserName_Key)) return false;
        else return true;
    }

    public bool PlayerPresDateDeleteAll()
    {
        PlayerPrefs.DeleteKey(UserDataKey.UserID_Key);
        Debug.Log(UserDataKey.UserID_Key + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserID_Default);
        Debug.Log(UserDataKey.UserID_Default + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserName_Key);
        Debug.Log(UserDataKey.UserName_Key + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserName_Default);
        Debug.Log(UserDataKey.UserName_Default + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserPref_Key);
        Debug.Log(UserDataKey.UserPref_Key + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserPref_Default.ToString());
        Debug.Log(UserDataKey.UserPref_Default + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserBirth_Key);
        Debug.Log(UserDataKey.UserBirth_Key + "削除");

        PlayerPrefs.DeleteKey(UserDataKey.UserBirth_Default);
        Debug.Log(UserDataKey.UserBirth_Default + "削除");


        if (PlayerPrefs.HasKey(UserDataKey.UserName_Key)) return false;
        else return true;
    }
}
