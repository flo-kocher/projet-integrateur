using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_type_23 : tile_type
{
    public static bool finish = false;
    public static int nbrTuile = 8;

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<rotateZ>();
        haut = Type_land.Chemin;
        gauche = Type_land.Plaine;
        bas = Type_land.Chemin;
        droite = Type_land.Plaine;
        milieu = Type_land.Rien;
        blason = false;
        
        this.GetComponent<Constraints>().haut = haut;
        this.GetComponent<Constraints>().gauche = gauche;
        this.GetComponent<Constraints>().bas = bas;
        this.GetComponent<Constraints>().droite = droite;
        this.GetComponent<Constraints>().milieu = milieu;
        this.GetComponent<Constraints>().posePossible = {true, false, true, false, false}
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

    public override int getNbrTuile(){
        return nbrTuile;
    }

    public override void decrementNbrTuile(){
        nbrTuile--;
    }

    public override bool getFinish(){
        return finish;
    }

    public override void changeFinish(){
        finish = true;
    }
}
