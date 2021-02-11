using Common;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Sailing.Server
{

    public class HaveMoney : MonoBehaviour
    {

        //private GameObject score_object = null; // Textオブジェクト

        [SerializeField]
        private Text score_num; // スコア変数

        // 初期化時の処理
        void Start()
        {
            // スコアのロード
            //score_num = PlayerPrefs.GetInt("SCORE", 1000);
            score_num = ObjectFind.ChildFind("HaveMoney", ObjectFind.ChildFind("Money", transform)).gameObject.GetComponent<Text>();

            Debug.Log(score_num.text);

            GetMoney();
        }

        void GetMoney()
        {
            // オブジェクトからTextコンポーネントを取得
            score_num.text = UserDataKey.UserMoney_Default;
            //score_num.text = "1000";
            Debug.Log("初期金額" + score_num.text);
        }
    }
}
