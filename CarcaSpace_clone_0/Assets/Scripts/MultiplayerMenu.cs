using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerMenu : MonoBehaviour
{
    private Button Create;
    private Button JoinFriends;
    
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
    
    void Start() {
        Create = GameObject.Find("CreateGame").GetComponent<Button>();
        JoinFriends = GameObject.Find("JoinFriends").GetComponent<Button>();
        // addPlayers = GameObject.Find("right-arrow").GetComponent<Button>();
        // subPlayers = GameObject.Find("left-arrow").GetComponent<Button>();

        Create.onClick.AddListener(EnterCreateMenu);
        JoinFriends.onClick.AddListener(EnterJoinFriendsMenu);
        // addPlayers.onClick.AddListener(addPlayer);
        // subPlayers.onClick.AddListener(substractPlayer);
    }
}
