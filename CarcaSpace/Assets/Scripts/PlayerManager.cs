using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class PlayerManager : NetworkBehaviour
{
    public GameObject grid;
    public GameObject temp;
    public GameObject ui;

    //Nbr total tuiles par partie
    private int nbrTotalTuiles = 84;

    /* ************************************ */


    bool create = true;

    public List<GameObject> all_tiles = new List<GameObject>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        grid = GameObject.Find("Grid");
        temp = GameObject.Find("Temp");
        ui = GameObject.Find("UI");
    }

    


    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        all_tiles = GetComponent<TileManager>().instatiateTiles();
        Debug.Log("els dans all_tiles : " +all_tiles);
    }

    [Command]
    public void CmdDealTiles()
    {
        int randInt = 0 ; 
        System.Random rnd = new System.Random();
         
        create = true;
        GameObject temp = null;
        var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject i in list)
        {
            if (i.name == "Temp")
                temp = i;
            if (i.name.Contains("Pioche"))
            {
                i.GetComponent<Move>().enabled = false;
                i.GetComponent<AccessDenied>().enabled = false;
                if (i.GetComponent<rotateZ>().enabled == true)
                    create = false;
            }
        }
        if (create)
        {
            randInt=rnd.Next(0,all_tiles.Count-1);
            // on pioche la tuile dans la liste all-tiles et puis on la supprime de la liste
            GameObject tuilos = Instantiate(all_tiles[randInt]);
            all_tiles.RemoveAt(randInt);
            NetworkServer.Spawn(tuilos, connectionToClient);
            RpcShowTiles(tuilos, "Dealt");

        }
    
    }

    // for message to all clients to display all tiles

    /*
        Test de tout mettre en commentaire : Fait
        Qu'il aille dans le if(hasAuthority) ou le else, déconnecte dans tous les cas

        Si on ne fait pas d'opération sur go (ex : go.SetActive(), go.name = "")
        il n'y a pas de déconnexion du client


    */
    [ClientRpc]
    void RpcShowTiles(GameObject go , string action)
    {
        Debug.Log("je suis dans rpc");
        Debug.Log("GameObject : "+ go);
        Debug.Log("Action : "+ action);
        
        if(action == "Dealt")
        {
            if(hasAuthority)
            {
                //go.name = "connard";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
                Debug.Log("je suis dans rpc if");
            }
            else
            {
                //go.name = "le con";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
                Debug.Log("je suis dans rpc else");
            }
        }
        else if (action == "Played")
        {

        }
        Debug.Log("je fais planter tout");
        
    }
}
