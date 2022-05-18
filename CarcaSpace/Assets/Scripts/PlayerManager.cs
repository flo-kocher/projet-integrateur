using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class PlayerManager : NetworkBehaviour
{
    // compteur de Meeple synchronisé entre tous les clients
    public bool pose = false;
    [SyncVar]
    public bool isOurTurn = false;
    public int compteurMeeple = 0;
    [SyncVar]
    public int id;
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
    [SyncVar]
    public bool IsSpawnGrid ; 

    public int[] meeplePlayerTown = new int[] {0,0,0,0,0}; 


    // private int compteur = 0;

    // emplacements des étoiles sur une tuile
    public Vector2 haut = new Vector2(0.5f, 0.83f);
    public Vector2 bas = new Vector2(0.5f, 0.17f);
    public Vector2 droite = new Vector2(0.83f, 0.5f);
    public Vector2 gauche = new Vector2(0.17f, 0.5f);
    public Vector2 milieu = new Vector2(0.5f, 0.5f);
    // tableau des emplacements
    public Vector2[] tabPos;
    public int compteur =0;


    //bool create = true;
    // liste de toutes les tuiles du jeu de tuiles
    public List<GameObject> all_tiles = new List<GameObject>();

    // liste des clients connectes
    public List<NetworkIdentity> playerList = new List<NetworkIdentity>();

    //
    // public List<GameObject> Move.plateau = new List<GameObject>();

    // structure d'un joueur
    public struct Player
    {
        // Déclaration
        public UInt32 id;
        public int points;
        public int meeple_libre;

        // Constructor
        public Player(UInt32 id)
        {
            this.id = id;
            points = 0;
            meeple_libre = 7;
        }
        
        public Player(UInt32 id, int points)
        {
            this.id = id;
            this.points = points;
            meeple_libre = 7;
        }
    }


    public Player player; 



    public void Update(){
        if (!isLocalPlayer) return ;
        if (!GameManager.Instance.gameEnded){
            if ((isOurTurn && isLocalPlayer && pose)){
                 GameManager.Instance.Apply.SetActive(true);
            }
            else{
                GameManager.Instance.Apply.SetActive(false);
            }
        }
    }


    [Command(requiresAuthority = false)]
    public void CmdUpdateJoueur(int i)
    {
        RpcUpdateJoueur(i);
    }

    [ClientRpc]
    public void RpcUpdateJoueur(int i)
    {
        pose = false;
        GameManager.Instance._players[GameManager.Instance.Current_player].isOurTurn = false;
        GameManager.Instance.Current_player = (GameManager.Instance.Current_player + i) % GameManager.Instance.nb_joueur;
        //Debug.Log(GameManager.Instance.Current_player);
        GameManager.Instance._players[GameManager.Instance.Current_player].isOurTurn = true;
        GameObject.Find("Bar").GetComponent<barre>().resetTimer();
    }



    //structure de chemins
    public struct CurrentRoads
    {
        //Variable declaration
        public string Name;
        public int Size;
        public List<GameObject> CurrentTiles;
        public bool isClosed;
        public List<Player> lstPlayer; 
        //Constructor
        public CurrentRoads(string name, GameObject tile)
        {
            this.Name = name;
            this.Size = 1;
            this.CurrentTiles = new List<GameObject>();
            this.CurrentTiles.Add(tile);
            this.lstPlayer = new List<Player>();

            this.isClosed = false;
            //this.isClosed = false;
        }

        public CurrentRoads(string name, bool status, List<GameObject> lll)
        {
            this.Name = name;
            this.Size = 1;
            this.CurrentTiles = lll;
            this.isClosed = status;
            this.lstPlayer = new List<Player>();
        }
    }

    // public int Move.nb_of_struct_roads;
    // public List<CurrentRoads> Move.list_of_struct_roads = new List<CurrentRoads>();
    public CurrentRoads getStructByName(String name)
    {
        for (int i = 0; i < Move.list_of_struct_roads.Count; i++)
        {
            if (Move.list_of_struct_roads[i].Name == name)
                return Move.list_of_struct_roads[i];
        }
        return new CurrentRoads("Error", null);
    }

    void setIsClosedByName(String name)
    {
        for (int i = 0; i < Move.list_of_struct_roads.Count; i++)
        {
            if (Move.list_of_struct_roads[i].Name == name)
            {
                Move.list_of_struct_roads[i] = new CurrentRoads(Move.list_of_struct_roads[i].Name, true, Move.list_of_struct_roads[i].CurrentTiles);
                return;
            }
        }
        return;
    }
    
    public void affichage_score(int i)
    {
        Debug.Log("indice du joueur : "+i);
        var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach(GameObject j in list)
        {
            string contient = "Points"+(i+1).ToString()+"";
            if(j.name.Contains(contient))
            {
                // dynamic x = test_point(j);
                // x.GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+Move.list_of_struct_player[i].points.ToString();
            }
        }
    }

    public void affichage_score_demo(int i,int points)
    {
        Debug.Log("indice du joueur : "+i);
        Debug.Log("nombre de points attribués : "+points);
        var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach(GameObject j in list)
        {
            string contient = "Points"+(i+1).ToString()+"";
            if(j.name.Contains(contient))
            {
                // Debug.Log("apel à cmdaffichagescoredemo");
                // CmdAffichageScoreDemo(j,i,points);
                RpcAffichageScoreDemo(j,i,points);
                // dynamic x = test_point(j);
                // x.GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+points.ToString();
            }
        }
    }

    [Command]
    void CmdAffichageScoreDemo(GameObject point, int i, int points)
    {
        RpcAffichageScoreDemo(point,i,points);
    }

    [ClientRpc]
    void RpcAffichageScoreDemo(GameObject point, int i, int points)
    {
        // if(point != null)
        // {
        //     if(i == 0)
        //     {
        //         Move.points1+=points;
        //         var allobjects = Resources.FindObjectsOfTypeAll<GameObject>();
        //         foreach (GameObject o in allobjects)
        //             if (o.name.Contains("Points1"))
        //                 o.GetComponent<Points1>().GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+Move.points1.ToString();
                
        //     }
        //     else if(i == 1)
        //     {
        //         Move.points2+=points;
        //         var allobjects = Resources.FindObjectsOfTypeAll<GameObject>();
        //         foreach (GameObject o in allobjects)
        //             if (o.name.Contains("Points2"))
        //                 o.GetComponent<Points2>().GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+Move.points2.ToString();

        //     }
        //     else if(i == 2)
        //     {
        //         Move.points3+=points;
        //         var allobjects = Resources.FindObjectsOfTypeAll<GameObject>();
        //         foreach (GameObject o in allobjects)
        //             if (o.name.Contains("Points3"))
        //                 o.GetComponent<Points3>().GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+Move.points3.ToString();
        //     }


        //     // Debug.Log("Go : "+point);
        //     // dynamic x = test_point(point);
        //     // x.GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+points.ToString();
        // }
        if(point != null)
        {
            if(i == 0)
            {
                Move.points1+=points;
                dynamic x = test_point(point);
                x.GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+Move.points1.ToString();
            }
            else if(i == 1)
            {
                Move.points2+=points;
                dynamic x = test_point(point);
                x.GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+Move.points2.ToString();                
            }
            else if(i == 2)
            {
                Move.points3+=points;
                dynamic x = test_point(point);
                x.GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+Move.points3.ToString();
            }


            // Debug.Log("Go : "+point);
            // dynamic x = test_point(point);
            // x.GetComponent<Text>().text = "Player "+(i+1).ToString()+" : "+points.ToString();
        }

    }

    public int comptage_points(CurrentRoads lst)    // Les carrefour n'existe pas encore pour nous Et il y a au plus 4 joueurs
    {
        // à faire dynamiquement
        int[] joueur = new int[] { 0, 0, 0, 0, 0};
        int max_joueur = 0;
        for(int i=0; i<lst.lstPlayer.Count; i++)
        {   
            for(int j=0; j<Move.list_of_struct_player.Count; j++)
            {
                if(lst.lstPlayer[i].id == Move.list_of_struct_player[j].id)
                {
                    joueur[j]++;
                }
            }
        }
        
        // joueur avec le plus de pions sur un chemin
        for(int i=0; i<5; i++)
        {
            if(joueur[i] > max_joueur)
                max_joueur = joueur[i];
        }
        // Plusieurs joueurs peuvent avoir le nb max
        for(int i=0; i<Move.list_of_struct_player.Count; i++)
        {
            if(joueur[i] == max_joueur && max_joueur !=0)
            {
                Move.list_of_struct_player[i] = new Player(Move.list_of_struct_player[i].id, Move.list_of_struct_player[i].points + lst.CurrentTiles.Count);  
                Debug.Log("Points joueurs: " + Move.list_of_struct_player[i].points);
                // affichage_score_demo(GameManager.Instance.Current_player,lst.CurrentTiles.Count);
            }
        }
        
        
        return 0;
    }

    [Command]
    public void checkAllStruct()
    {
        // Debug.Log("checkallstruct debut");
        for(int i = 0; i < Move.list_of_struct_roads.Count; i++)
        {
            int cmp_est_fermante = 0;
            for(int j = 0; j < Move.list_of_struct_roads[i].CurrentTiles.Count; j++)
            {
                if(Move.list_of_struct_roads[i].CurrentTiles[j].GetComponent<Constraints>().estFermante)
                    cmp_est_fermante++;
            }
            if(cmp_est_fermante == 2 || Move.list_of_struct_roads[i].isClosed)
            {
                Debug.Log("checkallstruct fermante");
                //calcul de points
                comptage_points(Move.list_of_struct_roads[i]);
                // Debug.Log("Current : "+GameManager.Instance.Current_player);
                affichage_score_demo(GameManager.Instance.Current_player,Move.list_of_struct_roads[i].CurrentTiles.Count);

                //donner points aux Joueurs qui ont des Meeples sur le chemin
                //suppression de liste
                Move.list_of_struct_roads.RemoveAt(i);
            }
        }
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
        // Debug.Log("client connects");

        // grid = GameObject.Find("Grid");
        temp = GameObject.Find("Temp");
        ui = GameObject.Find("UI");

        // on ajoute l'id du joueur pour pouvoir determiner le tour plus tard
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        if (networkIdentity != null)
        {
            //PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            // Debug.Log("Id : " + networkIdentity);
            // Debug.Log("NetId : "+networkIdentity.netId); // type UInt32
            Player player = new Player(networkIdentity.netId);
            playerList.Add(networkIdentity);
            Move.list_of_struct_player.Add(player);
            ////Debug.Log("Player list",playerList.Count);
            // on ajoute l'id du joueur pour pouvoir determiner le tour plus tard
            // NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            // playerList.Add(networkIdentity);
            
        }
        GameManager.Instance.AddPlayer(this);
    }

    public List<GameObject> instatiateTiles_demo()
    {
        all_tiles.Add(TileType0);
        all_tiles.Add(TileType4);
        all_tiles.Add(TileType8);
        all_tiles.Add(TileType8);
        all_tiles.Add(TileType8);
        all_tiles.Add(TileType22);
        all_tiles.Add(TileType24);
        all_tiles.Add(TileType23);        
        all_tiles.Add(TileType24);
        all_tiles.Add(TileType23);
        all_tiles.Add(TileType3);        
        all_tiles.Add(TileType23);
        all_tiles.Add(TileType24);
        // all_tiles.Add(TileType24);
        // all_tiles.Add(TileType23);
        all_tiles.Add(TileType17);
        all_tiles.Add(TileType15);
        all_tiles.Add(TileType2);
        all_tiles.Add(TileType2);
        
        return all_tiles;
    }


    // méthode générant la liste des tuiles
    // on génère pour chaque type de tuile un certain numbre prédéfini de tuiles
    public List<GameObject> instatiateTiles()
    {
        //GameObject go = GameObject.Find("Temp");
        //GameObject tmp = GameObject.Instantiate(temp);
        int x = 0;
        for (int i = 0; i < 25; i++)
        {
            switch (i)
            {
                case 0:
                    all_tiles.Add(TileType0);
                    break;
                case 1:
                    //tmp.AddComponent<tile_type_1>();
                    //go  = GameObject.Instantiate(tmp);
                    all_tiles.Add(TileType1);
                    break;
                case 2:
                    // tmp.AddComponent<tile_type_2>();
                    for (x = 0; x <= 1; x++)
                    {
                        all_tiles.Add(TileType2);
                    }
                    break;
                case 3:
                    //tmp.AddComponent<tile_type_3>();
                    for (x = 0; x <= 3; x++)
                    {
                        all_tiles.Add(TileType3);
                    }
                    break;
                case 4:
                    //tmp.AddComponent<tile_type_4>();
                    for (x = 0; x <= 2; x++)
                    {
                        all_tiles.Add(TileType4);
                    }
                    break;
                case 5:
                    //tmp.AddComponent<tile_type_5>();
                    all_tiles.Add(TileType5);
                    break;
                case 6:
                    // tmp.AddComponent<tile_type_6>();
                    all_tiles.Add(TileType6);
                    break;
                case 7:
                    // tmp.AddComponent<tile_type_7>();
                    for (x = 0; x <= 1; x++)
                    {
                        all_tiles.Add(TileType7);
                    }
                    break;
                case 8:
                    // tmp.AddComponent<tile_type_8>();
                    for (x = 0; x <= 2; x++)
                    {
                        all_tiles.Add(TileType8);
                    }
                    break;
                case 9:
                    // tmp.AddComponent<tile_type_9>();
                    for (x = 0; x <= 1; x++)
                    {
                        all_tiles.Add(TileType9);
                    }
                    break;
                case 10:
                    //tmp.AddComponent<tile_type_10>();
                    for (x = 0; x <= 1; x++)
                    {
                        all_tiles.Add(TileType10);
                    }
                    break;
                case 11:
                    //tmp.AddComponent<tile_type_11>();
                    for (x = 0; x <= 2; x++)
                    {
                        all_tiles.Add(TileType11);
                    }
                    break;
                case 12:
                    //tmp.AddComponent<tile_type_12>();
                    for (x = 0; x <= 1; x++)
                    {
                        all_tiles.Add(TileType12);
                    }
                    break;
                case 13:
                    // tmp.AddComponent<tile_type_13>();
                    all_tiles.Add(TileType13);
                    break;
                case 14:
                    for (x = 0; x <= 1; x++)
                    {
                        //tmp.AddComponent<tile_type_14>();
                        all_tiles.Add(TileType14);
                    }
                    break;
                case 15:
                    for (x = 0; x <= 2; x++)
                    {
                        //tmp.AddComponent<tile_type_15>();
                        all_tiles.Add(TileType15);
                    }
                    break;
                case 16:
                    for (x = 0; x <= 4; x++)
                    {
                        //tmp.AddComponent<tile_type_16>();
                        all_tiles.Add(TileType16);
                    }
                    break;
                case 17:
                    for (x = 0; x <= 2; x++)
                    {
                        //tmp.AddComponent<tile_type_17>();
                        all_tiles.Add(TileType17);
                    }
                    break;
                case 18:
                    for (x = 0; x <= 2; x++)
                    {
                        //tmp.AddComponent<tile_type_18>();
                        all_tiles.Add(TileType18);
                    }
                    break;
                case 19:
                    for (x = 0; x <= 2; x++)
                    {
                        //tmp.AddComponent<tile_type_19>();
                        all_tiles.Add(TileType19);
                    }
                    break;
                case 20:
                    for (x = 0; x <= 3; x++)
                    {
                        //tmp.AddComponent<tile_type_20>();
                        all_tiles.Add(TileType20);
                    }
                    break;
                case 21:
                    // tmp.AddComponent<tile_type_21>();
                    all_tiles.Add(TileType21);
                    break;
                case 22 :
                    for(x=0;x<=3;x++)
                    {
                        // tmp.AddComponent<tile_type_22>();
                        all_tiles.Add(TileType22);
                    }  
                    break;
                case 23:
                    for (x = 0; x <= 7; x++)
                    {
                        // tmp.AddComponent<tile_type_23>();
                        all_tiles.Add(TileType23);
                    }
                    break;
                case 24:
                    for (x = 0; x <= 8; x++)
                    {
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
        // Debug.Log("on start server");
        // instatiateTiles();
        instatiateTiles_demo();
        CmdSpawnGrid(10);
        // Points1.GetComponent<Text>().text = "Player 1 : 0";
        // Points3.GetComponent<Text>().text = "Player 4 : 0";
        // Points2.GetComponent<Text>().text = "Player 2 : 0";
        // Points4.GetComponent<Text>().text = "Player 5 : 0";
        // Points5.GetComponent<Text>().text = "Player 3 : 0";
        // Debug.Log("els dans all_tiles : " +all_tiles);
        // Debug.Log("ici start");

        //instantie le tableau des positions des étoiles
        tabPos = new Vector2[5];
        tabPos[0] = haut;
        tabPos[1] = gauche;
        tabPos[2] = bas;
        tabPos[3] = droite;
        tabPos[4] = milieu;
        Move.nb_of_struct_roads = 0;
    }

    // [Command]
    // public void resetVisite()
    // {
    //     RpcresetVisite();
    // }

    // [ClientRpc]
    [Command]
    public void resetVisite()
    {
        for (int i = 0; i < Move.plateau.Count; i++)
        {
            Move.plateau[i].GetComponent<Constraints>().visite = false;
        }
    }
    

    public void createNewStruct(GameObject tile_laid, String intersection)
    {
        //tile_laid.name += intersection; on ne peut pas changer son nom sinon sa change son nom en "global" pour une raison ou pour une autre
        CurrentRoads road = new CurrentRoads("Road " + Move.nb_of_struct_roads + "" + intersection, tile_laid);
        // ajout de la stucture à la liste_des_struct
        Move.list_of_struct_roads.Add(road);
    }

    [Command]
    public void seeStruct()
    {
        // Debug.Log("liste des structs "+Move.list_of_struct_roads.Count);
        for(int k = 0; k < Move.list_of_struct_roads.Count; k++)
        {
          Debug.Log("nb d'elt dans  la structure "+k+ " : "+Move.list_of_struct_roads[k].CurrentTiles.Count);
        }
    }

    [Command]
    public void roadIsClosed_Struct(GameObject tile_laid)
    {
        if (tile_laid.GetComponent<Constraints>().haut != Type_land.Chemin && tile_laid.GetComponent<Constraints>().bas != Type_land.Chemin && tile_laid.GetComponent<Constraints>().gauche != Type_land.Chemin && tile_laid.GetComponent<Constraints>().droite != Type_land.Chemin && tile_laid.GetComponent<Constraints>().haut != Type_land.Chemin)
        {
            //Debug.Log("Pas de composante Chemin sur ma tuile");
            return;
        }

        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;
       // Debug.Log(" Nom tile : " + tile_laid.name);
       // Debug.Log("Coord : "+x+"   "+y);

        // on ne met dans voisins[] que les voisins qui ont une connection Chemin avec notre Go, puisque ce ne sont que eux qui nous interesse
        GameObject[] voisins = new GameObject[4];
        for (int i = 0; i < Move.plateau.Count; i++)
        {
            // Debug.Log("COORD ELT PLATEAU : " + Move.plateau[i].GetComponent<Constraints>().coordX + "   " + Move.plateau[i].GetComponent<Constraints>().coordY);
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y + 1 && Move.plateau[i].GetComponent<Constraints>().bas == Type_land.Chemin)
            {
                // Debug.Log("haut");
                voisins[0] = Move.plateau[i]; // haut
            }
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x - 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y && Move.plateau[i].GetComponent<Constraints>().droite == Type_land.Chemin)
            {
                // Debug.Log("gauche");
                voisins[1] = Move.plateau[i]; // gauche
            }
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y - 1 && Move.plateau[i].GetComponent<Constraints>().haut == Type_land.Chemin)
            {
                // Debug.Log("bas");
                voisins[2] = Move.plateau[i]; // bas
            }
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x + 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y && Move.plateau[i].GetComponent<Constraints>().gauche == Type_land.Chemin)
            {
                // Debug.Log("droite");
                voisins[3] = Move.plateau[i]; // droite
            }
        }
        // Debug.Log("Taille du Move.plateau : " + Move.plateau.Count + "les coord de la tuile sont : " + x + " " + y);
        // for(int i =0; i < Move.plateau.Count; i++)
        // {
        //     Debug.Log(" Tuile : " + i + " a comme coord : " + Move.plateau[i].GetComponent<Constraints>().coordX + " " + Move.plateau[i].GetComponent<Constraints>().coordY);
        // }
        // Debug.Log("La tuile " + tile_laid + " a comme voisins : haut " + voisins[0] + " gauche " + voisins[1] + " bas " + voisins[2] + " droite " + voisins[3]);

        // il n'y a pas de connexion de chemin, on crée une nouvelle structure
        if (voisins[0] == null && voisins[1] == null && voisins[2] == null && voisins[3] == null)
        {
            //cas particulier de la tuile 21 n'arrivera jamais, puisque liaison chemin sur tous les coins

            //cas particulier tuile 17 et 22
            if (tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
            {
                // Debug.Log("Aucun voisins et tuile 17 ou 22");
                //créer 3 structures
                if (tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin)
                    createNewStruct(tile_laid, "_0");
                if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin)
                    createNewStruct(tile_laid, "_1");
                if (tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin)
                    createNewStruct(tile_laid, "_2");
                if (tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin)
                    createNewStruct(tile_laid, "_3");
            }
            else
            {
                // Debug.Log("Aucun voisins");
                //créer une nouvelle structure
                createNewStruct(tile_laid, "");
            }
            Move.nb_of_struct_roads++;
        }

        String nom_struct_haut = "";
        String nom_struct_gauche = "";
        String nom_struct_bas = "";
        String nom_struct_droite = "";
        bool face_haut = false;
        bool face_gauche = false;
        bool face_bas = false;
        bool face_droite = false;
        int cmp_haut = 0, cmp_gauche = 0, cmp_bas = 0, cmp_droite = 0;

        for (int j = 0; j < Move.list_of_struct_roads.Count; j++)
        {
            for (int k = 0; k < Move.list_of_struct_roads[j].CurrentTiles.Count; k++)
            {
                if (Move.list_of_struct_roads[j].CurrentTiles[k] == voisins[0])
                {
                    nom_struct_haut = Move.list_of_struct_roads[j].Name;
                    face_haut = true;
                    cmp_haut++;
                    if (cmp_haut > 1)
                    {
                        nom_struct_haut = nom_struct_haut.Substring(0, nom_struct_haut.Length - 1);
                        nom_struct_haut += "2";
                    }
                }
                if (Move.list_of_struct_roads[j].CurrentTiles[k] == voisins[1])
                {
                    nom_struct_gauche = Move.list_of_struct_roads[j].Name;
                    face_gauche = true;
                    cmp_gauche++;
                    if (cmp_gauche > 1)
                    {
                        nom_struct_gauche = nom_struct_gauche.Substring(0, nom_struct_gauche.Length - 1);
                        nom_struct_gauche += "3";
                    }
                }
                if (Move.list_of_struct_roads[j].CurrentTiles[k] == voisins[2])
                {
                    nom_struct_bas = Move.list_of_struct_roads[j].Name;
                    face_bas = true;
                    cmp_bas++;
                    if (cmp_bas > 1)
                    {
                        nom_struct_bas = nom_struct_bas.Substring(0, nom_struct_bas.Length - 1);
                        nom_struct_bas += "0";
                    }
                }
                if (Move.list_of_struct_roads[j].CurrentTiles[k] == voisins[3])
                {
                    nom_struct_droite = Move.list_of_struct_roads[j].Name;
                    face_droite = true;
                    cmp_droite++;
                    if (cmp_droite > 1)
                    {
                        nom_struct_droite = nom_struct_droite.Substring(0, nom_struct_droite.Length - 1);
                        nom_struct_droite += "1";
                    }
                }
            }
        }

        // Debug.Log("Noms face : face_haut "+face_haut+"  face_garche "+face_gauche+"  face_bas "+face_bas+"  face_droite "+face_droite);
        // Debug.Log("Nom structs : Nom_haut "+nom_struct_haut+"  Nom_gauche "+nom_struct_gauche+"  Nom_bas "+nom_struct_bas+"  Nom_droite "+nom_struct_droite);

        if(face_haut && face_gauche && face_bas && face_droite) // tile 21 forcément
        // if(tile_laid.name.Contains("21"))
        {
            //9 cas possibles :
            // cas 1
            if(nom_struct_haut != nom_struct_gauche && nom_struct_gauche != nom_struct_bas && nom_struct_bas != nom_struct_droite && nom_struct_droite != nom_struct_haut)
            {
                CurrentRoads r1 = getStructByName(nom_struct_haut);
                r1.CurrentTiles.Add(tile_laid);
                CurrentRoads r2 = getStructByName(nom_struct_gauche);
                r2.CurrentTiles.Add(tile_laid);
                CurrentRoads r3 = getStructByName(nom_struct_bas);
                r3.CurrentTiles.Add(tile_laid);
                CurrentRoads r4 = getStructByName(nom_struct_droite);
                r4.CurrentTiles.Add(tile_laid);
            }
            // cas 2
            if(nom_struct_haut == nom_struct_gauche && nom_struct_gauche != nom_struct_bas && nom_struct_bas != nom_struct_droite && nom_struct_droite != nom_struct_haut)
            {
                CurrentRoads r1 = getStructByName(nom_struct_haut);
                r1.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_haut);
                CurrentRoads r2 = getStructByName(nom_struct_bas);
                r2.CurrentTiles.Add(tile_laid);
                CurrentRoads r3 = getStructByName(nom_struct_droite);
                r3.CurrentTiles.Add(tile_laid);
            }
            // cas 3
            if(nom_struct_haut == nom_struct_droite && nom_struct_droite != nom_struct_bas && nom_struct_bas != nom_struct_gauche && nom_struct_bas != nom_struct_gauche)
            {
                CurrentRoads r1 = getStructByName(nom_struct_haut);
                r1.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_haut);
                CurrentRoads r2 = getStructByName(nom_struct_bas);
                r2.CurrentTiles.Add(tile_laid);
                CurrentRoads r3 = getStructByName(nom_struct_gauche);
                r3.CurrentTiles.Add(tile_laid);
            }
            // cas 4
            if(nom_struct_bas == nom_struct_gauche && nom_struct_gauche != nom_struct_haut && nom_struct_haut != nom_struct_droite && nom_struct_droite != nom_struct_bas)
            {
                CurrentRoads r1 = getStructByName(nom_struct_bas);
                r1.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);
                CurrentRoads r2 = getStructByName(nom_struct_haut);
                r2.CurrentTiles.Add(tile_laid);
                CurrentRoads r3 = getStructByName(nom_struct_droite);
                r3.CurrentTiles.Add(tile_laid);
            }
            // cas 5
            if(nom_struct_bas == nom_struct_droite && nom_struct_droite != nom_struct_haut && nom_struct_haut != nom_struct_gauche && nom_struct_gauche!= nom_struct_bas)
            {
                CurrentRoads r1 = getStructByName(nom_struct_bas);
                r1.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);
                CurrentRoads r2 = getStructByName(nom_struct_haut);
                r2.CurrentTiles.Add(tile_laid);
                CurrentRoads r3 = getStructByName(nom_struct_gauche);
                r3.CurrentTiles.Add(tile_laid);
            }
            // cas 6
            if(nom_struct_haut == nom_struct_gauche && nom_struct_bas == nom_struct_droite && nom_struct_haut != nom_struct_droite && nom_struct_bas != nom_struct_gauche)
            {
                CurrentRoads r1 = getStructByName(nom_struct_haut);
                r1.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_haut);
                CurrentRoads r2 = getStructByName(nom_struct_bas);
                r2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);
            }
            // cas 7
            if(nom_struct_haut == nom_struct_droite && nom_struct_bas == nom_struct_gauche && nom_struct_haut != nom_struct_gauche && nom_struct_bas!= nom_struct_droite)
            {
                CurrentRoads r1 = getStructByName(nom_struct_haut);
                r1.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_haut);
                CurrentRoads r2 = getStructByName(nom_struct_bas);
                r2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);
            }
            // cas 8
            if(nom_struct_haut == nom_struct_bas && nom_struct_bas != nom_struct_gauche && nom_struct_bas != nom_struct_droite && nom_struct_droite != nom_struct_gauche)
            {
                CurrentRoads r1 = getStructByName(nom_struct_haut);
                r1.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_haut);
                CurrentRoads r2 = getStructByName(nom_struct_gauche);
                r2.CurrentTiles.Add(tile_laid);
                CurrentRoads r3 = getStructByName(nom_struct_droite);
                r3.CurrentTiles.Add(tile_laid);
            }
            // cas 9
            if(nom_struct_gauche == nom_struct_droite && nom_struct_haut != nom_struct_bas && nom_struct_bas != nom_struct_droite && nom_struct_droite!=nom_struct_haut)
            {
                CurrentRoads r1 = getStructByName(nom_struct_haut);
                r1.CurrentTiles.Add(tile_laid);
                CurrentRoads r2 = getStructByName(nom_struct_bas);
                r2.CurrentTiles.Add(tile_laid);
                CurrentRoads r3 = getStructByName(nom_struct_droite);
                r3.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_droite);
            }
        }
        //début 3 cas
        else if (face_haut && face_gauche && face_bas && !face_droite) // tile 17 ou 22 forcément
        {
            if (nom_struct_haut != nom_struct_gauche && nom_struct_gauche != nom_struct_bas && nom_struct_bas != nom_struct_haut)
            {
                CurrentRoads road = getStructByName(nom_struct_haut);
                road.CurrentTiles.Add(tile_laid);
                CurrentRoads road2 = getStructByName(nom_struct_gauche);
                road2.CurrentTiles.Add(tile_laid);
                CurrentRoads road3 = getStructByName(nom_struct_bas);
                road3.CurrentTiles.Add(tile_laid);
            } 
            if (nom_struct_haut == nom_struct_gauche && nom_struct_gauche != nom_struct_bas )
            {
                //fusion entre haut et gauche
                CurrentRoads road = getStructByName(nom_struct_gauche);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en bas
                CurrentRoads road2 = getStructByName(nom_struct_bas);
                road2.CurrentTiles.Add(tile_laid);
                // chemin fermé à gauche mais pas focement en bas(ça dépend s'il une tuiles fermante)
                setIsClosedByName(nom_struct_gauche);
            }
            if (nom_struct_gauche == nom_struct_bas && nom_struct_gauche != nom_struct_haut)
            {
                //fusion entre gauche et bas
                CurrentRoads road = getStructByName(nom_struct_gauche);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en haut
                CurrentRoads road2 = getStructByName(nom_struct_haut);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_gauche);
            }
            if (nom_struct_bas == nom_struct_haut && nom_struct_haut != nom_struct_gauche)
            {
                //fusion entre bas et haut
                CurrentRoads road = getStructByName(nom_struct_bas);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en gauche
                CurrentRoads road2 = getStructByName(nom_struct_gauche);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);
            }
        }
        else if (face_haut && face_gauche && face_droite && !face_bas) // tile 17 ou 22 forcément
        {
            if (nom_struct_haut != nom_struct_gauche && nom_struct_gauche != nom_struct_droite && nom_struct_droite!=nom_struct_haut)
            {
                CurrentRoads road = getStructByName(nom_struct_haut);
                road.CurrentTiles.Add(tile_laid);
                CurrentRoads road2 = getStructByName(nom_struct_gauche);
                road2.CurrentTiles.Add(tile_laid);
                CurrentRoads road3 = getStructByName(nom_struct_droite);
                road3.CurrentTiles.Add(tile_laid);
            }
            if (nom_struct_haut == nom_struct_gauche && nom_struct_gauche != nom_struct_droite)
            {
                //fusion entre haut et gauche
                CurrentRoads road = getStructByName(nom_struct_gauche);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en bas
                CurrentRoads road2 = getStructByName(nom_struct_droite);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_haut);
            }
            if (nom_struct_gauche == nom_struct_droite && nom_struct_droite != nom_struct_haut)
            {
                //fusion entre gauche et bas
                CurrentRoads road = getStructByName(nom_struct_gauche);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en haut
                CurrentRoads road2 = getStructByName(nom_struct_haut);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_gauche);
            }
            if (nom_struct_droite == nom_struct_haut && nom_struct_haut!=nom_struct_gauche)
            {
                //fusion entre bas et haut
                CurrentRoads road = getStructByName(nom_struct_droite);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en gauche
                CurrentRoads road2 = getStructByName(nom_struct_gauche);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_droite);
            }
        }
        else if (face_haut && face_droite && face_bas && !face_gauche) // tile 17 ou 22 forcément
        {
            if (nom_struct_haut != nom_struct_droite && nom_struct_droite != nom_struct_bas && nom_struct_bas!=nom_struct_haut)
            {
                CurrentRoads road = getStructByName(nom_struct_haut);
                road.CurrentTiles.Add(tile_laid);
                CurrentRoads road2 = getStructByName(nom_struct_droite);
                road2.CurrentTiles.Add(tile_laid);
                CurrentRoads road3 = getStructByName(nom_struct_bas);
                road3.CurrentTiles.Add(tile_laid);
            }
            if (nom_struct_haut == nom_struct_droite && nom_struct_droite != nom_struct_bas)
            {
                //fusion entre haut et gauche
                CurrentRoads road = getStructByName(nom_struct_droite);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en bas
                CurrentRoads road2 = getStructByName(nom_struct_bas);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_droite);
            }
            if (nom_struct_bas == nom_struct_droite && nom_struct_droite != nom_struct_haut)
            {
                //fusion entre gauche et bas
                CurrentRoads road = getStructByName(nom_struct_bas);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en haut
                CurrentRoads road2 = getStructByName(nom_struct_haut);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_droite);
            }
            if (nom_struct_bas == nom_struct_haut && nom_struct_bas!=nom_struct_droite)
            {
                //fusion entre bas et haut
                CurrentRoads road = getStructByName(nom_struct_bas);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en gauche
                CurrentRoads road2 = getStructByName(nom_struct_droite);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);
            }
        }
        else if (face_droite && face_gauche && face_bas && !face_haut) // tile 17 ou 22 forcément
        {
            if (nom_struct_gauche != nom_struct_droite && nom_struct_droite != nom_struct_bas && nom_struct_bas!= nom_struct_droite)
            {
                CurrentRoads road = getStructByName(nom_struct_gauche);
                road.CurrentTiles.Add(tile_laid);
                CurrentRoads road2 = getStructByName(nom_struct_droite);
                road2.CurrentTiles.Add(tile_laid);
                CurrentRoads road3 = getStructByName(nom_struct_bas);
                road3.CurrentTiles.Add(tile_laid);
            }
            if (nom_struct_bas == nom_struct_droite && nom_struct_droite != nom_struct_gauche)
            {
                //fusion entre haut et gauche
                CurrentRoads road = getStructByName(nom_struct_droite);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en bas
                CurrentRoads road2 = getStructByName(nom_struct_gauche);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);

            }
            if (nom_struct_bas == nom_struct_gauche && nom_struct_gauche!= nom_struct_droite)
            {
                //fusion entre gauche et bas
                CurrentRoads road = getStructByName(nom_struct_bas);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en haut
                CurrentRoads road2 = getStructByName(nom_struct_droite);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);
            }
            if (nom_struct_droite == nom_struct_gauche && nom_struct_gauche!=nom_struct_bas)
            {
                //fusion entre bas et haut
                CurrentRoads road = getStructByName(nom_struct_droite);
                road.CurrentTiles.Add(tile_laid);
                //concaténation en gauche
                CurrentRoads road2 = getStructByName(nom_struct_bas);
                road2.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_droite);
            }
        }

        //cas avec 2 faces Chemin
        else if (face_haut && face_gauche || face_haut && face_bas || face_gauche && face_droite || face_bas && face_gauche || face_bas && face_droite || face_haut && face_droite)
        {
            if (nom_struct_gauche == nom_struct_haut && nom_struct_bas == "" && nom_struct_droite == "")
            {
                // Si tuile 21
                if (tile_laid.name.Contains("21"))
                {
                    // créer deux structures
                    createNewStruct(tile_laid, "_2");
                    createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads+=2;
                }
                // si c'est la tuile 17 ou 22
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    // créer une structure 
                    if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_2");
                    if(tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
                // sinon simple virage
                CurrentRoads road = getStructByName(nom_struct_gauche);
                road.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_gauche);
            }
            if (nom_struct_gauche != nom_struct_haut && nom_struct_bas == "" && nom_struct_droite == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_2");
                    createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads+=2;
                    CurrentRoads r1 = getStructByName(nom_struct_gauche);
                    r1.CurrentTiles.Add(tile_laid);
                    CurrentRoads r2 = getStructByName(nom_struct_haut);
                    r2.CurrentTiles.Add(tile_laid);
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_2");
                    if(tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
                else
                {
                    CurrentRoads road_gauche = getStructByName(nom_struct_gauche);
                    CurrentRoads road_haut = getStructByName(nom_struct_haut);
                    // Debug.Log("RG : " + road_gauche + " RH : " + road_haut);
                    for (int i = 0; i < road_gauche.CurrentTiles.Count; i++)
                    {
                        road_haut.CurrentTiles.Add(road_gauche.CurrentTiles[i]);
                    }
                    road_haut.CurrentTiles.Add(tile_laid);
                    Move.list_of_struct_roads.Remove(road_gauche);
                    Move.nb_of_struct_roads--;
                }
            }

            if (nom_struct_gauche == nom_struct_bas && nom_struct_haut == "" && nom_struct_droite == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_0");
                    createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads+=2;
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_3");
                    if(tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_0");
                    Move.nb_of_struct_roads++;
                }
                CurrentRoads road = getStructByName(nom_struct_gauche);
                road.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_gauche);             
            }

            if (nom_struct_gauche != nom_struct_bas && nom_struct_droite == "" && nom_struct_haut == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_0");
                    createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads+=2;
                    CurrentRoads r1 = getStructByName(nom_struct_gauche);
                    r1.CurrentTiles.Add(tile_laid);
                    CurrentRoads r2 = getStructByName(nom_struct_haut);
                    r2.CurrentTiles.Add(tile_laid);
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_0");
                    if(tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
                else
                {
                    CurrentRoads road_gauche = getStructByName(nom_struct_gauche);
                    CurrentRoads road_haut = getStructByName(nom_struct_haut);
                    //Debug.Log("RG : " + road_gauche + " RH : " + road_haut);
                    for (int i = 0; i < road_gauche.CurrentTiles.Count; i++)
                    {
                        road_haut.CurrentTiles.Add(road_gauche.CurrentTiles[i]);
                    }
                    road_haut.CurrentTiles.Add(tile_laid);
                    Move.list_of_struct_roads.Remove(road_gauche);
                    Move.nb_of_struct_roads--;
                }
            }

            if (nom_struct_gauche == nom_struct_droite && nom_struct_bas == "" && nom_struct_haut == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_0");
                    createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads+=2;
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_0");
                    if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads++;
                }
                CurrentRoads road = getStructByName(nom_struct_droite);
                road.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_droite);
            }
            if (nom_struct_gauche != nom_struct_droite && nom_struct_bas == "" && nom_struct_haut == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_0");
                    createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads+=2;
                    CurrentRoads r1 = getStructByName(nom_struct_gauche);
                    r1.CurrentTiles.Add(tile_laid);
                    CurrentRoads r2 = getStructByName(nom_struct_droite);
                    r2.CurrentTiles.Add(tile_laid);
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_0");
                    if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads++;
                }
                else
                {
                    CurrentRoads road_gauche = getStructByName(nom_struct_gauche);
                    CurrentRoads road_haut = getStructByName(nom_struct_droite);
                    for (int i = 0; i < road_gauche.CurrentTiles.Count; i++)
                    {
                        road_haut.CurrentTiles.Add(road_gauche.CurrentTiles[i]);
                    }
                    road_haut.CurrentTiles.Add(tile_laid);
                    Move.list_of_struct_roads.Remove(road_gauche);
                    Move.nb_of_struct_roads--;
                }
            }
            if (nom_struct_droite == nom_struct_haut && nom_struct_bas == "" && nom_struct_gauche == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_1");
                    createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads+=2;
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_1");
                    if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads++;
                }
                CurrentRoads road = getStructByName(nom_struct_droite);
                road.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_droite);              
            }
            if (nom_struct_droite != nom_struct_haut && nom_struct_bas == "" && nom_struct_gauche == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_1");
                    createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads+=2;
                    CurrentRoads r1 = getStructByName(nom_struct_bas);
                    r1.CurrentTiles.Add(tile_laid);
                    CurrentRoads r2 = getStructByName(nom_struct_haut);
                    r2.CurrentTiles.Add(tile_laid);
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_1");
                    if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads++;
                }
                else
                {
                    CurrentRoads road_gauche = getStructByName(nom_struct_haut);
                    CurrentRoads road_haut = getStructByName(nom_struct_droite);
                    for (int i = 0; i < road_gauche.CurrentTiles.Count; i++)
                    {
                        road_haut.CurrentTiles.Add(road_gauche.CurrentTiles[i]);
                    }
                    road_haut.CurrentTiles.Add(tile_laid);
                    Move.list_of_struct_roads.Remove(road_gauche);
                    Move.nb_of_struct_roads--;
                }
            }
            if (nom_struct_droite == nom_struct_bas && nom_struct_haut == "" && nom_struct_gauche == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_0");
                    createNewStruct(tile_laid, "_1");
                    Move.nb_of_struct_roads+=2;
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_0");
                    if(tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_1");
                    Move.nb_of_struct_roads++;
                }
                CurrentRoads road = getStructByName(nom_struct_bas);
                road.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);              
            }
            if (nom_struct_droite != nom_struct_bas && nom_struct_haut == "" && nom_struct_gauche == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_0");
                    createNewStruct(tile_laid, "_1");
                    Move.nb_of_struct_roads+=2;
                    CurrentRoads r1 = getStructByName(nom_struct_bas);
                    r1.CurrentTiles.Add(tile_laid);
                    CurrentRoads r2 = getStructByName(nom_struct_droite);
                    r2.CurrentTiles.Add(tile_laid);
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_0");
                    if(tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_1");
                    Move.nb_of_struct_roads++;
                }
                else
                {
                    CurrentRoads road_gauche = getStructByName(nom_struct_bas);
                    CurrentRoads road_haut = getStructByName(nom_struct_droite);
                    for (int i = 0; i < road_gauche.CurrentTiles.Count; i++)
                    {
                        road_haut.CurrentTiles.Add(road_gauche.CurrentTiles[i]);
                    }
                    road_haut.CurrentTiles.Add(tile_laid);
                    Move.list_of_struct_roads.Remove(road_gauche);
                    Move.nb_of_struct_roads--;
                }
            }
            if (nom_struct_haut == nom_struct_bas && nom_struct_gauche == "" && nom_struct_droite == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_1");
                    createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads+=2;
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_1");
                    if(tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
                CurrentRoads road = getStructByName(nom_struct_bas);
                road.CurrentTiles.Add(tile_laid);
                setIsClosedByName(nom_struct_bas);              
            }
            if (nom_struct_haut != nom_struct_bas && nom_struct_gauche == "" && nom_struct_droite == "")
            {
                if (tile_laid.name.Contains("21"))
                {
                    createNewStruct(tile_laid, "_1");
                    createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads+=2;
                    CurrentRoads r1 = getStructByName(nom_struct_bas);
                    r1.CurrentTiles.Add(tile_laid);
                    CurrentRoads r2 = getStructByName(nom_struct_haut);
                    r2.CurrentTiles.Add(tile_laid);
                }
                else if(tile_laid.name.Contains("17") || tile_laid.name.Contains("22"))
                {
                    if(tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_1");
                    if(tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin) 
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
                else
                {
                    CurrentRoads road_gauche = getStructByName(nom_struct_bas);
                    CurrentRoads road_haut = getStructByName(nom_struct_haut);
                    for (int i = 0; i < road_gauche.CurrentTiles.Count; i++)
                    {
                        road_haut.CurrentTiles.Add(road_gauche.CurrentTiles[i]);
                    }
                    road_haut.CurrentTiles.Add(tile_laid);
                    Move.list_of_struct_roads.Remove(road_gauche);
                    Move.nb_of_struct_roads--;
                }
            }
        }
        // cas avec 1 face Chemin déjà connecté à notre tile_laid
        else if (face_haut || face_gauche || face_bas || face_droite)
        {
            // Debug.Log("1 voisins");
            if (nom_struct_haut != "")
            {
                //ajout du Go à la struct du haut
                CurrentRoads cr = getStructByName(nom_struct_haut);
                //on ajoute la tuile au chemin qui nous connecte
                cr.CurrentTiles.Add(tile_laid);
                if (tile_laid.name.Contains("17") || tile_laid.name.Contains("22") || tile_laid.name.Contains("21"))
                {
                    ////Debug.Log("1 voisin et tuile 17 ou 22");
                    if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin)
                        createNewStruct(tile_laid, "_1");
                    if (tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin)
                        createNewStruct(tile_laid, "_2");
                    if (tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin)
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
            }
            if (nom_struct_gauche != "")
            {
                CurrentRoads cr = getStructByName(nom_struct_gauche);
                cr.CurrentTiles.Add(tile_laid);
                if (tile_laid.name.Contains("17") || tile_laid.name.Contains("22") || tile_laid.name.Contains("21"))
                {
                    if (tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin)
                        createNewStruct(tile_laid, "_0");
                    if (tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin)
                        createNewStruct(tile_laid, "_2");
                    if (tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin)
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
            }
            if (nom_struct_bas != "")
            {
                CurrentRoads cr = getStructByName(nom_struct_bas);
                cr.CurrentTiles.Add(tile_laid);
                if (tile_laid.name.Contains("17") || tile_laid.name.Contains("22") || tile_laid.name.Contains("21"))
                {
                    if (tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin)
                        createNewStruct(tile_laid, "_0");
                    if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin)
                        createNewStruct(tile_laid, "_1");
                    if (tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin)
                        createNewStruct(tile_laid, "_3");
                    Move.nb_of_struct_roads++;
                }
            }
            if (nom_struct_droite != "")
            {
                CurrentRoads cr = getStructByName(nom_struct_droite);
                cr.CurrentTiles.Add(tile_laid);
                if (tile_laid.name.Contains("17") || tile_laid.name.Contains("22") || tile_laid.name.Contains("21"))
                {
                    if (tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin)
                        createNewStruct(tile_laid, "_0");
                    if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin)
                        createNewStruct(tile_laid, "_1");
                    if (tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin)
                        createNewStruct(tile_laid, "_2");
                    Move.nb_of_struct_roads++;
                }
            }
        }
    }

    public void drawshit(GameObject tile_laid)
    {
        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;

        GameObject[] voisins = new GameObject[4];
        for(int i = 0; i < Move.plateau.Count; i++)
        {
            // Debug.Log("plat : " + Move.plateau[i]);
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y+1)
                    voisins[0] = Move.plateau[i]; // haut
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x-1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                    voisins[1] = Move.plateau[i]; // gauche
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y-1)
                    voisins[2] = Move.plateau[i]; // bas
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x+1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                    voisins[3] = Move.plateau[i]; // droite
        }
        if (tile_laid.name.Contains("10"))
        {
            //Debug.Log ("cas spé 10");
            //haut et gauche
            if (tile_laid.GetComponent<Constraints>().haut == Type_land.Ville && tile_laid.GetComponent<Constraints>().gauche == Type_land.Ville)
            {
                resetVisite();
                if (voisins[0] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[0]);
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    resetVisite();
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                        //Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                               // Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                            Debug.Log("j = "+j);
                            Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }

                            //Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
                resetVisite();
                if (voisins[1] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[1]);
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    resetVisite();
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                        //Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                                //Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                           // Debug.Log("j = "+j);
                          //  Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                          //  Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
            }
            //gaucge et bas
            if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Ville && tile_laid.GetComponent<Constraints>().bas == Type_land.Ville)
            {
                resetVisite();
                if (voisins[1] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[1]);
                    resetVisite();
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                      //  Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                                Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                          //  Debug.Log("j = "+j);
                          //  Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                         //   Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
                resetVisite();
                if (voisins[2] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[2]);
                    resetVisite();
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                       // Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                             //   Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                          //  Debug.Log("j = "+j);
                         //   Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                         //   Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
            }
            //bas et droite
            if (tile_laid.GetComponent<Constraints>().bas == Type_land.Ville && tile_laid.GetComponent<Constraints>().droite == Type_land.Ville)
            {
                resetVisite();
                if (voisins[2] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[2]);
                    resetVisite();
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                      //  Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                              //  Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                          //  Debug.Log("j = "+j);
                            //Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                           // Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }  
                }
                resetVisite();
                if (voisins[3] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[3]);
                    resetVisite();
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                       // Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                             //   Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                         //   Debug.Log("j = "+j);
                          //  Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                          //  Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
            }
            //haut et droite
            if (tile_laid.GetComponent<Constraints>().haut == Type_land.Ville && tile_laid.GetComponent<Constraints>().droite == Type_land.Ville)
            {
                resetVisite();
                if (voisins[0] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[0]);
                    resetVisite();
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                      //  Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                               // Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                          //  Debug.Log("j = "+j);
                           // Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                         //   Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
                resetVisite();
                if (voisins[3] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[3]);
                    resetVisite();
                    //Debug.Log("La partie haute est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                       // Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                      //          Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                      //      Debug.Log("j = "+j);
                       //     Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                        //    Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
            }
        }
        if (tile_laid.name.Contains("15"))
        {
            //Debug.Log ("cas spé 15");
            if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Ville)
            {
                resetVisite();
                if (voisins[1] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[1]);
                    resetVisite();
                    Debug.Log("La partie gauche est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                        affichage_score_demo(GameManager.Instance.Current_player,p);
                       // Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                          //      Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                          //  Debug.Log("j = "+j);
                        //    Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                        //    Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
                resetVisite();
                if (voisins[3] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[3]);
                    resetVisite();
                    Debug.Log("La partie droite est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                      //  Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                      //          Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                     //       Debug.Log("j = "+j);
                       //     Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                        //    Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
            }
            if (tile_laid.GetComponent<Constraints>().haut == Type_land.Ville)
            {
                resetVisite();
                if (voisins[0] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[0]);
                    resetVisite();
                    Debug.Log("La partie haute est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                      //  Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                       //         Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                          //  Debug.Log("j = "+j);
                         //   Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                            // Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
                resetVisite();
                if (voisins[2] != null)
                {
                    tile_laid.GetComponent<Constraints>().visite = true;
                    bool rep = townIsClosed(voisins[2]);
                    resetVisite();
                    Debug.Log("La partie bas est fermé ?" + rep);
                    if(rep == true )
                    {
                        int p = score(tile_laid)*2;
                      //  Debug.Log ("point " + p);
                        int max =0;
                        for(int i = 0; i < meeplePlayerTown.Length; i++)
                        {
                            if(max< meeplePlayerTown[i])
                            {
                                max = meeplePlayerTown[i];
                            }
                      //          Debug.Log("MAx meeple ville = "+max );
                        }
                        for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                        {
                        //    Debug.Log("j = "+j);
                      //      Debug.Log("meeple = "+ meeplePlayerTown[j]);
                            if(meeplePlayerTown[j] == max && max != 0)
                            {
                                Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                            }
                      //      Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                        }
                    }
                }
            }
        }
        if (!(tile_laid.name.Contains("10")) && !(tile_laid.name.Contains("15")))
        {
            resetVisite();
            bool rep = townIsClosed(tile_laid);
            //Debug.Log ("pas de cas spé");
            Debug.Log("La ville est fermé ?" + rep);
            if(rep == true )
            {
                resetVisite();
                // int p = score(tile_laid)*2;
                int p = score(tile_laid)*2;
                Debug.Log ("non special fermé");
                int max =0;
                for(int i = 0; i < meeplePlayerTown.Length; i++)
                {
                    if(max< meeplePlayerTown[i])
                    {
                        max = meeplePlayerTown[i];
                    }
                //        Debug.Log("MAx meeple ville = "+max );
                }
                // Debug.Log("Nombre de players : "+Move.list_of_struct_player.Count);
                for(int j =0; j < Move.list_of_struct_player.Count; j++ )
                {
                //    Debug.Log("j = "+j);
               //     Debug.Log("meeple = "+ meeplePlayerTown[j]);
                    if(meeplePlayerTown[j] == max && max != 0)
                    {
                        Debug.Log("max" + max + "joueur" + j);

                        Move.list_of_struct_player[j] = new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + p);
                    }
                    

                    Debug.Log("score = "+ Move.list_of_struct_player[j].points);
                }
                // Debug.Log("Id du joueur actuel : "+networkIdentity);
                affichage_score_demo(GameManager.Instance.Current_player,p);
            }
        }
    }

    public int getCompteur(PlayerManager player)
    {
        return player.compteur ;
    }


    //faire algo pour vérifier que la tuile pioché n'est pas une tuille spé 

    //algo de villes sur cas généraux
    public bool townIsClosed(GameObject tile_laid)
    {
        if(tile_laid.GetComponent<Constraints>().haut != Type_land.Ville && tile_laid.GetComponent<Constraints>().bas != Type_land.Ville && tile_laid.GetComponent<Constraints>().gauche != Type_land.Ville && tile_laid.GetComponent<Constraints>().droite != Type_land.Ville && tile_laid.GetComponent<Constraints>().haut != Type_land.Ville)
            return false;
        
        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;

        GameObject[] voisins = new GameObject[4];
        for(int i = 0; i < Move.plateau.Count; i++)
        {
            // Debug.Log("plat : " + Move.plateau[i]);
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y+1)
                    voisins[0] = Move.plateau[i]; // haut
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x-1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                    voisins[1] = Move.plateau[i]; // gauche
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y-1)
                    voisins[2] = Move.plateau[i]; // bas
                if(Move.plateau[i].GetComponent<Constraints>().coordX == x+1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                    voisins[3] = Move.plateau[i]; // droite
        }

        Debug.Log("Liste des voisins : haut : "+voisins[0]+" gauche : "+voisins[1]+" bas : "+voisins[2] + " droite : "+voisins[3]);
        // Debug.Log("Liste valeur visites : haut : "+voisins[0].GetComponent<Constraints>().visite+" gauche : "+voisins[1].GetComponent<Constraints>().visite+" bas : "+voisins[2].GetComponent<Constraints>().visite + " droite : "+voisins[3].GetComponent<Constraints>().visite);
                
        tile_laid.GetComponent<Constraints>().visite = true;
        // RpcEstVisite(tile_laid);
        // && tile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine pour distinguer des tuile 10 et 15
        if (tile_laid.GetComponent<Constraints>().haut == Type_land.Ville && (!tile_laid.name.Contains("10") || !tile_laid.name.Contains("15")))
        {
            if (voisins[0] == null)
                {
                    return false;
                }
           // Debug.Log("mon voisin du haut est non null");
            Debug.Log("viste haut : " + voisins[0].GetComponent<Constraints>().visite);
            if (!voisins[0].GetComponent<Constraints>().visite)
                {
                voisins[0].GetComponent<Constraints>().visite = true;
                // RpcEstVisite(voisins[0]);
                if(!townIsClosed(voisins[0]))
                    return false;
                }
        }
        if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Ville && (!tile_laid.name.Contains("10") || !tile_laid.name.Contains("15")))
        {
            if (voisins[1] == null)
                {
                    return false;
                }
          //  Debug.Log("mon voisin de gauche est non null");
            Debug.Log("viste gauche : " + voisins[1].GetComponent<Constraints>().visite);
            if (!voisins[1].GetComponent<Constraints>().visite)
                {
                voisins[1].GetComponent<Constraints>().visite = true;
                // RpcEstVisite(voisins[1]);
                if(!townIsClosed(voisins[1]))
                        return false;
                }
        }
        if (tile_laid.GetComponent<Constraints>().bas == Type_land.Ville && (!tile_laid.name.Contains("10") || !tile_laid.name.Contains("15")))
            {
                if (voisins[2] == null)
                    {
                        return false;
                    }
               // Debug.Log("mon voisin du bas est non null");
                Debug.Log("viste bas : " + voisins[2].GetComponent<Constraints>().visite);
                if (!voisins[2].GetComponent<Constraints>().visite)
                    {
                    voisins[2].GetComponent<Constraints>().visite = true;
                    // RpcEstVisite(voisins[2]);
                    if(!townIsClosed(voisins[2]))
                        return false;
                    }
            }
        if (tile_laid.GetComponent<Constraints>().droite == Type_land.Ville && (!tile_laid.name.Contains("10") || !tile_laid.name.Contains("15")))
            {
                if (voisins[3] == null)
                    {
                    return false;
                    }
              //  Debug.Log("mon voisin de droite est non null");
                Debug.Log("viste droite : " + voisins[3].GetComponent<Constraints>().visite);
                if (!voisins[3].GetComponent<Constraints>().visite)
                    {
                    voisins[3].GetComponent<Constraints>().visite = true;
                    // RpcEstVisite(voisins[3]);
                    if (!townIsClosed(voisins[3]))
                        return false;
                    }
            }
        return true;
    }

    [ClientRpc]
    void RpcEstVisite(GameObject go)
    {
        go.GetComponent<Constraints>().visite = true;
    }

    [Command]
    public void abbeyIsClose()
    {
        // parcours de plateau
        for (int i = 0; i < Move.abbeyes.Count; i++)
        {
            if (abbeyLaid(Move.abbeyes[i]))
            {
                affichage_score_demo(GameManager.Instance.Current_player,9);
            }



            // if (Move.abbeyes[i].GetComponent<Constraints>().meeple == 4)
            // {
            //     if (abbeyLaid(Move.abbeyes[i]))
            //     {
            //         for (int j = 0; j < Move.list_of_struct_player.Count; j++)
            //         {
            //             if(Move.list_of_struct_player[j].id == Move.abbeyes[i].GetComponent<Constraints>().id_joueur)
            //             {
            //                 Move.list_of_struct_player[j]= new Player(Move.list_of_struct_player[j].id, Move.list_of_struct_player[j].points + 9);
            //                 Move.abbeyes.RemoveAt(i);
            //                 affichage_score_demo(GameManager.Instance.Current_player,9);
            //             }
            //             // Debug.Log("add points");
                        
            //         }
            //     }
            //     // if (abbeyLaid(Move.abbeyes[i]) == false)
            //     // {
            //     //     player.points = counterAbbey;
            //     // }
            // }
        }
        // if (Move.plateau[i].milie == abbaye)
        //  je lance abbeyLaid(Move.plateau[i])
        // faire un tableau des abbayes
    }

    public bool abbeyLaid(GameObject tile_laid)
    {
        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;
        GameObject[] voisins = new GameObject[8];
        int Abbaye_compteur = 0;
        // pose abbaye direct avec les 8 tuiles
        for (int i = 0; i < Move.plateau.Count; i++)
        {
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y + 1)
                Abbaye_compteur++; // haut
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x - 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                Abbaye_compteur++; // gauche
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y - 1)
                Abbaye_compteur++; // bas
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x + 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                Abbaye_compteur++; // droite
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x + 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y + 1)
                Abbaye_compteur++; // haut droite
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x - 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y + 1)
                Abbaye_compteur++; // haut gauche
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x + 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y - 1)
                Abbaye_compteur++; // bas droite
            if (Move.plateau[i].GetComponent<Constraints>().coordX == x + 1 && Move.plateau[i].GetComponent<Constraints>().coordY == y - 1)
                Abbaye_compteur++; // bas gauche
        }
        if (Abbaye_compteur == 8)
        {
            // calcul de points
           // //Debug.Log("abbaye: " + tile_laid + " est complet");
            return true;
        }
        return false;
        // lancer la fonction sur tous les GO qui sont des abbayes (milieu = abbaye) -> tout le Move.plateau pour tous les tours
        // const int voisins[8][2] = {{-1,-1}, {-1,0}, {-1,1}, {0,-1}, {0,1}, {1,-1}, {1,0}, {1,1}};
    }

    public void placeMeeple(float x,float y, GameObject go)
    {
        float xTuile = x - go.GetComponent<Constraints>().coordX;
        float yTuile = y - go.GetComponent<Constraints>().coordY;

        // Haut
        if(xTuile>=0.5f && xTuile<0.6f && yTuile>=0.82f && yTuile<0.84f)
        {
            
            Debug.Log("meeple haut");
            
            go.GetComponent<Constraints>().meeple = 0;    
            if( go.GetComponent<Constraints>().haut == Type_land.Chemin)
            {
                for(int i = 0; i < Move.list_of_struct_roads.Count; i++)
                {
                    for (int j = 0; j < Move.list_of_struct_roads[i].CurrentTiles.Count; j++)
                    {
                        if (Move.list_of_struct_roads[i].CurrentTiles[j] == go)
                        {
                            for(int k = 0; k < Move.list_of_struct_player.Count; k++)
                            {
                                if(Move.list_of_struct_player[k].id==go.GetComponent<Constraints>().id_joueur)
                                    Move.list_of_struct_roads[i].lstPlayer.Add(Move.list_of_struct_player[k]); 
                            }
                        } 
                    }
                }
            }
            if (go.GetComponent<Constraints>().haut == Type_land.Ville)
            {

            }
        }
        
        // Gauche
        if(xTuile>=0.17f && xTuile<0.18f && yTuile>=0.5f && yTuile<0.6f)
        {
            Debug.Log("meeple gauche");
            go.GetComponent<Constraints>().meeple = 1; 
            if( go.GetComponent<Constraints>().gauche == Type_land.Chemin)
            {
                for(int i = 0; i < Move.list_of_struct_roads.Count; i++)
                {
                    for (int j = 0; j < Move.list_of_struct_roads[i].CurrentTiles.Count; j++)
                    {
                        if (Move.list_of_struct_roads[i].CurrentTiles[j] == go)
                        {
                            for(int k = 0; k < Move.list_of_struct_player.Count; k++)
                            {
                                if(Move.list_of_struct_player[k].id==go.GetComponent<Constraints>().id_joueur)
                                    Move.list_of_struct_roads[i].lstPlayer.Add(Move.list_of_struct_player[k]); 
                            }
                        } 
                    }
                }
            }   
        }

        // Bas
        if(xTuile>=0.5f && xTuile<0.6f && yTuile>=0.17f && yTuile<0.18f)
        {
            Debug.Log("meeple bas");
            go.GetComponent<Constraints>().meeple = 2;
            if( go.GetComponent<Constraints>().bas == Type_land.Chemin)
            {
                for(int i = 0; i < Move.list_of_struct_roads.Count; i++)
                {
                    for (int j = 0; j < Move.list_of_struct_roads[i].CurrentTiles.Count; j++)
                    {
                        if (Move.list_of_struct_roads[i].CurrentTiles[j] == go)
                        {
                            //Debug.Log("ajout meeple dans chemin");
                            //Debug.Log("placeMEeple: i " +i+ " j " +j);
                            for(int k = 0; k < Move.list_of_struct_player.Count; k++)
                            {
                                if(Move.list_of_struct_player[k].id==go.GetComponent<Constraints>().id_joueur)
                                    Move.list_of_struct_roads[i].lstPlayer.Add(Move.list_of_struct_player[k]); 
                            }
                        } 
                    }
                }
            }  

        }

        // Droite
        if(xTuile>=0.82f && xTuile<0.84f && yTuile>=0.5f && yTuile<0.6f)
        {
            Debug.Log("meeple droite");
            go.GetComponent<Constraints>().meeple = 3;
            if( go.GetComponent<Constraints>().droite == Type_land.Chemin)
            {
                for(int i = 0; i < Move.list_of_struct_roads.Count; i++)
                {
                    for (int j = 0; j < Move.list_of_struct_roads[i].CurrentTiles.Count; j++)
                    {
                        if (Move.list_of_struct_roads[i].CurrentTiles[j] == go)
                        {
                            //Debug.Log("ajout meeple dans chemin");
                            //Debug.Log("placeMEeple: i " +i+ " j " +j);
                            for(int k = 0; k < Move.list_of_struct_player.Count; k++)
                            {
                                if(Move.list_of_struct_player[k].id==go.GetComponent<Constraints>().id_joueur)
                                    Move.list_of_struct_roads[i].lstPlayer.Add(Move.list_of_struct_player[k]);

                                
                            }
                        } 
                    }
                }
            }
            if( go.GetComponent<Constraints>().droite == Type_land.Ville)
            {

            }
        }

        // Milieu
        if(xTuile>=0.5f && xTuile<0.6f && yTuile>=0.5f && yTuile<0.6f)
        {
            if (go.GetComponent<Constraints>().milieu == Type_land.Abbaye)
            {
                go.GetComponent<Constraints>().meeple = 4;
            }
            // if (go.GetComponent<Constraints>().droite == Type_land.);
        }

    }    

    // Demande du client au serveur du tirage d'une tuile de manière aléatoire
    [Command]
    public void CmdDealTiles()
    {
        // int randInt = 0;
        // génération aléaoire de la seed
        // System.Random rnd = new System.Random();
        if(CheckPick()){
            if(PIck.compteur == all_tiles.Count-1)
            {
                Debug.Log("AFFICHAGE DE FIN DE DEMO");
                Debug.Log("Faire return qui emmene autre part pour pas avoir de déconnexion de client");
                RpcChangeScenes("MainMenu");
            }
            else
            {
                GameObject tuilos = Instantiate(all_tiles[PIck.compteur]);
                // all_tiles.RemoveAt(PIck.compteur);
                // Debug.Log("Objet à faire spawn : " + tuilos);
                NetworkServer.Spawn(tuilos, connectionToClient);
                RpcShowTiles(tuilos, "Dealt");
                PIck.compteur++;
            }
            /*
            if (PIck.compteur == 0)
            {
                PIck.compteur++;
                GameObject tuilos = Instantiate(all_tiles[0]);
                all_tiles.RemoveAt(0);
                // Debug.Log("Objet à faire spawn : " + tuilos);
                NetworkServer.Spawn(tuilos, connectionToClient);
                RpcShowTiles(tuilos, "Dealt");

            }
            else
            {
                PIck.compteur++;
                // génération aléatoire d'un nombre parmi le nombre total de tuiles restantes dans la pioche
                randInt = rnd.Next(0, all_tiles.Count);
                // on pioche la tuile dans la liste all-tiles et puis on la supprime de la liste
                GameObject tuilos = Instantiate(all_tiles[randInt]);
                all_tiles.RemoveAt(randInt);
                ////Debug.Log("Objet à faire spawn : " + tuilos);
                // on spawn la tuile sur le serveur
                NetworkServer.Spawn(tuilos, connectionToClient);
                // on affiche la tuile chez tous les clients
                RpcShowTiles(tuilos, "Dealt");
            }
            */
            // Debug.Log("Compteur tuile : "+PIck.compteur);

        }
        
        // compteur++;
    }

    [ClientRpc]
    public void RpcChangeScenes(string scene){
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }

    bool CheckPick(){
        //Debug.Log("our turn "+ isOurTurn);
        if (isOurTurn){
            return true;
        }
        return false;
    }

    // Affiche les tuiles chez tous les clients
    [ClientRpc]
    void RpcShowTiles(GameObject go, string action)
    {
        if (action == "Dealt")
        {
            if (hasAuthority)
            {
                //go.name = "connard";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
                //Debug.Log("je suis dans rpc if");
                // Move.plateau.Add(go);
                //roadIsClosed(go);
                //Debug.Log(Move.plateau.Count);
            }
            else
            {
                //go.name = "le con";
                go.SetActive(true);
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
                //Debug.Log("je suis dans rpc else");
                // Move.plateau.Add(go);
                //roadIsClosed(go);
                //Debug.Log(Move.plateau.Count);
            }
        }
        else if (action == "Played")
        {

        }
    }

    // Demande de pose de Meeple au serveur
    [Command]
    public void CmdSpawnMeeple(float x, float y, GameObject go)
    {
        // Debug.Log("creation meeple ");
        compteurMeeple++;
        // instantiation d'un Prefab Meeple
        GameObject meeple = GameObject.Instantiate(Meeples);
        meeple.SetActive(true);
        NetworkServer.Spawn(meeple, connectionToClient);
        meeple.name = "Meeple" + compteurMeeple;
        ////Debug.Log("positions stars en x : " +transform.position.x + " et en y : " + transform.position.y);
        // on positionne le Meeple au dessus de la position de l'étoile sur laquelle on clique (ses positions sont les paramètres x et y)
        meeple.transform.position = new Vector3(x + 0.6f, y - 0.04f, 0.25f);
        placeMeeple(x,y, go);

        //meeple.transform.SetParent(GameObject.Find("Meeples").transform);
        MoveMeeple.rmStars(); ////////////////////////////////////////////////////////////////// à implémenter

        var allobjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject i in allobjects)
            // if (-0.05f<i.transform.position.z && i.transform.position.z<0.05f && i.transform.position.x == x + 0.6f && i.transform.position.y == y - 0.04f)
            // if(meeple.transform.position.x-0.7f < i.transform.position.x && i.transform.position.x < meeple.transform.position.x+0.7f && meeple.transform.position.y-0.7f < i.transform.position.y && i.transform.position.y < meeple.transform.position.y+0.7f && -0.05f < i.transform.position.z && i.transform.position.z < 0.05f)
            if((meeple.transform.position.x-(meeple.transform.position.x-0.35f))/2 < i.transform.position.x && i.transform.position.x < (meeple.transform.position.x+(meeple.transform.position.x+0.35f))/2 && (meeple.transform.position.y-(meeple.transform.position.x-(meeple.transform.position.x-0.35f))/2 < i.transform.position.y && i.transform.position.y < (meeple.transform.position.y+(meeple.transform.position.x+0.35f))/2 && -0.05f < i.transform.position.z && i.transform.position.z < 0.05f))

                Debug.Log(i.name);

        RpcShowMeeples(meeple, "Dealt");
    }

    // Affichage des Meeples chez tous les clients
    [ClientRpc]
    void RpcShowMeeples(GameObject go, string action)
    {
        //Debug.Log("je suis dans rpc");
        // Debug.Log("GameObject : "+ go);
        //Debug.Log("Action : "+ action);

        if (action == "Dealt")
        {
            if (hasAuthority)
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
        for (int i = 0; i < 5; i++)
        {
            // on vérifie les valeurs des positions et on crée les étoiles si les valeurs valent true
            if (tab[i])
            {
                GameObject star = Instantiate(Stars);

                star.SetActive(true);
                ////Debug.Log("Stars à faire spawn : " + star);
                //NetworkServer.Spawn(star, connectionToClient);
                star.name = "Star" + i;
                star.transform.position = new Vector3(x + tabPos[i].x, y + tabPos[i].y, -0.1f);
                star.transform.SetParent(GameObject.Find("Stars").transform);
                NetworkServer.Spawn(star, connectionToClient);
                RpcShowStars(star, "Dealt");
                ////Debug.Log("rpc fais buggué ? ");
            }
        }
    }

    // Affichage des étoiles chez tous les clients
    [ClientRpc]
    void RpcShowStars(GameObject go, string action)
    {
        ////Debug.Log("GO : " + go);
        if (action == "Dealt")
        {
            if (hasAuthority)
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

    // [Command]
    // public void rmStars()
    // {
    //     var allobjects = Resources.FindObjectsOfTypeAll<GameObject>();
    //     foreach (GameObject i in allobjects)
    //         if (i.name == "Star")
    //             NetworkServer.Destroy(i);
    // }


    public int score(GameObject tile_laid)
    {
        
        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;
        int p = 0;
        if(tile_laid.GetComponent<Constraints>().blason == true)
            p++;
        GameObject[] voisins = new GameObject[4];
        GameObject tmpTile_laid= tile_laid;
        for(int i = 0; i < Move.plateau.Count; i++)
        {
            //Debug.Log("plat : " + Move.plateau[i]);
            if(Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y+1)
                voisins[0] = Move.plateau[i]; // haut
            if(Move.plateau[i].GetComponent<Constraints>().coordX == x-1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[1] = Move.plateau[i]; // gauche
            if(Move.plateau[i].GetComponent<Constraints>().coordX == x && Move.plateau[i].GetComponent<Constraints>().coordY == y-1)
                voisins[2] = Move.plateau[i]; // bas
            if(Move.plateau[i].GetComponent<Constraints>().coordX == x+1 && Move.plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[3] = Move.plateau[i]; // droite
        }

        tmpTile_laid.GetComponent<Constraints>().visite = true;
        if (tmpTile_laid.GetComponent<Constraints>().haut == Type_land.Ville && tmpTile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
        {
            Debug.Log("yo");
            if(tmpTile_laid.GetComponent<Constraints>().meeple == 0)
            {
                for(int j=0; j<Move.list_of_struct_player.Count; j++)
                {
                    if(tmpTile_laid.GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                    {
                        meeplePlayerTown[j]++;
                    }
                }
            }
            if(voisins[0] != null &&  !(voisins[0].GetComponent<Constraints>().visite) && (voisins[0].name.Contains("10") || voisins[0].name.Contains("15")))
            {
                if(voisins[0].GetComponent<Constraints>().meeple == 2)
                {
                    for(int j=0; j<Move.list_of_struct_player.Count; j++)
                    {
                        if(voisins[0].GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                        {
                            meeplePlayerTown[j]++;
                        }
                    }            
                }
            }
            else if (voisins[0] != null && !(voisins[0].GetComponent<Constraints>().visite))
            {
                ////Debug.Log("haut +");
                voisins[0].GetComponent<Constraints>().visite = true;
                p += score(voisins[0]);
            }    
        }
        if (tmpTile_laid.GetComponent<Constraints>().gauche == Type_land.Ville && tmpTile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
        {
            if(tmpTile_laid.GetComponent<Constraints>().meeple == 1)
            {
                for(int j=0; j<Move.list_of_struct_player.Count; j++)
                {
                    if(tmpTile_laid.GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                    {
                        meeplePlayerTown[j]++;
                    }
                }
            }
            if(voisins[1] != null &&  !(voisins[1].GetComponent<Constraints>().visite) && (voisins[1].name.Contains("10") || voisins[1].name.Contains("15")))
            {
                if(voisins[1].GetComponent<Constraints>().meeple == 3)
                {
                    for(int j=0; j<Move.list_of_struct_player.Count; j++)
                    {
                        if(voisins[1].GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                        {
                            meeplePlayerTown[j]++;
                        }
                    }            
                }
            }
            else if (voisins[1] != null && !(voisins[1].GetComponent<Constraints>().visite))
            {
                ////Debug.Log("gauche +");
                voisins[1].GetComponent<Constraints>().visite = true;
                p += score(voisins[1]);
            }    
        }
        if (tmpTile_laid.GetComponent<Constraints>().bas == Type_land.Ville && tmpTile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
        {
            if(tmpTile_laid.GetComponent<Constraints>().meeple == 2)
            {
                for(int j=0; j<Move.list_of_struct_player.Count; j++)
                {
                    if(tmpTile_laid.GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                    {
                        meeplePlayerTown[j]++;
                    }
                }
            }
            if((voisins[2] != null &&  !(voisins[2].GetComponent<Constraints>().visite)) && (voisins[2].name.Contains("10") || voisins[2].name.Contains("15")))
            {
                //Debug.Log(" merde +");
                if(voisins[2].GetComponent<Constraints>().meeple == 0)
                {
                    for(int j=0; j<Move.list_of_struct_player.Count; j++)
                    {
                        if(voisins[2].GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                        {
                            meeplePlayerTown[j]++;
                        }
                    }            
                }
            }
            
            else if (voisins[2] != null && !(voisins[2].GetComponent<Constraints>().visite))
            {
               // Debug.Log(" on continue en bas +");
                voisins[2].GetComponent<Constraints>().visite = true;

                p += score(voisins[2]);
            }
          //  Debug.Log("bientot on continue en bas +");
          //  Debug.Log("visite voisin " + voisins[2].GetComponent<Constraints>().visite);
        }
        if (tmpTile_laid.GetComponent<Constraints>().droite == Type_land.Ville && tmpTile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
        {
            if(tmpTile_laid.GetComponent<Constraints>().meeple == 3)
            {
                for(int j=0; j<Move.list_of_struct_player.Count; j++)
                {
                    if(tmpTile_laid.GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                    {
                        meeplePlayerTown[j]++;
                    }
                }
            }
            if(voisins[3] != null &&  !(voisins[3].GetComponent<Constraints>().visite) && (voisins[3].name.Contains("10") || voisins[3].name.Contains("15")))
            {
                if(voisins[3].GetComponent<Constraints>().meeple == 1)
                {
                    for(int j=0; j<Move.list_of_struct_player.Count; j++)
                    {
                        if(voisins[3].GetComponent<Constraints>().id_joueur == Move.list_of_struct_player[j].id)
                        {
                            meeplePlayerTown[j]++;
                        }
                    }            
                }
            }
            else if (voisins[3] != null && !(voisins[3].GetComponent<Constraints>().visite))
            {
              //  Debug.Log("droite +");
                voisins[3].GetComponent<Constraints>().visite = true;
                
                p += score(voisins[3]);
            }    
        }
       // Debug.Log("merci");
        return p+ 1;
    }

    
    [ClientRpc]
    void RpcShowGrid(GameObject go, string action, int x, int y)
    {
        //Debug.Log("ALOOO");
        if (action == "Dealt")
        {
            if (hasAuthority)
            {
                Debug.Log("has authority " +go);
                //go.name = "connard";
                go.transform.SetParent(GameObject.Find("Grid").transform);
                go.name = x + "/" + y;
                Vector2 v = new Vector2(x + 0.5f, y + 0.5f);
                go.transform.position = v;
                go.SetActive(true);
                
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
            }
            else
            {
                //go.name = "le con";
                Debug.Log("has NO authority " +go);
                go.transform.SetParent(GameObject.Find("Grid").transform);
                go.name = x + "/" + y;
                Vector2 v = new Vector2(x + 0.5f, y + 0.5f);
                go.transform.position = v;
                go.SetActive(true);
                
                //go.transform.SetParent(GameObject.Find("Tiles").transform, false);
            }
        }
        else if (action == "Played")
        {

        }
        // Debug.Log("CHANGE TON NOM : " + go.name);
    }

    [Command]
    public void CmdSpawnGrid(int nbTuiles)
    {
        //Debug.Log($"ya zebi");
        for (int x = 0; x < nbTuiles; x++)
        {
            for (int y = 0; y < nbTuiles; y++)
            {
                GameObject clone = Instantiate(grid);
                //clone.transform.SetParent(GameObject.Find("Grid").transform);
                //clone.name = x + "/" + y;
                //Vector2 v = new Vector2(x + 0.5f, y + 0.5f);
                //clone.transform.position = v;
               
                // Debug.Log("spawn 1");
                NetworkServer.Spawn(clone, connectionToClient);
                RpcShowGrid(clone,"Dealt",x,y);
            }
        }
        // Alignement de la caméra pour se trouver au milieu de la grille
        Vector3 vec = new Vector3((float)Decimal.Divide(nbTuiles, 2),
                                  (float)Decimal.Divide(nbTuiles, -4), -5);
        Camera.main.transform.position = vec;
    }
    

    [Command]
    public void CmdDealMove(GameObject disapear, Type_land h, Type_land b, Type_land g, Type_land d, Type_land m, int x, int y){
        // Debug.Log($"disa{disapear}");
        NetworkServer.Spawn(disapear, connectionToClient);
        RpcShowMove(disapear, h, b, g, d, m, x, y);
    }


    [ClientRpc]
    void RpcShowMove(GameObject disapear, Type_land h, Type_land b, Type_land g, Type_land d, Type_land m, int x, int y){
        // Debug.Log("Debug yes : "+disapear);
        // Debug.Log("Debug yes : "+disapear.GetComponent<Constraints>().haut);
        // Debug.Log("Debug yes : "+disapear.GetComponent<Constraints>().bas);
        // Debug.Log("Debug haut : "+disapear.GetComponent<Constraints>().gauche);
        // Debug.Log("Debug yes : "+disapear.GetComponent<Constraints>().droite);
        // Debug.Log("Debug yes : "+disapear.GetComponent<Constraints>().milieu);
        // Debug.Log("Debug haut : "+disapear.GetComponent<Constraints>().coordX);
        // Debug.Log("Debug haut : "+disapear.GetComponent<Constraints>().coordY);
        // Debug.Log("ok : "+h);
        // Debug.Log("ok : "+g);
        // Debug.Log("ok : "+b);
        // Debug.Log("ok : "+d);
        // Debug.Log("ok : "+m);
        // Debug.Log("ok : "+x);
        // Debug.Log("ok : "+y);
        disapear.GetComponent<Constraints>().haut =
            h;
        disapear.GetComponent<Constraints>().bas =
            b;
        disapear.GetComponent<Constraints>().gauche =
            g;
        disapear.GetComponent<Constraints>().droite =
            d;
        disapear.GetComponent<Constraints>().milieu =
            m;
        disapear.GetComponent<Constraints>().coordX = x;
        disapear.GetComponent<Constraints>().coordY = y;
        // Move.plateau.Add(disapear); /////////////////////// ça ajoute la grid et pas la tile !
    }

    [Command]
    public void CmdDealCoord(GameObject go, Type_land h, Type_land b, Type_land g, Type_land d, Type_land m, int x, int y)
    {
        RpcShowCoord(go, h, b, g, d, m, x, y);
    }

    [ClientRpc]
    void RpcShowCoord(GameObject go, Type_land h, Type_land b, Type_land g, Type_land d, Type_land m, int x, int y)
    {
        go.GetComponent<Constraints>().haut =
            h;
        go.GetComponent<Constraints>().bas =
            b;
        go.GetComponent<Constraints>().gauche =
            g;
        go.GetComponent<Constraints>().droite =
            d;
        go.GetComponent<Constraints>().milieu =
            m;
        go.GetComponent<Constraints>().coordX = x;
        go.GetComponent<Constraints>().coordY = y;
        Move.plateau.Add(go);
    }

    [Command]
    public void CmdRotate(GameObject go, Type_land h, Type_land b, Type_land g, Type_land d)
    {
        RpcRotate(go,h,b,g,d);
    }

    [ClientRpc]
    void RpcRotate(GameObject go, Type_land h, Type_land b, Type_land g, Type_land d)
    {
        tile_type i = test(go);
        i.haut = h;
        i.gauche = g;
        i.bas = b;
        i.droite = d;

        go.GetComponent<Constraints>().haut = h;
        go.GetComponent<Constraints>().bas = b;
        go.GetComponent<Constraints>().gauche = g;
        go.GetComponent<Constraints>().droite = d;
    }

    [Command]
    public void CmdSetLaid(GameObject go)
    {
        RpcSetLaid(go);
    }

    [ClientRpc]
    void RpcSetLaid(GameObject go)
    {
        go.GetComponent<Constraints>().laid = true;
    }

    tile_type test(GameObject go) 
    {
        var mm = go.GetComponents(typeof(Component));
        foreach(object i in mm)
        {
        if (i.GetType().Name.Contains("tile_type"))
            return (tile_type)i;
        }
        return null;
    }

        dynamic test_point(GameObject go) 
    {
        var mm = go.GetComponents(typeof(Component));
        foreach(object i in mm)
        {
        if (i.GetType().Name.Contains("Points"))
            return i;
        }
        return null;
    }
}
