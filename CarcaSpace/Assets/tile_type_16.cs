using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile_type_16 : tile_type
{
    public static bool finish = false;

    // Start is called before the first frame update
    void Start()
    {
        r = this.GetComponent<rotateZ>();
        haut = Type_land.Ville;
        gauche = Type_land.Plaine;
        bas = Type_land.Plaine;
        droite = Type_land.Plaine;
        milieu = Type_land.Ville;
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
