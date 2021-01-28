/*スクリプト名：UserHelpText.cs
 *作成者：足立拓海
 *作成日：2021/01/28
 *概要：ログインやログアウトで失敗した際にPlayerに対して通知をする為のスクリプト。
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserHelpText : MonoBehaviour
{
    public string userHelpText
    {
        get;
        set;
    }

    public float HelpTextLifeTime
    {
        get;
        set;
    }

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HelpTextLifeTime >= 0) HelpTextLifeTime -= Time.deltaTime;
        if (HelpTextLifeTime < 0) userHelpText = "";

        text.text = userHelpText;
    }
}
