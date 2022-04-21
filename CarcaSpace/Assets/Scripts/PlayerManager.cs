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

    // liste des abbayes posées
    public List<GameObject> abbeyes = new List<GameObject>();

    //structure de chemins
    public struct CurrentRoads
    {
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

        // méthode qui check qui la liste des tuiles est fermé ou pas
        public bool isClosed()
        {
            return true;
        }
    }

    public int nb_of_struct_roads;
    public List<CurrentRoads> list_of_struct_roads = new List<CurrentRoads>();

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
    public List<GameObject>  instatiateTiles()
    {
        //GameObject go = GameObject.Find("Temp");
        //GameObject tmp = GameObject.Instantiate(temp);
        int x = 0 ;
        for(int i =0 ;i< 25; i++)
        {
            switch(i)
            {
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
                    for(x=0; x<=1; x++)
                    {
                        all_tiles.Add(TileType2);
                    }
                    break;
                case 3 : 
                    //tmp.AddComponent<tile_type_3>();
                    for(x=0; x<=3; x++)
                    {
                        all_tiles.Add(TileType3);
                    }
                    break;
                case 4 : 
                    //tmp.AddComponent<tile_type_4>();
                    for(x=0; x<=2; x++)
                    {
                        all_tiles.Add(TileType4);
                    }
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
                    for(x=0; x<=1; x++)
                    {
                        all_tiles.Add(TileType7);
                    }
                    break;
                case 8 : 
                    // tmp.AddComponent<tile_type_8>();
                    for(x=0; x<=2; x++)
                    {
                        all_tiles.Add(TileType8);
                    }
                    break;
                case 9 : 
                    // tmp.AddComponent<tile_type_9>();
                    for(x=0; x<=1; x++)
                    {
                        all_tiles.Add(TileType9);
                    }
                    break;
                case 10 : 
                    //tmp.AddComponent<tile_type_10>();
                    for(x=0; x<=1; x++)
                    {
                        all_tiles.Add(TileType10);
                    }
                    break;
                case 11 : 
                    //tmp.AddComponent<tile_type_11>();
                    for(x=0; x<=2; x++)
                    {
                        all_tiles.Add(TileType11);
                    }
                    break;
                case 12 : 
                    //tmp.AddComponent<tile_type_12>();
                    for(x=0; x<=1; x++)
                    {
                        all_tiles.Add(TileType12);
                    }
                    break;
                case 13 : 
                    // tmp.AddComponent<tile_type_13>();
                    all_tiles.Add(TileType13); 
                    break;                       
                case 14 :
                    for(x=0;x<=1;x++)
                    {
                        //tmp.AddComponent<tile_type_14>();
                        all_tiles.Add(TileType14);
                    }
                    break;
                    /*
                case 15 :
                    for(x=0;x<=2;x++)
                    {
                        //tmp.AddComponent<tile_type_15>();
                        all_tiles.Add(TileType15);
                    }
                    break;
                    */
                case 16 :
                    for(x=0;x<=4;x++)
                    {
                        //tmp.AddComponent<tile_type_16>();
                        all_tiles.Add(TileType16);
                    }
                    break;
                    /*
                case 17 :
                    for(x=0;x<=2;x++)
                    {
                        //tmp.AddComponent<tile_type_17>();
                        all_tiles.Add(TileType17);
                    }
                    break;
                    */
                case 18 :
                    for(x=0;x<=2;x++)
                    {
                        //tmp.AddComponent<tile_type_18>();
                        all_tiles.Add(TileType18);
                    } 
                    break;
                case 19 :
                    for(x=0;x<=2;x++)
                    {
                        //tmp.AddComponent<tile_type_19>();
                        all_tiles.Add(TileType19);
                    } 
                    break;
                //a revoir
                case 20 :
                    for(x=0;x<=3;x++)
                    {
                        //tmp.AddComponent<tile_type_20>();
                        all_tiles.Add(TileType20);
                    } 
                    break;
                    /*
                case 21 :
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
                    */
                case 23:
                    for(x=0;x<=7;x++)
                    {
                        // tmp.AddComponent<tile_type_23>();
                        all_tiles.Add(TileType23);
                    }  
                    break;
                case 24 :
                    for(x=0;x<=8;x++)
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
        nb_of_struct_roads = 0;
    }

    public void resetVisite()
    {
        for(int i = 0; i < plateau.Count; i++)
        {
            plateau[i].GetComponent<Constraints>().visite = false;
        }
    }

    public void roadIsClosed_Struct(GameObject tile_laid)
    {
        if(tile_laid.GetComponent<Constraints>().haut != Type_land.Chemin && tile_laid.GetComponent<Constraints>().bas != Type_land.Chemin && tile_laid.GetComponent<Constraints>().gauche != Type_land.Chemin && tile_laid.GetComponent<Constraints>().droite != Type_land.Chemin && tile_laid.GetComponent<Constraints>().haut != Type_land.Chemin)
        {
            Debug.Log("Pas de composante Chemin sur ma tuile");
            return;
        }

        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;

    GameObject[] voisins = new GameObject[4];
    for(int i = 0; i < plateau.Count; i++)
    {
        //Debug.Log("plat : " + plateau[i]);
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y+1)
                voisins[0] = plateau[i]; // haut
            if(plateau[i].GetComponent<Constraints>().coordX == x-1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[1] = plateau[i]; // gauche
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y-1)
                voisins[2] = plateau[i]; // bas
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y)
                voisins[3] = plateau[i]; // droite
        }
        Debug.Log("La tuile "+ tile_laid + " a comme voisins : haut "+voisins[0]+" gauche "+voisins[1]+" bas "+voisins[2]+" droite "+voisins[3]);

        // il n'y a pas de connexion de chemin, on crée une nouvelle structure
        if((tile_laid.GetComponent<Constraints>().haut != Type_land.Chemin || voisins[0] == null) && (tile_laid.GetComponent<Constraints>().gauche != Type_land.Chemin || voisins[1] == null) && (tile_laid.GetComponent<Constraints>().bas != Type_land.Chemin || voisins[2] == null) && (tile_laid.GetComponent<Constraints>().droite != Type_land.Chemin || voisins[3] == null))
        {
            //créer une nouvelle structure
            CurrentRoads road = new CurrentRoads("Road "+nb_of_struct_roads,tile_laid);
            // ajout de la stucture à la liste_des_struct
            list_of_struct_roads.Add(road);
            Debug.Log("crea struct 1 " + list_of_struct_roads.Count);
            //Debug.Log("struct-"+road.Name+"created");
        }


        // condition noir
        if(tile_laid.GetComponent<Constraints>().haut == Type_land.Chemin && voisins[0] != null && voisins[0].GetComponent<Constraints>().bas == Type_land.Chemin)
        {
            Debug.Log("cas haut");
            //rajouter tile_laid à la structure du voisin du haut
            //CurrentRoads cp;
            for(int i = 0; i < list_of_struct_roads.Count; i++)
            {
                CurrentRoads rd = list_of_struct_roads[i];
                for (int j = 0; j < rd.CurrentTiles.Count ; j++)
                {
                    if(rd.CurrentTiles[j] == voisins[0])
                    {
                        //cp = rd;
                        rd.CurrentTiles.Add(tile_laid);
                        break;
                    }
                }
            }
        } 
        
        
        if(tile_laid.GetComponent<Constraints>().gauche == Type_land.Chemin && voisins[1] != null && voisins[1].GetComponent<Constraints>().droite == Type_land.Chemin)
        {
            Debug.Log("cas gauche");
            // rajouter tile_laid à la structure du voisin de gauche.
            for(int i = 0; i < list_of_struct_roads.Count; i++)
            {
                CurrentRoads rd = list_of_struct_roads[i];
                for (int j = 0; j < rd.CurrentTiles.Count ; j++)
                {
                    Debug.Log("parcours d'un elt dans ma liste de tiles");
                    if(rd.CurrentTiles[j] == voisins[1])
                    {
                        //cp = rd;
                        rd.CurrentTiles.Add(tile_laid);
                        break;
                    }
                }
            }
        } 

        if(tile_laid.GetComponent<Constraints>().bas == Type_land.Chemin && voisins[2] != null && voisins[2].GetComponent<Constraints>().haut == Type_land.Chemin)
        {
            Debug.Log("cas bas");
            //rajouter tile_laid à la structure du voisin du bas
            for(int i = 0; i < list_of_struct_roads.Count; i++)
            {
                CurrentRoads rd = list_of_struct_roads[i];
                for (int j = 0; j < rd.CurrentTiles.Count ; j++)
                {
                    if(rd.CurrentTiles[j] == voisins[2])
                    {
                        //cp = rd;
                        rd.CurrentTiles.Add(tile_laid);
                        break;
                    }        
                }
            }
        } 

        if(tile_laid.GetComponent<Constraints>().droite == Type_land.Chemin && voisins[3] != null && voisins[3].GetComponent<Constraints>().droite == Type_land.Chemin)
        {
            Debug.Log("cas droite");
            //rajouter tile_laid à la structure du voisin du droite
            for(int i = 0; i < list_of_struct_roads.Count; i++)
            {
                CurrentRoads rd = list_of_struct_roads[i];
                for (int j = 0; j < rd.CurrentTiles.Count ; j++)
                {
                    if(rd.CurrentTiles[j] == voisins[3])
                    {
                        //cp = rd;
                        rd.CurrentTiles.Add(tile_laid);
                        break;
                    }                
                }
            }
        }
    } 

    // condition verte
    // parcours la liste des struct actuelle
    




    /*
        virer l'ajout du go dans les deux structures et à la place juste regarder
        si le go.gauche appartient à une structure et si le go.droite appartient à une autre structure

        si go.gauche et go.droite appartient à la même alors le chemin est complet et forme une boucle
        et sinon on fusionne juste les deux structure en une seule



        il faut qu'on mette 3 if()

        1er if qui fait le cas de la boucle sur elle -même donc si le go.gauche et le go.droite appartienne à la même structure
                du coup faire le calculer de point avec les meeples etc.. et détruit la structure

        2è if qui fait le cas de la fusion qui va fusionner les deux structures en une seule
                du coup vérifier si on se retrouve avec 2 tuiles qui ferment/ouvrent des chemins et du coup calculer les points et détruire

        3è if cas général où la tuile va juste se rajouter à une structure existante ou en créer une nouvelle
                vérifier aussi si deux tuiles fermantes et calcule de points


        
 



    */
    int count = 0;
    int indice_list_1 = -1;

        // condition verte
        // parcours la liste des struct actuelle
        
        int count = 0;
        int indice_list_1 = -1;

        // On parcours la liste_des_structs actu...
        for( int i = 0; i< list_of_struct_roads.Count; i++)
        {
            // si notre tuile est commun a tous les structures
            // alors on compte

            /* on recupere un road */
            CurrentRoads rd = list_of_struct_roads[i];
            
            /* pour chaque tuile appartenant a la liste de ce road...*/
            for (int j = 0; j < rd.CurrentTiles.Count ; j++)
            {
                /* si on trouve notre tuile alors... count++ */
                if(rd.CurrentTiles[j] == tile_laid)
                {
                    Debug.Log("dans count ++");
                    // si notre tuile appartient a un road_X
                    // on count++ puis on save l'indice du road_X
                    count ++;
                    if(count == 1)
                        indice_list_1 = i;
                }
            }

            Debug.Log("count : "+count);
            if(count == 2)
            {
                for(int k = 0; k < list_of_struct_roads[indice_list_1].CurrentTiles.Count; k++)
                {
                    list_of_struct_roads[i].CurrentTiles.Add(list_of_struct_roads[indice_list_1].CurrentTiles[k]);
                }
                list_of_struct_roads[i].CurrentTiles.Remove(tile_laid);
                list_of_struct_roads.RemoveAt(indice_list_1);
                break;
            }
        }
        return;
    }

    //faire algo pour vérifier que la tuile pioché n'est pas une tuille spé 

    // rajouter condition pour les tuiles 10 et 15 qu'il faut lancer l'algo 2 fois
    public bool townIsClosed(GameObject tile_laid)
    {
        if(tile_laid.GetComponent<Constraints>().haut != Type_land.Ville && tile_laid.GetComponent<Constraints>().bas != Type_land.Ville && tile_laid.GetComponent<Constraints>().gauche != Type_land.Ville && tile_laid.GetComponent<Constraints>().droite != Type_land.Ville && tile_laid.GetComponent<Constraints>().haut != Type_land.Ville)
            return false;

        
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

        tile_laid.GetComponent<Constraints>().visite = true;
        // && tile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine pour distinguer des tuile 10 et 15
        if (tile_laid.GetComponent<Constraints>().haut == Type_land.Ville && tile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
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
        if (tile_laid.GetComponent<Constraints>().gauche == Type_land.Ville && tile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
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
        if (tile_laid.GetComponent<Constraints>().bas == Type_land.Ville && tile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
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
        if (tile_laid.GetComponent<Constraints>().droite == Type_land.Ville && tile_laid.GetComponent<Constraints>().milieu != Type_land.Plaine)
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

    public void abbeyIsClose()
    {
        //ajouter dès qu'on pick une tuile ou dès qu'on la pose
        //si son milieu est == Abbaye
        // -> on la rajoute à une liste d'abbaye
        //et ensuite on lance à chaque tour, abbeyIsClose sur seulement cette liste
        

        // parcours de plateau
        for(int i = 0; i < abbeyes.Count; i++)
        {
           abbeyLaid(abbeyes[i]);        
        }
        // if (plateau[i].milie == abbaye)
            //  je lance abbeyLaid(plateau[i])
        // faire un tableau des abbayes
    }

    public bool abbeyLaid(GameObject tile_laid)
    {
        int x = tile_laid.GetComponent<Constraints>().coordX;
        int y = tile_laid.GetComponent<Constraints>().coordY;
        GameObject[] voisins = new GameObject[8];
        int compteur = 0;
        // pose abbaye direct avec les 8 tuiles
        for(int i = 0; i < plateau.Count; i++)
        {
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y+1)
                compteur++; // haut
            if(plateau[i].GetComponent<Constraints>().coordX == x-1 && plateau[i].GetComponent<Constraints>().coordY == y)
                compteur++; // gauche
            if(plateau[i].GetComponent<Constraints>().coordX == x && plateau[i].GetComponent<Constraints>().coordY == y-1)
                compteur++; // bas
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y)
                compteur++; // droite
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y+1)
                compteur++; // haut droite
            if(plateau[i].GetComponent<Constraints>().coordX == x-1 && plateau[i].GetComponent<Constraints>().coordY == y+1)
                compteur++; // haut gauche
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y-1)
                ompteur++; // bas droite
            if(plateau[i].GetComponent<Constraints>().coordX == x+1 && plateau[i].GetComponent<Constraints>().coordY == y-1)
                compteur++; // bas gauche
        }
        if (compteur == 8)
        {
            // calcul de points
            Debug.Log("abbaye: " + tile_laid + " est complet");
        }
        // lancer la fonction sur tous les GO qui sont des abbayes (milieu = abbaye) -> tout le plateau pour tous les tours
        // const int voisins[8][2] = {{-1,-1}, {-1,0}, {-1,1}, {0,-1}, {0,1}, {1,-1}, {1,0}, {1,1}};
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
        else 
        {
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
    public void CmdSpawnMeeple(float x, float y)
    {
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
        for (int i = 0; i < 5; i++)
        {
            // on vérifie les valeurs des positions et on crée les étoiles si les valeurs valent true
            if (tab[i]) 
            {
                GameObject star = Instantiate(Stars);
                
                star.SetActive(true);
                //Debug.Log("Stars à faire spawn : " + star);
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
        //Debug.Log("GO : " + go);
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