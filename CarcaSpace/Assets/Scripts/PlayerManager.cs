using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class PlayerManager : NetworkBehaviour
{
    [SyncVar]
    int compteurMeeple = 0;
    [SyncVar]
    public bool isOurTurn = false;  // tour du joueur
    [SyncVar]
    public int id;

    public GameObject grid;
    public GameObject temp;
    public GameObject TileType0;
    public GameObject TileType1;
    public GameObject TileType2;
    public GameObject TileType3;
    public GameObject TileType4;
    public GameObject TileType5;
    public GameObject TileType6;
    public GameObject TileType7;
    public GameObject TileType8;
    public GameObject TileType9;
    public GameObject TileType10;
    public GameObject TileType11;
    public GameObject TileType12;
    public GameObject TileType13;
    public GameObject TileType14;
    public GameObject TileType15;
    public GameObject TileType16;
    public GameObject TileType17;
    public GameObject TileType18;
    public GameObject TileType19;
    public GameObject TileType20;
    public GameObject TileType21;
    public GameObject TileType22;
    public GameObject TileType23;
    public GameObject TileType24;

    public GameObject ui;

    private int compteur = 0;
    

    /* ************************************ */


    bool create = true;

    public List<GameObject> all_tiles = new List<GameObject>();

    /* liste des clients connectes*/
    public List<NetworkIdentity> playerList = new List<NetworkIdentity>();

    public override void OnStartClient()
    {
        base.OnStartClient();

        grid = GameObject.Find("Grid");
        temp = GameObject.Find("Temp");
        ui = GameObject.Find("UI");

        // on ajoute l'id du joueur pour pouvoir determiner le tour plus tard
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        playerList.Add(networkIdentity);
        GameManager.Instance.AddPlayer(this);
        //Debug.Log("Player list",playerList.Count);
    }

    
    public void Update(){   
        if (!isLocalPlayer) return ;
        if (!GameManager.Instance.gameEnded){
            if (isOurTurn && isLocalPlayer){
                GameManager.Instance.endTurnButton.SetActive(true);
            }

        }
    }

     public List<GameObject>  instatiateTiles(){
        //GameObject go = GameObject.Find("Temp");
        //GameObject tmp = GameObject.Instantiate(temp);
        int x = 0 ;
        for(int i =0 ;i< 25; i++){
            switch(i){
                case 0 :
                   all_tiles.Add(TileType0);
                   break;
                case 1 : 
                   //tmp.AddComponent<tile_type_1>();
                   //go  = GameObject.Instantiate(tmp);
                   all_tiles.Add(TileType1);
                   break;
                case 2 : 
                  // tmp.AddComponent<tile_type_2>();
                   all_tiles.Add(TileType2);
                   all_tiles.Add(TileType2);
                   break;
                case 3 : 
                   //tmp.AddComponent<tile_type_3>();
                   all_tiles.Add(TileType3);
                   all_tiles.Add(TileType3);
                   all_tiles.Add(TileType3);
                   all_tiles.Add(TileType3);
                   break;
                case 4 : 
                   //tmp.AddComponent<tile_type_4>();
                   all_tiles.Add(TileType4);
                   all_tiles.Add(TileType4);
                   all_tiles.Add(TileType4);
                   break;
                case 5 : 
                   //tmp.AddComponent<tile_type_5>();
                   all_tiles.Add(TileType5);
                   break;
                case 6 : 
                  // tmp.AddComponent<tile_type_6>();
                   all_tiles.Add(TileType6);
                   break;
                case 7 : 
                  // tmp.AddComponent<tile_type_7>();
                   all_tiles.Add(TileType7);
                   all_tiles.Add(TileType7);
                   break;
                case 8 : 
                  // tmp.AddComponent<tile_type_8>();
                   all_tiles.Add(TileType8);
                   all_tiles.Add(TileType8);
                   all_tiles.Add(TileType8);
                   break;
                case 9 : 
                  // tmp.AddComponent<tile_type_9>();
                   all_tiles.Add(TileType9);
                   all_tiles.Add(TileType9);
                   break;
                case 10 : 
                   //tmp.AddComponent<tile_type_10>();
                   all_tiles.Add(TileType10);
                   all_tiles.Add(TileType10);
                   break;
                case 11 : 
                   //tmp.AddComponent<tile_type_11>();
                   all_tiles.Add(TileType11);
                   all_tiles.Add(TileType11);
                   all_tiles.Add(TileType11);
                   break;
                case 12 : 
                   //tmp.AddComponent<tile_type_12>();
                   all_tiles.Add(TileType12);
                   all_tiles.Add(TileType12);
                   break;
                case 13 : 
                  // tmp.AddComponent<tile_type_13>();
                   all_tiles.Add(TileType13); 
                   break;                       
                case 14 :
                    for(x=0;x<1;x++){
                        //tmp.AddComponent<tile_type_14>();
                        all_tiles.Add(TileType14);
                    }
                    break;
                case 15 :
                    for(x=0;x<2;x++){
                        //tmp.AddComponent<tile_type_15>();
                        all_tiles.Add(TileType15);
                    }
                    break;
                case 16 :
                    for(x=0;x<4;x++){
                        //tmp.AddComponent<tile_type_16>();
                        all_tiles.Add(TileType16);
                    }
                    break;
                case 17 :
                    for(x=0;x<2;x++){
                        //tmp.AddComponent<tile_type_17>();
                        all_tiles.Add(TileType17);
                    }
                    break;
                case 18 :
                    for(x=0;x<2;x++){
                        //tmp.AddComponent<tile_type_18>();
                        all_tiles.Add(TileType18);
                    } 
                    break;
                case 19 :
                    for(x=0;x<2;x++){
                        //tmp.AddComponent<tile_type_19>();
                        all_tiles.Add(TileType19);
                    } 
                    break;
                //a revoir
                case 20 :
                    for(x=0;x<3;x++){
                        //tmp.AddComponent<tile_type_20>();
                        all_tiles.Add(TileType20);
                    } 
                    break;
                case 21 :
                   // tmp.AddComponent<tile_type_21>();
                    all_tiles.Add(TileType21);
                    break;
                case 22 :
                    for(x=0;x<3;x++){
                       // tmp.AddComponent<tile_type_22>();
                        all_tiles.Add(TileType22);
                    }  
                    break;
                case 23:
                  for(x=0;x<7;x++){
                       // tmp.AddComponent<tile_type_23>();
                        all_tiles.Add(TileType23);
                    }  
                    break;
                case 24 :
                    for(x=0;x<8;x++){
                       // tmp.AddComponent<tile_type_24>();
                        all_tiles.Add(TileType24);
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
        Debug.Log(all_tiles.Count);
    }

    [Command]
    public void CmdDealTiles()
    {
        int randInt = 0 ; 
        System.Random rnd = new System.Random();
        /*
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
        */
        if (compteur==0)
        {
          GameObject tuilos = Instantiate(all_tiles[0]);
          all_tiles.RemoveAt(0);
          Debug.Log("Objet à faire spawn : " + tuilos);
          NetworkServer.Spawn(tuilos, connectionToClient);
          RpcShowTiles(tuilos, "Dealt");

        }
        else {
          randInt=rnd.Next(0,all_tiles.Count);
          // on pioche la tuile dans la liste all-tiles et puis on la supprime de la liste
          GameObject tuilos = Instantiate(all_tiles[randInt]);
          all_tiles.RemoveAt(randInt);
          Debug.Log("Objet à faire spawn : " + tuilos);
          NetworkServer.Spawn(tuilos, connectionToClient);
          RpcShowTiles(tuilos, "Dealt");
        }
        compteur++;
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

    [Command]
    public void CmdSpawnMeeple(){
    compteurMeeple++;
    GameObject temp = null;
    var list = Resources.FindObjectsOfTypeAll<GameObject>();
    foreach (GameObject i in list) {
      if (i.name == "tempMeeple")
        temp = i;
    }
    GameObject clone = GameObject.Instantiate(temp);
    clone.SetActive(true);
    NetworkServer.Spawn(clone, connectionToClient);
    clone.name = "Meeple" + compteurMeeple;
    clone.transform.position = new Vector3(transform.position.x + 0.6f,
                                           transform.position.y - 0.04f, 0.25f);
    clone.transform.SetParent(GameObject.Find("Meeples").transform);
    MoveMeeple.rmStars();
    }

    [ClientRpc]
    public void RpcShowMeeple(GameObject go ){
        go.SetActive(true);
    }
}


