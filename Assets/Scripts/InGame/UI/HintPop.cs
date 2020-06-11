using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using UnityEngine;

public class HintPop : MonoBehaviour
{
    
    // Hint表示までの時間
    public float HintEnableTime
    {
        get;
        private set;
    }

    // 表示するかのフラグ
    public bool IsHint
    {
        get;
        private set;
    }

    // 表示するオブジェクト
    [SerializeField]
    private GameObject HintUI;


    /// <summary>
    /// 初期値設定
    /// </summary>
    private void Awake()
    {

        // ファンファーレ時間を加味して多めに設定
        HintEnableTime = 23.7f;
        IsHint = false;

        HintUI.gameObject.SetActive(false);

    }

    private void FixedUpdate()
    {

        HintEnableTime -= Time.deltaTime;

        // ShipObjectクラスのIsHintを参照し代入
        IsHint = FindObjectOfType<Sailing.ShipObject>().HintEnable(IsHint);
        //Debug.Log(IsHint);

        if(HintEnableTime <= 0 && IsHint)
        {
            //Debug.Log("HintEnable!!");
            HintUI.gameObject.SetActive(true);
        }
        else if(HintEnableTime <= 0 && !IsHint)
        {
            HintEnableTime = 10.0f;
        }
        else
        {
            HintUI.gameObject.SetActive(false);
        }

    }



}
