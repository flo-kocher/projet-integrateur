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

     public List<GameObject>  instatiateTiles(){
        GameObject go = GameObject.Find("Temp");
        GameObject tmp = GameObject.Instantiate(temp);
        int x = 0 ;
        for(int i =1 ;i< 25; i++){
            switch(i){
                case 1 : 
                   tmp.AddComponent<tile_type_1>();
                   //go  = GameObject.Instantiate(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 2 : 
                   tmp.AddComponent<tile_type_2>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 3 : 
                   tmp.AddComponent<tile_type_3>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   all_tiles.Add(go);
                   break;
                case 4 : 
                   tmp.AddComponent<tile_type_4>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 5 : 
                   tmp.AddComponent<tile_type_5>();
                   all_tiles.Add(tmp);
                   break;
                case 6 : 
                   tmp.AddComponent<tile_type_6>();
                   all_tiles.Add(tmp);
                   break;
                case 7 : 
                   tmp.AddComponent<tile_type_7>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 8 : 
                   tmp.AddComponent<tile_type_8>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 9 : 
                   tmp.AddComponent<tile_type_9>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 10 : 
                   tmp.AddComponent<tile_type_10>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 11 : 
                   tmp.AddComponent<tile_type_11>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 12 : 
                   tmp.AddComponent<tile_type_12>();
                   all_tiles.Add(tmp);
                   all_tiles.Add(tmp);
                   break;
                case 13 : 
                   tmp.AddComponent<tile_type_13>();
                   all_tiles.Add(tmp); 
                   break;                       
                case 14 :
                    for(x=0;x<1;x++){
                        tmp.AddComponent<tile_type_14>();
                        all_tiles.Add(tmp);
                    }
                    break;
                case 15 :
                    for(x=0;x<2;x++){
                        tmp.AddComponent<tile_type_15>();
                        all_tiles.Add(tmp);
                    }
                    break;
                case 16 :
                    for(x=0;x<4;x++){
                        tmp.AddComponent<tile_type_16>();
                        all_tiles.Add(tmp);
                    }
                    break;
                case 17 :
                    for(x=0;x<2;x++){
                        tmp.AddComponent<tile_type_17>();
                        all_tiles.Add(tmp);
                    }
                    break;
                case 18 :
                    for(x=0;x<2;x++){
                        tmp.AddComponent<tile_type_18>();
                        all_tiles.Add(tmp);
                    } 
                    break;
                case 19 :
                    for(x=0;x<2;x++){
                        tmp.AddComponent<tile_type_19>();
                        all_tiles.Add(tmp);
                    } 
                    break;
                //a revoir
                case 20 :
                    for(x=0;x<3;x++){
                        tmp.AddComponent<tile_type_20>();
                        all_tiles.Add(tmp);
                    } 
                    break;
                case 21 :
                    tmp.AddComponent<tile_type_21>();
                    all_tiles.Add(tmp);
                    break;
                case 22 :
                    for(x=0;x<3;x++){
                        tmp.AddComponent<tile_type_22>();
                        all_tiles.Add(tmp);
                    }  
                    break;
                case 23:
                  for(x=0;x<7;x++){
                        tmp.AddComponent<tile_type_23>();
                        all_tiles.Add(tmp);
                    }  
                    break;
                case 24 :
                    for(x=0;x<8;x++){
                        tmp.AddComponent<tile_type_24>();
                        all_tiles.Add(tmp);
                    }
                    break;
            }
                
        }
        return all_tiles;
    }


    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        instatiateTiles();
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
            randInt=rnd.Next(0,all_tiles.Count);
            // on pioche la tuile dans la liste all-tiles et puis on la supprime de la liste
            GameObject tuilos = all_tiles[randInt];
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
