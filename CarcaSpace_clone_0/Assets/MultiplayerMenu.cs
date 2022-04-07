using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerMenu : MonoBehaviour
{
    private Button Solo;
    private Button JoinFriends;
    private Button addPlayers;
    private Button subPlayers;
    private Text text;
    private Mask mask;
    public void EnterMultiplayerMenu()
    {
         SceneManager.LoadScene("MultiplayerMenu", LoadSceneMode.Single);
    }

    public void EnterSoloMenu()
    {
        SceneManager.LoadScene("game interface",LoadSceneMode.Single);
    }

    public void EnterJoinFriendsMenu()
    {
        SceneManager.LoadScene("JFMenu",LoadSceneMode.Single);
    }
    public void addPlayer()
    {
        text = GameObject.Find("PlayerInput").GetComponent<Text>();
        mask = GameObject.Find("left-arrow").GetComponent<Mask>();

        
        int nbPlayers = int.Parse(text.text) + 1;
        if(nbPlayers == 2){
            mask.showMaskGraphic = true;
        }
        text.text = nbPlayers.ToString();
    }

    public void substractPlayer()
    {
        text = GameObject.Find("PlayerInput").GetComponent<Text>();
        mask = GameObject.Find("left-arrow").GetComponent<Mask>();

        int nbPlayers = int.Parse(text.text);

        if(nbPlayers > 1)
        {
            nbPlayers -= 1;
            if(nbPlayers == 1)
            {
                mask.showMaskGraphic = false;
            }
        }
        
        text.text = nbPlayers.ToString();
    }
    void Start() {
        Solo = GameObject.Find("Solo").GetComponent<Button>();
        JoinFriends = GameObject.Find("JoinFriends").GetComponent<Button>();
        addPlayers = GameObject.Find("right-arrow").GetComponent<Button>();
        subPlayers = GameObject.Find("left-arrow").GetComponent<Button>();

        Solo.onClick.AddListener(EnterSoloMenu);
        JoinFriends.onClick.AddListener(EnterJoinFriendsMenu);
        addPlayers.onClick.AddListener(addPlayer);
        subPlayers.onClick.AddListener(substractPlayer);
    }
}
