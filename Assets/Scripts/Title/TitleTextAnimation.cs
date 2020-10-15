/***********************************************************************/
/*! @file   TitleTextAnimation.cs
*************************************************************************
*   @brief  タイトルのアニメーションを制御するスクリプト
*************************************************************************
*   @author yuta takatsu
*************************************************************************
*   Copyright © 2017 yuta takatsu All Rights Reserved.
************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleTextAnimation : MonoBehaviour
{

    [SerializeField]
    private Image me;                // @brief 画像登録
    private Vector3 movedPos;        // @brief 座標用

    [SerializeField]
    private float durationSecondes;  // @brief 点滅の周期時間
    private Ease easeType;           // @brief SetEaseのEasingを指定

    [SerializeField]
    private eAnimeType animeType;    // @brief enum判断用

    private CanvasGroup canvasGroup; // @brief 子要素含め扱える

    /// <summary>
    /// @brief どの画像かを判断する用
    /// </summary>
    public enum eAnimeType
    {
        eTitle_Wind,
        eTitle_Raser,
        eTitle_Text
    }

    /// <summary>
    /// @brief 初期座標をセット Typeに応じて最終座標も代入 
    /// </summary>
    protected void Awake()
    {

        if (animeType == eAnimeType.eTitle_Wind)
        {
            movedPos.x = 430.0f;
            me.rectTransform.localPosition = new Vector3(-2000.0f, 200.0f, 0.0f);
        }
        if (animeType == eAnimeType.eTitle_Raser)
        {
            movedPos.x = -350.0f;
            me.rectTransform.localPosition = new Vector3(2000.0f, 0.0f, 0.0f);
        }
    }

    /// <summary> 
    /// movedPosの位置に2.0fで移動 
    /// </summary>
    void Start()
    {
        // テキストの点滅
        if (animeType == eAnimeType.eTitle_Text)
        {
            DOVirtual.DelayedCall(2.5f, () => Text());
        }
        // タイトルロゴのアニメーション
        else
        {
            TitleLog();
        }
    }

    public void Text()
    {    
        this.canvasGroup = this.GetComponent<CanvasGroup>();
        this.canvasGroup.DOFade(1.0f, this.durationSecondes).SetEase(this.easeType).SetLoops(-1, LoopType.Yoyo);
    }

    public void TitleLog()
    {
        me.rectTransform.DOAnchorPosX(movedPos.x, 2.0f).SetEase(Ease.InOutBack);
    }
}