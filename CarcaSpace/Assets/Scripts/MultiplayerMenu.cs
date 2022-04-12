using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror ; 

public class MultiplayerMenu : NetworkBehaviour
{

    public static MultiplayerMenu instance ; 
    private Button Create;
    private Button JoinFriends;
    
    private Text text;
    private Mask mask;

    [SerializeField] InputField joinMatchInput ; 
    [SerializeField] InputField playerNumberInput ; 
    [SerializeField] Button joinButton ;
    [SerializeField] Button hostButton ;

    [SerializeField]  Canvas lobbyCanvas ; 


    void Start() {
        instance = this ; 
    }
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

    public void Host(string _matchId,GameObject _player)
    {
        joinMatchInput.interactable = false ;
        joinButton.interactable = false ; 
        hostButton.interactable = false ;  
    }

    //pour reactiver les boutons si echec
    public void hostSucc(bool success){
        if(success){
            SceneManager.LoadScene("Lobby");
        }else{
            joinMatchInput.interactable = true ;
            joinButton.interactable = true ; 
            hostButton.interactable = true ;  
        }
    }

    public void Join(){
        joinMatchInput.interactable = false ;
        joinButton.interactable = false ; 
        hostButton.interactable = false ;  
    }

    //pour reactiver les boutons si echec
    public void joinSucc(bool success){
        if(success){

        }else{
            joinMatchInput.interactable = true ;
            joinButton.interactable = true ; 
            hostButton.interactable = true ;  
        }
    }
    
    
}
