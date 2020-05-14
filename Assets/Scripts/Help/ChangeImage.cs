using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Sprite Image01;
    public Sprite Image02;
    public Sprite Image03;
    public Sprite Image04;
    public Sprite Image05;

    public int PageNum = 1;

    public void PageUp()
    {
        Debug.Log("進む:" + PageNum);
        PageNum += 1;
        if (PageNum > 5)
        {
            PageNum = 1;
        }
        ChangeSprite();
    }
    public void PageDown()
    {
        Debug.Log("戻る:" + PageNum); 
        PageNum -= 1;
        if (PageNum < 1)
        {
            PageNum = 5;
        }
        ChangeSprite();
    }

    public void ChangeSprite()
    {
        if (PageNum == 1)
        {
            Debug.Log("Page.01");
            this.gameObject.GetComponent<Image>().sprite = Image01;
        }
        if (PageNum == 2)
        {
            Debug.Log("Page.02");
            this.gameObject.GetComponent<Image>().sprite = Image02;
        }
        if (PageNum == 3)
        {
            Debug.Log("Page.03");
            this.gameObject.GetComponent<Image>().sprite = Image03;
        }
        if (PageNum == 4)
        {
            Debug.Log("Page.04");
            this.gameObject.GetComponent<Image>().sprite = Image04;
        }
        if (PageNum == 5)
        {
            Debug.Log("Page.05");
            this.gameObject.GetComponent<Image>().sprite = Image05;
        } 
    }
}
