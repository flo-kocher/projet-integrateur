using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class PlayerManager : NetworkBehaviour
{
    // compteur de Meeple synchronisé entre tous les clients
    [SyncVar]
    public int compteurMeeple = 0;
    // listes tous les Prefabs qui sont instanciés dans le jeu
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
    // UI
    public GameObject ui;
    // étoiles qui correspondent aux emplacements où poser les Meeples
    public GameObject Stars;
    // Meeples
    public GameObject Meeples;

    private int compteur = 0;

    // emplacements des étoiles sur une tuile
    public Vector2 nord = new Vector2(0.5f, 0.83f);
    public Vector2 sud = new Vector2(0.5f, 0.17f);
    public Vector2 est = new Vector2(0.83f, 0.5f);
    public Vector2 ouest = new Vector2(0.17f, 0.5f);
    public Vector2 milieu = new Vector2(0.5f, 0.5f);
    // tableau des emplacements
    public Vector2[] tabPos;

    
    //bool create = true;
    // liste de toutes les tuiles du jeu de tuiles
    public List<GameObject> all_tiles = new List<GameObject>();

    // liste des clients connectes
    public List<NetworkIdentity> playerList = new List<NetworkIdentity>();

    // méthode se lançant au démarrage du client
    public override void OnStartClient()
    {
        base.OnStartClient();

        grid = GameObject.Find("Grid");
        temp = GameObject.Find("Temp");
        ui = GameObject.Find("UI");

        // on ajoute l'id du joueur pour pouvoir determiner le tour plus tard
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        playerList.Add(networkIdentity);
        //Debug.Log("Player list",playerList.Count);
    }

    // méthode générant la liste des tuiles
    // on génère pour chaque type de tuile un certain numbre prédéfini de tuiles
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

    //méthode qui se lance au démarrage du serveur
    [Server]
    public override void OnStartServer()
    {
        base.OnStartServer();
        instatiateTiles();
        //Debug.Log("els dans all_tiles : " +all_tiles);
        //Debug.Log(all_tiles.Count);
        
        //instantie le tableau des positions des étoiles
        tabPos = new Vector2[5];
        tabPos[0] = nord;
        tabPos[1] = sud;
        tabPos[2] = est;
        tabPos[3] = ouest;
        tabPos[4] = milieu;
    }

    // Demande du client au serveur du tirage d'une tuile de manière aléatoire
    [Command]
    public void CmdDealTiles()
    {
        int randInt = 0 ; 
        // génération aléaoire de la seed
        System.Random rnd = new System.Random();

        if (compteur==0)
        {
            GameObject tuilos = Instantiate(all_tiles[0]);
            all_tiles.RemoveAt(0);
            Debug.Log("Objet à faire spawn : " + tuilos);
            NetworkServer.Spawn(tuilos, connectionToClient);
            RpcShowTiles(tuilos, "Dealt");

        }
        else {
            // génération aléatoire d'un nombre parmi le nombre total de tuiles restantes dans la pioche
            randInt=rnd.Next(0,all_tiles.Count);
            // on pioche la tuile dans la liste all-tiles et puis on la supprime de la liste
            GameObject tuilos = Instantiate(all_tiles[randInt]);
            all_tiles.RemoveAt(randInt);
            //Debug.Log("Objet à faire spawn : " + tuilos);
            // on spawn la tuile sur le serveur
            NetworkServer.Spawn(tuilos, connectionToClient);
            // on affiche la tuile chez tous les clients
            RpcShowTiles(tuilos, "Dealt");
        }
        compteur++;
    }

    // Affiche les tuiles chez tous les clients
    [ClientRpc]
    void RpcShowTiles(GameObject go , string action)
    {
        
        if(action == "Dealt")
        {
            if(hasAuthority)
            {
                //go.name = "connard";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
                //Debug.Log("je suis dans rpc if");
            }
            else
            {
                //go.name = "le con";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
                //Debug.Log("je suis dans rpc else");
            }
        }
        else if (action == "Played")
        {

        }
        
    }

    // Demande de pose de Meeple au serveur
    [Command]
    public void CmdSpawnMeeple(float x, float y){
        compteurMeeple++;
        // instantiation d'un Prefab Meeple
        GameObject meeple = GameObject.Instantiate(Meeples);
        meeple.SetActive(true);
        NetworkServer.Spawn(meeple, connectionToClient);
        meeple.name = "Meeple" + compteurMeeple;
        //Debug.Log("positions stars en x : " +transform.position.x + " et en y : " + transform.position.y);
        // on positionne le Meeple au dessus de la position de l'étoile sur laquelle on clique (ses positions sont les paramètres x et y)
        meeple.transform.position = new Vector3(x + 0.6f, y - 0.04f, 0.25f);

        //meeple.transform.SetParent(GameObject.Find("Meeples").transform);

        //MoveMeeple.rmStars(); ////////////////////////////////////////////////////////////////// à implémenter
        
        RpcShowMeeples(meeple,"Dealt");
    }

    // Affichage des Meeples chez tous les clients
    [ClientRpc]
    void RpcShowMeeples(GameObject go , string action)
    {
        //Debug.Log("je suis dans rpc");
        //Debug.Log("GameObject : "+ go);
        //Debug.Log("Action : "+ action);
        
        if(action == "Dealt")
        {
            if(hasAuthority)
            {
                //go.name = "connard";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
            }
            else
            {
                //go.name = "le con";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
            }
        }
        else if (action == "Played")
        {

        }        
    }

    // Demande de spawn de Stars (emplacements possibles des Meeples)
    // tab : tableau de booléens des emplacements des tuiles (nord,sud,est,ouest,milieu)
    // si = true alors on génère une étoile à cet emplacement
    // x , y : positions de l'étoile
    [Command]
    public void CmdSpawnStars(bool[] tab, float x, float y)
    {
        for (int i = 0; i < 5; i++) {
            // on vérifie les valeurs des positions et on crée les étoiles si les valeurs valent true
            if (tab[i]) {
                GameObject star = Instantiate(Stars);
                
                star.SetActive(true);
                Debug.Log("Stars à faire spawn : " + star);
                //NetworkServer.Spawn(star, connectionToClient);
                star.name = "Star" + i;
                star.transform.position = new Vector3(x + tabPos[i].x, y + tabPos[i].y, -0.1f);
                star.transform.SetParent(GameObject.Find("Stars").transform);
                NetworkServer.Spawn(star, connectionToClient);
                RpcShowStars(star, "Dealt");
                //Debug.Log("rpc fais buggué ? ");
            }
        }        
    }

    // Affichage des étoiles chez tous les clients
    [ClientRpc]
    void RpcShowStars(GameObject go , string action)
    {
        Debug.Log("GO : " + go);
        if(action == "Dealt")
        {
            if(hasAuthority)
            {
                //go.name = "connard";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
            }
            else
            {
                //go.name = "le con";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
            }
        }
        else if (action == "Played")
        {

        }        
    }
}
