using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror ; 
public class JoiningGame : MonoBehaviour
{
    public static JoiningGame instance ; 

    //Input du code de la partie 
    [SerializeField] InputField joinMatchInput;

    //Boutton pour rejoindre
    [SerializeField] Button joinButton;

     void Start () {
        instance = this;
    }
    public void Join(){
        joinMatchInput.interactable = false ;
        joinButton.interactable = false ; 
        //hostButton.interactable = false ;  

        PlayerManager.localPlayer.JoinGame (joinMatchInput.text.ToUpper());
    }

    //pour reactiver les boutons si echec et changer de scene sinon 
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
