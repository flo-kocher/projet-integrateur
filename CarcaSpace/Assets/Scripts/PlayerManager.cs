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

    //
    public List<GameObject> plateau = new List<GameObject>();

    //structure de chemins
    struct CurrentRoads {
    //Variable declaration
    public string Name;
    public int Size;
    public List<GameObject> CurrentTiles;

    /*
        rajoute soit les coordonnées de l'extremité 1 et l'extremité 2 
        soit mettre peut être le Go direct de l'extremité 1 et le Go de la 2
        soit peut être lister la listes des Go qui font partie du chemin avec le premier est l'extrémité 1 et le second l'extremité 2
    */
   
    //Constructor (not necessary, but helpful)
    public CurrentRoads(string name, GameObject tile)
    {
        this.Name = name;
        this.Size = 1;
        this.CurrentTiles = new List<GameObject>();
        this.CurrentTiles.Add(tile);
    }
}

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

    public void roadIsClosed(GameObject tile_laid) // (tuile posé)
    {
        // récupération de ses coordonnées sur la grid
        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;

        //Debug.Log("je suis dans roadisclosed");
        //Debug.Log(tile_laid);
        //Debug.Log(tile_laid.GetComponent<Constraints>().haut);
        //CurrentRoads road1 = new CurrentRoads("Road 1",size);
        //Debug.Log(road1.Name);

        //if struct.size == 1 alors la tile dans la liste est l'extrémité1 et extremite2
        //if struct.size > 1 alors l'extrémité1 est le premier élément et l'éxtrémité2 est le dernier elt
        /*
        if(cond)
            CurrentRoads road = new CurrentRoads(i); // variavle globale du nbr de structure pour pas qu'on ait plusieurs fois les mêmes noms
            attributs a init etc...

            puis on le rajoute à une liste de structures

        */

        /*
        if(tile_laid.GetComponent<Constraints>().haut != Type_land.Chemin && tile_laid.GetComponent<Constraints>().bas != Type_land.Chemin && tile_laid.GetComponent<Constraints>().gauche != Type_land.Chemin && tile_laid.GetComponent<Constraints>().droite != Type_land.Chemin){
            Debug.Log("je sors");
            return;
        }
        */

        //récupérer les voisins du Go parmis les tuiles du plateau
        GameObject[] voisins = new GameObject[4];
        for(int i = 0; i < plateau.Count; i++)
        {
            Debug.Log("plat : " + plateau[i]);
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y+1)
                voisins[0] = plateau[i]; // haut
            if(plateau[i].GetComponent<Constraints>().coordX == x-1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[1] = plateau[i]; // gauche
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y-1)
                voisins[2] = plateau[i]; // bas
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[3] = plateau[i]; // droite
        }
        Debug.Log("V0 "+voisins[0]+" V1 "+voisins[1]+" V2 "+voisins[2]+" V3 "+voisins[3]);
        /*
        if(tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin || tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin || tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin || tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin)
        {
            if(voisins[0] != null)

        }
        */




        /*
        si on a une valeur chemin, si le voisin est du côté du chemin alors on ajoute la tuile à la structure du voisin
        sinon on crée une nouvelle structure

        si tuile carrefour, on doit créer 3/4 structures

        */
    
        
        //puis lancer l'algo quoi

    }

public void resetVisite()
{
    for(int i = 0; i < plateau.Count; i++)
    {
        plateau[i].GetComponent<Constraints>().visite = false;
    }
}


public int noeud = 0;
public bool est_complet_chemin(GameObject tile_laid)
{
    int x = tile_laid.GetComponent<Constraints>().coordX;
    int y = tile_laid.GetComponent<Constraints>().coordY;

    GameObject[] voisins = new GameObject[4];
    for(int i = 0; i < plateau.Count; i++)
    {
        Debug.Log("plat : " + plateau[i]);
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y+1)
                voisins[0] = plateau[i]; // haut
            if(plateau[i].GetComponent<Constraints>().coordX == x-1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[1] = plateau[i]; // gauche
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y-1)
                voisins[2] = plateau[i]; // bas
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[3] = plateau[i]; // droite
    }

    if (noeud == 2)
        {
            return true;
        }  
    if (tile_laid.GetComponent<Constraints>().milieu == Type_land.Chemin)
        noeud++;
    if (tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin)
        {
            if (voisins[0] == null)
                {
                    noeud = 0;
                    return false;
                }
            if (!voisins[0].visite && tile_laid.GetComponent<Constraints>().carrefour == false)
                {
                visite = true;
                est_complet_chemin(voisin[0]);
                }
        }
    if (tile_laid.GetComponent<Constraints>().droite == Type_land.chemin)
            {
                if (voisins[1] == null)
                    {
                        noeud = 0;
                        return false;
                    }
            if (!voisins[1].visite && tile_laid.GetComponent<Constraints>().carrefour == false)
                {
                visite = true;
                est_complet_chemin(voisin[1]);
                }
            }
    if (tile_laid.GetComponent<Constraints>().bas == Type_land.chemin)
            {
                if (voisins[2] == null)
                    {
                        noeud = 0;
                        return false;
                    }
            if (!voisins[2].visite && tile_laid.GetComponent<Constraints>().carrefour == false)
                {
                visite = true;
                est_complet_chemin(voisin[2]);
                }
            }
    if (tile_laid.GetComponent<Constraints>().gauche == Type_land.chemin)
            {
                if (voisins[3] == null)
                    {
                        noeud = 0;
                        return false;
                    }
            if (!voisins[3].visite && tile_laid.GetComponent<Constraints>().carrefour == false)
                {
                visite = true;
                est_complet_chemin(voisin[3]);
                }
            } 
    if ((voisins[0].visite && voisins[1].visite)||(voisins[0].visite && voisins[2].visite)||(voisins[0].visite && voisins[3].visite)||(voisins[1].visite && voisins[2].visite)||(voisins[1].visite && voisins[3].visite)||(voisins[2].visite && voisins[3].visite))
        {
            noeud = 0;
            return true;
        }
}



// rajouter condition pour les tuiles 10 et 15 qu'il faut lancer l'algo 2 fois
public bool townIsClosed(GameObject tile_laid)
{
    int x = tile_laid.GetComponent<Constraints>().coordX;
    int y = tile_laid.GetComponent<Constraints>().coordY;

    GameObject[] voisins = new GameObject[4];
    for(int i = 0; i < plateau.Count; i++)
    {
        Debug.Log("plat : " + plateau[i]);
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y+1)
                voisins[0] = plateau[i]; // haut
            if(plateau[i].GetComponent<Constraints>().coordX == x-1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[1] = plateau[i]; // gauche
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y-1)
                voisins[2] = plateau[i]; // bas
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[3] = plateau[i]; // droite
    }
    //Debug.Log("V0 "+voisins[0]+" V1 "+voisins[1]+" V2 "+voisins[2]+" V3 "+voisins[3]);

    if (tile_laid.GetComponent<Constraints>().haut == Type_land.Ville)
    {
        if (voisins[0] == null)
            {
                return false;
            }
        if (!voisins[0].GetComponent<Constraints>().visite)
            {
            voisins[0].GetComponent<Constraints>().visite = true;
            townIsClosed(voisins[0]);
            }
    }
    if (tile_laid.GetComponent<Constraints>().droite == Type_land.Ville)
    {
        if (voisins[1] == null)
            {
                return false;
            }
        if (!voisins[1].GetComponent<Constraints>().visite)
            {
            voisins[1].GetComponent<Constraints>().visite = true;
            townIsClosed(voisins[1]);
            }
    }
    if (tile_laid.GetComponent<Constraints>().bas == Type_land.Ville)
        {
            if (voisins[2] == null)
                {
                    return false;
                }
            if (!voisins[2].GetComponent<Constraints>().visite)
                {
                voisins[2].GetComponent<Constraints>().visite = true;
                townIsClosed(voisins[2]);
                }
        }
    if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Ville)
        {
            if (voisins[3] == null)
                {
                    return false;
                }
            if (!voisins[3].GetComponent<Constraints>().visite)
                {
                voisins[3].GetComponent<Constraints>().visite = true;
                townIsClosed(voisins[3]);
                }
        }
    
    return true;
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
                plateau.Add(go);
                //roadIsClosed(go);
                //Debug.Log(plateau.Count);
            }
            else
            {
                //go.name = "le con";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
                //Debug.Log("je suis dans rpc else");
                plateau.Add(go);
                //roadIsClosed(go);
                //Debug.Log(plateau.Count);
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


