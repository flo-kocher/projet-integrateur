using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type_land
{
  Ville, Plaine, Chemin, Ville_blason, Arret_chemin, Abbaye
}
public class Constraints : MonoBehaviour
{
    public Type_land haut;
    public Type_land gauche;
    public Type_land bas;
    public Type_land droite;
    public Type_land milieu;
    private rotateZ r;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.eulerAngles.z);
        r = this.GetComponent<rotateZ>();
        haut = Type_land.Plaine;
        gauche = Type_land.Chemin;
        bas = Type_land.Plaine;
        droite = Type_land.Chemin;
        milieu = Type_land.Chemin;
    }
/*
rotation a gauche : 90
rotation a droite : -90
rotation deux fois : 180 ou -180


*/
    // Update is called once per frame
    void Update()
    {
        //call a chaque instant de rotation side switch
        // ou call des que pression sur bouton gauche ou droite

        //attention si on fait trop vite, l'animation a pas le temps de se faire
        //donc ca decale tout
        if(Input.GetKeyDown("left") && !(r.leve || r.couche || r.tourne))
        {
            rotate_left();
        }
        if(Input.GetKeyDown("right") && !(r.leve || r.couche || r.tourne))
        {
            rotate_right();
        }
    }
/*
    // pas encore terminer/pas encore tester
    void rotation_side_switch()
    {
        //rotation Z = 0 90 (-180 ou 180) -90
        //private Vector3 r_values = tranform.Rotate.forward;
        float rot_value_z = transform.eulerAngles.z;
        if(83 < rot_value_z && rot_value_z < 97)
        {
            Debug.Log("tourner a 90");
            string cp = haut;
            haut = droite;
            droite = bas;
            bas = gauche;
            gauche = cp;
        }
        else if(-97 < rot_value_z && rot_value_z < -83)
        {
            Debug.Log("tourner a -90");
            string cp = haut;
            haut = gauche;
            gauche = bas;
            bas = droite;
            droite = cp;
        }
        else if( (178 < rot_value_z && rot_value_z < 182) || (-182 < rot_value_z && rot_value_z < -178))
        {
            Debug.Log("tourner a 180 -180");
            string cp1 = haut;
            haut = bas;
            bas = cp1;
            string cp2 = gauche;
            gauche = droite;
            droite = cp2;
        }
            
    }
*/

    // des pressions sur touche pour tourner faire les changements de valeurs
    void rotate_left()
    {
        Type_land cp = haut;
        haut = droite;
        droite = bas;
        bas = gauche;
        gauche = cp;
    }

    void rotate_right()
    {
        Type_land cp = haut;
        haut = gauche;
        gauche = bas;
        bas = droite;
        droite = cp;
    }

    
}
