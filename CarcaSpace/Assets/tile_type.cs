using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class tile_type : MonoBehaviour
{

    
    
    public Type_land haut;
    public Type_land gauche;
    public Type_land bas;
    public Type_land droite;
    public Type_land milieu;
    public int coordX;
    public int coordY;
    public rotateZ r;
    public static int nbrTuile = 30;
    public static bool finish = false;

    

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<rotateZ>();
        haut = Type_land.Plaine;
        gauche = Type_land.Chemin;
        bas = Type_land.Plaine;
        droite = Type_land.Chemin;
        milieu = Type_land.Chemin;
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
    }

    protected void rotate_right()
    {
        Type_land cp = haut;
        haut = gauche;
        gauche = bas;
        bas = droite;
        droite = cp;
    }

    public abstract int getNbrTuile();

    public abstract void decrementNbrTuile();

    public abstract bool getFinish();

    public abstract void changeFinish();
    

}
