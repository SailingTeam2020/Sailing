using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriceChange : MonoBehaviour
{
    float ReturnPrice;

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
            default:
                ReturnPrice = 0;
                break;

        }
        return ReturnPrice;
    }
}
