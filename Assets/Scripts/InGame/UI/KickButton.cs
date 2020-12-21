using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickButton : MonoBehaviour
{

    [SerializeField]
    GameObject kickMenu;
    [SerializeField]
    GameObject roomOutButton;
    [SerializeField]
    GameObject playerKickButton;

    public static string Player
    {
        get;
        private set;
    }

    private void Awake()
    {
        Player = null;
    }

    public void OnClick()
    {
        //Debug.Log("(・。・)");
        kickMenu.SetActive(true);
        roomOutButton.SetActive(false);
        playerKickButton.SetActive(false);
        if (this.gameObject.tag == "Player2")
        {

            Player = "Player2";
            //Debug.Log(Player);
            //Debug.Log("Player2!!!!");
        }
        else if (this.gameObject.tag == "Player3")
        {
            Player = "Player3";
        }
        else if (this.gameObject.tag == "Player4")
        {
            Player = "Player4";
        }
        else if (this.gameObject.tag == "Player5")
        {
            Player = "Player5";
        }
        else if (this.gameObject.tag == "Player6")
        {
            Player = "Player6";
        }
        else if (this.gameObject.tag == "Player7")
        {
            Player = "Player7";
        }
        else if(this.gameObject.tag == "Player8")
        {
            Player = "Player8";
        }
        else
        {
            Player = "AllPlayer";
        }
    }

    public string returnPlayer()
    {
        return Player;
    }

}
