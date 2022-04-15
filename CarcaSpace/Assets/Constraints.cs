using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Type_land
{
  Rien, Ville, Plaine, Chemin, Ville_blason, Arret_chemin, Abbaye, Continue
}

public enum Meeple
{
    Rien, Haut, Bas, Gauche, Droite, Milieu
}

public class Constraints : MonoBehaviour
{
    public Type_land haut;
    public Type_land gauche;
    public Type_land bas;
    public Type_land droite;
    public Type_land milieu;
    public Type_land rien;
    public bool blason;
    public int coordX;
    public int coordY;
    public Meeple meeple;
   
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(transform.eulerAngles.z);
        haut = Type_land.Rien;
        gauche = Type_land.Rien;
        bas = Type_land.Rien;
        droite = Type_land.Rien;
        milieu = Type_land.Rien;
        blason = false;
        meeple = Meeple.Rien;
    }

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



















