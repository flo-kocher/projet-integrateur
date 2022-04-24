using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_type_7 : tile_type
{
    public static bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<rotateZ>();
        haut = Type_land.Ville;
        gauche = Type_land.Ville;
        bas = Type_land.Chemin;
        droite = Type_land.Ville;
        milieu = Type_land.Continue;
        blason = true;
        
        this.GetComponent<Constraints>().haut = haut;
        this.GetComponent<Constraints>().gauche = gauche;
        this.GetComponent<Constraints>().bas = bas;
        this.GetComponent<Constraints>().droite = droite;
        this.GetComponent<Constraints>().milieu = milieu;
        this.GetComponent<Constraints>().posePossible = new bool[5] {true, true, true, true, true};
        this.GetComponent<Constraints>().carrefour = true;
        this.GetComponent<Constraints>().estCheminFermant = true; //g
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

    // public override bool getFinish(){
    //     return finish;
    // }

    // public override void changeFinish(){
    //     finish = true;
    // }
}
