using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerMenu : MonoBehaviour
{
    private Button Create;
    private Button JoinFriends;
    private Button addPlayers;
    private Button subPlayers;
    private Text text;
    private Mask mask;
    public void EnterMultiplayerMenu()
    {
         SceneManager.LoadScene("MultiplayerMenu", LoadSceneMode.Single);
    }

    public void EnterCreateMenu()
    {
        SceneManager.LoadScene("CreatingGame",LoadSceneMode.Single);
    }

    public void EnterJoinFriendsMenu()
    {
        SceneManager.LoadScene("JoiningMenu",LoadSceneMode.Single);
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
        Create = GameObject.Find("Create Game").GetComponent<Button>();
        JoinFriends = GameObject.Find("JoinFriends").GetComponent<Button>();
        
        Create.onClick.AddListener(EnterCreateMenu);
        JoinFriends.onClick.AddListener(EnterJoinFriendsMenu);
    }
}
