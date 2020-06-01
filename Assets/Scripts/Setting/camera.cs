/*作成：小出萌々子
 * 着手日                                          5/18
 * 総項目完了日　
 * 
 * 行いたいこと
 * 1.画面サイズの取得                         完了日5/18
 * 2.各々のスマホ、プラットフォーム画面に合うサイズにする 完了日5/22
 * 3.UIが画面から見切れないようにする
 * 
 *      メモ
 *      //1.画面サイズの取得
        //ユーザーが見てる画面サイズの取得
        Debug.Log("Screen Width: " + Screen.width);     //横幅
        Debug.Log("Screen Height: " + Screen.height);   //縦幅
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    //画面のwidth
    public float defaultWidth = 9.0f;

    //画面のheight
    public float defaultHeight = 16.0f;

    void Start()
    {
        //camera.mainを変数に格納
        Camera mainCamera = Camera.main;

        //画面のアスペクト比 
        float defaultAspect = defaultWidth / defaultHeight;

        //実際の画面のアスペクト比
        float actualAspect = (float)Screen.width / (float)Screen.height;

        //実機とunity画面の比率
        float ratio = actualAspect / defaultAspect;

        //サイズ調整
        mainCamera.orthographicSize /= ratio;

    }
}