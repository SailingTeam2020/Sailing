using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class PlayerKick : MonoBehaviour
{

    [SerializeField]
    GameObject kickMenu;
    [SerializeField]
    GameObject kickButton;
    [SerializeField]
    GameObject roomOutButton;
    [SerializeField]
    GameObject OnlineManager;

    Player[] kickPlayer = new Player[7];

    string Player;

    private void Awake()
    {
        Player = null;
        if (!PhotonNetwork.IsMasterClient)
        {
            this.gameObject.SetActive(false);
        }
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            kickPlayer = FindObjectOfType<Sailing.Online.MatchingManager>().kickPlayer;
        }
    }

    public void OnClick()
    {
        Player = FindObjectOfType<KickButton>().returnPlayer();
        //Player = FindObjectOfType<KickButton>().returnPlayer();
        Debug.Log(Player);
        kickButton.SetActive(true);
        roomOutButton.SetActive(true);
        kickMenu.SetActive(false);
        if (Player == "Player2")
        {
            if (PhotonNetwork.IsMasterClient)
            {
                if (kickPlayer[0] == null)
                {
                    return;
                }
                PhotonNetwork.CloseConnection(kickPlayer[0]);
                
            }
        }
        else
        {
            if (PhotonNetwork.IsMasterClient)
            {
                for (int i = 0; i < 7; i++)
                {
                    if (kickPlayer[i] == null)
                    {
                        return;
                    }
                    PhotonNetwork.CloseConnection(kickPlayer[i]);
                }
            }
        }
    }

}
