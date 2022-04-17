using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror ; 

public class MultiplayerMenu : MonoBehaviour
{

    public static MultiplayerMenu instance ; 

  
    private Button Create;
    private Button JoinFriends;
    
    private Text text;
    [SerializeField] Button addPlayers;
    [SerializeField] Button subPlayers;
    // private Mask mask1;
    // private Mask mask2;
    // ou l'on rentre le code de la partie 
    [SerializeField] InputField joinMatchInput ; 

    //ou l'on rentre le nombre de joueurs
    [SerializeField] InputField playerNumberInput ; 
    [SerializeField] Button joinButton ;
    [SerializeField] Button hostButton ;

    //Bouton ready pour les joeuurs non implemente encore 
    [SerializeField] Button readyButton ; 

    [SerializeField]  Canvas CanvasCreating  ; 
    [SerializeField]  Canvas CanvasJoining  ; 
    [SerializeField]  Canvas CanvasLobby  ;

    [SerializeField]  Canvas CanvasMain ;

    
    

    
    //public PlayerManager Player ; 
    public void addPlayer()
    {
        int nbPlayers = int.Parse(playerNumberInput.text);
        if(nbPlayers < 6){
            nbPlayers++ ;
            if(nbPlayers == 2){
                // mask2.showMaskGraphic = true;
                addPlayers.interactable=false;
            }
        }
        Debug.Log(nbPlayers);
        playerNumberInput.text = nbPlayers.ToString();
    }

    public void substractPlayer()
    {

        int nbPlayers = int.Parse(playerNumberInput.text);

        if(nbPlayers >= 0)
        {
            nbPlayers--;
            if(nbPlayers == 2)
            {
                // mask2.showMaskGraphic = false;
                subPlayers.interactable=false;
                
            }
        }
        playerNumberInput.text = nbPlayers.ToString();
    }

    void Awake(){
        CanvasLobby.enabled = true ;
    }
    void Start() {
        instance = this ; 
        // Debug.Log($"Mode is {CreateOrJoin.instance.getMode()}");
        // if(CreateOrJoin.instance.getMode()==false){
        //     CanvasJoining.enabled = true ; 
        // }else{
        //     CanvasCreating.enabled = true ;
        // }
        
    }

    public void EnterCreateMenu()
    {
        CanvasCreating.enabled = true ;
    }

    public void EnterJoinFriendsMenu()
    {
        CanvasJoining.enabled = true ; 
    }

    public void Host()
    {
        //joinMatchInput.interactable = false ;
        //joinButton.interactable = false ; 
        hostButton.interactable = false ;  
        //convertir le nb de joueur en int 
        // int playerNb =  System.Convert.ToInt32(playerNumberInput.text);
        // Debug.Log($"Nb de joeur choisit {playerNb}");
        Debug.Log($"Player is  {PlayerManager.localPlayer}");
        PlayerManager.localPlayer.HostGame();
    }

    //pour reactiver les boutons si echec
    public void hostSucc(bool success){
        if(success){
            SceneManager.LoadScene("Lobby");
        }else{
            // joinMatchInput.interactable = true ; 
            // joinButton.interactable = true ; 
            hostButton.interactable = true ;  
        }
    }

    public void Join(){
        joinMatchInput.interactable = false ;
        joinButton.interactable = false ; 
        //hostButton.interactable = false ;  

        PlayerManager.localPlayer.JoinGame (joinMatchInput.text.ToUpper());
    }

    //pour reactiver les boutons si echec
    public void joinSucc(bool success){
        if(success){
            SceneManager.LoadScene("Lobby",LoadSceneMode.Single);
            //spawn les "Cartes des joeuurs ici " 
        }else{
            joinMatchInput.interactable = true ;
            joinButton.interactable = true ; 
            // hostButton.interactable = true ;  
        }
    }
    
    
}
