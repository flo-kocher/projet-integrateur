using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type_land
{
  Rien, Ville, Plaine, Chemin, Ville_blason, Arret_chemin, Abbaye
}
public class Constraints : MonoBehaviour
{
    public Type_land haut;
    public Type_land gauche;
    public Type_land bas;
    public Type_land droite;
    public Type_land milieu;
    public Type_land rien;
    public int coordX;
    public int coordY;
   
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.eulerAngles.z);
        haut = Type_land.Rien;
        gauche = Type_land.Rien;
        bas = Type_land.Rien;
        droite = Type_land.Rien;
        milieu = Type_land.Rien;
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

    /*
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
    */

// Beaucoup a faire, il faut trouver un moyen de recuperer les pieces par rapport a leurs coordonnées
// créer un script pour retenir les infos des pieces dans grid.
    void Voisin(int x, int y, GameObject[] voisins)
    {
        voisins[0] = GameObject.Find(coordX + "/" + (coordY+1));
        voisins[1] = GameObject.Find((coordX+1) + "/" + coordY);
        voisins[2] = GameObject.Find(coordX + "/" + (coordY-1));
        voisins[3] = GameObject.Find((coordX-1) + "/" + coordY);
    }

    public bool verif(Type_land h, Type_land b, Type_land d, Type_land g)
    {
        bool vide = true;
        this.coordX = (int) transform.position.x;
        this.coordY = (int) transform.position.y; 
        GameObject[] voisins = new GameObject[4];
        GameObject actuel = GameObject.Find(coordX + "/" + coordY);

        if(actuel.GetComponent<Constraints>().bas != Type_land.Rien && actuel.GetComponent<Constraints>().haut != Type_land.Rien && actuel.GetComponent<Constraints>().droite != Type_land.Rien && actuel.GetComponent<Constraints>().gauche != Type_land.Rien){
            return false;
        }
        Voisin(coordY, coordY, voisins);

        // Bas
        if(voisins[0] != null && voisins[0].GetComponent<Constraints>().bas != Type_land.Rien && voisins[0].GetComponent<Constraints>().bas != h){
            return false;
        }

        // gauche
        if(voisins[1] != null && voisins[1].GetComponent<Constraints>().gauche != Type_land.Rien && voisins[1].GetComponent<Constraints>().gauche != d){
            return false;
        }
        
        // haut
        if(voisins[2] != null && voisins[2].GetComponent<Constraints>().haut != Type_land.Rien && voisins[2].GetComponent<Constraints>().haut != b){
            return false;
        }
        
        // droite
        if(voisins[3] != null && voisins[3].GetComponent<Constraints>().droite != Type_land.Rien && voisins[3].GetComponent<Constraints>().droite != g){
            return false;
        }        
        if((voisins[0] != null && voisins[0].GetComponent<Constraints>().bas != Type_land.Rien) || (voisins[1] != null && voisins[1].GetComponent<Constraints>().gauche != Type_land.Rien) ||
        (voisins[2] != null && voisins[2].GetComponent<Constraints>().haut != Type_land.Rien) || (voisins[3] != null && voisins[3].GetComponent<Constraints>().droite != Type_land.Rien) ||
        this.GetComponent<tile_type_0>() != null)
            return true;
        return false;
    }

}



















