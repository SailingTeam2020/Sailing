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
        Debug.Log("(・。・)");
        kickMenu.SetActive(true);
        roomOutButton.SetActive(false);
        playerKickButton.SetActive(false);
        if(this.gameObject.tag == "Player2")
        {
            
            Player = "Player2";
            Debug.Log(Player);
            //Debug.Log("Player2!!!!");
        }
    }

    public string returnPlayer()
    {
        return Player;
    }

}
