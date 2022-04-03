using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EndTurn : MonoBehaviour
{

   
    public GameManager serveur;

    private void Start()
    {

    }

    public void OnClick()
    {
       
        if(serveur.Current_player == 0)
        {
            serveur.CmdUpdateJoueur(1);
        }


    }

}