using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateOrJoin : MonoBehaviour
{
    public static CreateOrJoin instance ; 

    // MultiplayerMenu Multiplayer = gameObject.GetComponent<Multiplayer>(); 
   
    //Ces 2 boutons pour changer de scene
    [SerializeField] Button Create;
    [SerializeField] Button JoinFriends;

    // POur savoir si on veut lancer(true) ou rejoindre(false) 
    [SerializeField] bool Mode ; 

    void Awake(){
        instance = this ; 
    } 
    public void EnterMultiplayerMenu()
    {
        // SceneManager.LoadScene("JoiningMenu", LoadSceneMode.Single);
        // Multiplayer.Joining ; 
        Mode = true  ;
    }

    public void EnterCreateMenu()
    {
        // SceneManager.LoadScene("CreatingGame",LoadSceneMode.Single);
        // Multiplayer.Creating ; 
        Mode = false ;
    }

    public bool getMode(){
        return Mode ;
    }
    
}
