using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{

   
    public GameManager serveur;
    public Button yourButton; 
    public bool some = true;
    int y=0;
    public PlayerManager PlayerManager;
    private void Start()
    {
        
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    public void Update(){
        if(!barre.first && y==0){
            barre.first = true;
            y++;
            OnClick();
        }
    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        // sent a request (which is a Command server side) to server to do something for us 
        
       
        PlayerManager.CmdUpdateJoueur(1);
        


    }

}