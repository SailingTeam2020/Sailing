using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceChange : MonoBehaviour
{
    float ReturnPrice;//設定された価格
    string NameRegistration;//押されたボタンの名前を格納する

    public float ChangPrice(string objname)
    {
        switch (objname)
        {
            case "MoneyButton":
                ReturnPrice = 200;
                break;
            case "BuyButtonSample":
                ReturnPrice = 150;
                break;
            case "BuyCourse2Button":
                ReturnPrice = 50;
                break;

            default:
                ReturnPrice = 0;
                break;

        }
        return ReturnPrice;
    }

    public void ObjectNameRegistrationUpdate(string objname)
    {
        NameRegistration = objname;
    }

    public string PostObjectNameRegistrationUpdate()
    {
        return NameRegistration;
    }
}
