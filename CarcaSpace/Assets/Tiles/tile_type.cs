using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror ;




public abstract class tile_type : NetworkBehaviour
{
    public Type_land haut;
    public Type_land gauche;
    public Type_land bas;
    public Type_land droite;
    public Type_land milieu;
    public int coordX;
    public int coordY;
    public rotateZ r;
    public static int nbrTuile = 72; // sans rivière : 72, avec rivière : 84
    public static bool finish = false;
    public static bool blason;
    public PlayerManager PlayerManager;


    

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<rotateZ>();
        /*
        haut = Type_land.Plaine;
        gauche = Type_land.Chemin;
        bas = Type_land.Plaine;
        droite = Type_land.Chemin;
        milieu = Type_land.Chemin;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("left") && r!=null)
        {
            if(!(r.leve || r.couche || r.tourne)){
                rotate_left();
            }
            
        }
        if(Input.GetKeyDown("right") && r!=null)
        {
            if(!(r.leve || r.couche || r.tourne)){
                rotate_right();
            }
        }
    }

    protected void rotate_left()
    {
        Type_land cp = haut;
        haut = droite;
        droite = bas;
        bas = gauche;
        gauche = cp;

        this.GetComponent<Constraints>().haut = haut;
        this.GetComponent<Constraints>().gauche = gauche;
        this.GetComponent<Constraints>().bas = bas;
        this.GetComponent<Constraints>().droite = droite;
        bool[] copie = this.GetComponent<Constraints>().posePossible;
        this.GetComponent<Constraints>().posePossible = new bool[5]{copie[3], copie[0], copie[1], copie[2], copie[4]};
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.CmdRotate(gameObject,haut,bas,gauche,droite);
        //this.GetComponent<Constraints>().milieu = milieu;
    }

    protected void rotate_right()
    {
        // Debug.Log("type : "+test());
        Type_land cp = haut;
        haut = gauche;
        gauche = bas;
        bas = droite;
        droite = cp;

        this.GetComponent<Constraints>().haut = haut;
        this.GetComponent<Constraints>().gauche = gauche;
        this.GetComponent<Constraints>().bas = bas;
        this.GetComponent<Constraints>().droite = droite;
        bool[] copie = this.GetComponent<Constraints>().posePossible;
        this.GetComponent<Constraints>().posePossible = new bool[5]{copie[1], copie[2], copie[3], copie[0], copie[4]};
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        PlayerManager.CmdRotate(gameObject,haut,bas,gauche,droite);
        //this.GetComponent<Constraints>().milieu = milieu;
    }

    //public abstract int getNbrTuile();

    //public abstract void decrementNbrTuile();

    //public abstract bool getFinish();

    //public abstract void changeFinish();
    

}
