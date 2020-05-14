using UnityEngine;

public class HelpText : MonoBehaviour
{
    GameObject ImageObject;
    /*
    void OnGUI()
    {
        // テキストフィールドを表示する
        GUI.TextField(new Rect(125, 50, 500, 300), "");
        
        // ボタンを表示する
        if (GUI.Button(new Rect(30, 60, 60, 30), "←"))
        {
            //Debug.Log("戻る");
            PageNum -= 1;
            if(PageNum < 1)
            {
                PageNum = 5;
            }
        }
        if (GUI.Button(new Rect(660, 60, 60, 30), "→"))
        {
            //Debug.Log("進む");
            PageNum += 1;
            if (PageNum > 5)
            {
                PageNum = 1;
            }
        }

        switch (PageNum)
        {
            // テキスト表示
            case 1:
                Debug.Log("ページ1");
                GUI.Label(new Rect(140, 60, 480, 280), "1");
                break;

            case 2:
                Debug.Log("ページ2");
                GUI.Label(new Rect(140, 60, 480, 280), "2");
                break;

            case 3:
                Debug.Log("ページ3");
                GUI.Label(new Rect(140, 60, 480, 280), "3");
                break;

            case 4:
                Debug.Log("ページ4");
                GUI.Label(new Rect(140, 60, 480, 280), "4");
                break;

            case 5:
                Debug.Log("ページ5");
                GUI.Label(new Rect(140, 60, 480, 280), "5");
                break;

        }
    }
    */
    void Start()
    {
        // 任意のオブジェクトを取得する
        ImageObject = GameObject.Find("Image");
    }
    public void Next()
    {
        // 他のスクリプトのPageUpメソッドを使用
        ImageObject.GetComponent<ChangeImage>().PageUp();    
    }
    public void Back()
    {
        // 他のスクリプトのPageUpメソッドを使用
        ImageObject.GetComponent<ChangeImage>().PageDown();
    }

}
