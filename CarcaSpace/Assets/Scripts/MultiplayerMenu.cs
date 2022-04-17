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

   // [SerializeField] private NetworkRoomManager networkRoomManager  = null ;
    private Button Create;
    private Button JoinFriends;
    
    private Text text;
    private Button addPlayers;
    private Button subPlayers;
    private Mask mask1;
    private Mask mask2;
    // ou l'on rentre le code de la partie 
    [SerializeField] InputField joinMatchInput ; 

    //ou l'on rentre le nombre de joueurs
    [SerializeField] InputField playerNumberInput ; 
    [SerializeField] Button joinButton ;
    [SerializeField] Button hostButton ;

    //Bouton ready pour les joeuurs non implemente encore 
    [SerializeField] Button readyButton ; 

    [SerializeField]  Canvas lobbyCanvas ; 

    
    //public PlayerManager Player ; 
    public void addPlayer()
    {


        int nbPlayers = int.Parse(text.text);
        if(nbPlayers < 6){
            nbPlayers++ ;
            if(nbPlayers == 2){
                mask2.showMaskGraphic = true;
            }
        }
        Debug.Log(nbPlayers);
        text.text = nbPlayers.ToString();
    }

    public void substractPlayer()
    {

        int nbPlayers = int.Parse(text.text);

        if(nbPlayers >= 0)
        {
            nbPlayers--;
            if(nbPlayers == 2)
            {
                mask2.showMaskGraphic = false;
            }
        }
        text.text = nbPlayers.ToString();
    }


    void Start() {
        instance = this ; 
        //PlayerManager.startClient();
        text = GameObject.Find("PlayerInput").GetComponent<Text>();
        mask1 = GameObject.Find("right-arrow").GetComponent<Mask>();
        mask2 = GameObject.Find("left-arrow").GetComponent<Mask>();
        Create = GameObject.Find("CreateGame").GetComponent<Button>();
        JoinFriends = GameObject.Find("JoinFriends").GetComponent<Button>();
        addPlayers = GameObject.Find("right-arrow").GetComponent<Button>();
        subPlayers = GameObject.Find("left-arrow").GetComponent<Button>();

        //Create.onClick.AddListener(EnterCreateMenu);
        JoinFriends.onClick.AddListener(EnterJoinFriendsMenu);
        addPlayers.onClick.AddListener(addPlayer);
        subPlayers.onClick.AddListener(substractPlayer);


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

    public void Host()
    {
        //joinMatchInput.interactable = false ;
        //joinButton.interactable = false ; 
        hostButton.interactable = false ;  
        
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
