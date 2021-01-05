/*
 *作成者：小林凱
 *更新日：01/04
 *更新者：小林凱
 *概要　：生成されたオブジェクトを動かす処理
 *外部変数
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEffectSource : MonoBehaviour
{    

    public float WindDirection
    {
        get;
        private set;
    }
    
    public float WindSpeed
    {
        get;
        private set;
    }

    public bool DestroyFlg
    {
        get;
        private set;
    }

    private void Awake()
    {
        
        WindDirection = FindObjectOfType<WindEffect>().SetAng();
        WindSpeed = 4.0f;
        DestroyFlg = false;

    }

    /// <summary>
    /// 取得した角度方向へオブジェクトを動かす。
    /// </summary>
    void FixedUpdate()
    {
        // オブジェクトを動かす
        this.gameObject.transform.Translate((WindDirection * WindSpeed), 0, (WindDirection * WindSpeed));
        //Debug.Log(WindDirection);

        // オブジェクトがz-100を超えた場合デストロイフラグを正にする。
        if(transform.position.z < -100)
        {
            DestroyFlg = true;
            Destroy(this.gameObject);
        }

    }

    /// <summary>
    /// フラグを返す
    /// </summary>
    /// <returns>このオブジェクトを削除可能か</returns>
    public int ReturnFlg()
    {
        //Debug.Log(DestroyFlg);
        if (DestroyFlg)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

}
