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

  
    [SerializeField] Button Create;
    [SerializeField] Button JoinFriends;
    
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
        // CanvasLobby.enabled = true ;
    }
    void Start() {
        instance = this ; 
        Debug.Log("Launchin main canvas ");
        CanvasMain.gameObject.SetActive (true) ;

        Create.onClick.AddListener(EnterCreateMenu);
        JoinFriends.onClick.AddListener(EnterJoinFriendsMenu);
        hostButton.onClick.AddListener(Host);
        joinButton.onClick.AddListener(Join);
        
        
    }

    public void EnterCreateMenu()
    {
        // if(CanvasCreating.gameObject.ActiveSelf == false){

            CanvasCreating.gameObject.SetActive (true) ;

            CanvasJoining.gameObject.SetActive (false) ;
            CanvasLobby.gameObject.SetActive (false) ;
            CanvasMain.gameObject.SetActive (false) ;
        // }
        
    }

    public void EnterJoinFriendsMenu()
    {
        // if(CanvasJoining.gameObject.ActiveSelf == false){

            CanvasJoining.gameObject.SetActive (true) ;

            CanvasCreating.gameObject.SetActive (false) ;
            CanvasLobby.gameObject.SetActive (false) ;
            CanvasMain.gameObject.SetActive (false) ;
        // }
    }

    public void Host()
    {
        //joinMatchInput.interactable = false ;
        //joinButton.interactable = false ; 
        hostButton.interactable = false ;  
        //convertir le nb de joueur en int 
        // int playerNb =  System.Convert.ToInt32(playerNumberInput.text);
        // Debug.Log($"Nb de joeur choisit {playerNb}");
        Debug.Log($"Player is  {RoomPlayerManager.localPlayer}");
        RoomPlayerManager.localPlayer.HostGame();
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

        RoomPlayerManager.localPlayer.JoinGame (joinMatchInput.text.ToUpper());
    }

    //pour reactiver les boutons si echec
    public void joinSucc(bool success){
        if(success){
            
            //spawn les "Cartes des joeuurs ici " 
        }else{
            joinMatchInput.interactable = true ;
            joinButton.interactable = true ; 
            // hostButton.interactable = true ;  
        }
    }
    
    
}
