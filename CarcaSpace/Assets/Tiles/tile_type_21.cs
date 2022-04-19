using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_type_21 : tile_type
{
    public static bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<rotateZ>();
        haut = Type_land.Chemin;
        gauche = Type_land.Chemin;
        bas = Type_land.Chemin;
        droite = Type_land.Chemin;
        milieu = Type_land.Rien;
        blason = false;
        
        this.GetComponent<Constraints>().haut = haut;
        this.GetComponent<Constraints>().gauche = gauche;
        this.GetComponent<Constraints>().bas = bas;
        this.GetComponent<Constraints>().droite = droite;
        this.GetComponent<Constraints>().milieu = milieu;
        this.GetComponent<Constraints>().posePossible = new bool[5] {true, true, true, true, false};
        this.GetComponent<Constraints>().carrefour = true;
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
