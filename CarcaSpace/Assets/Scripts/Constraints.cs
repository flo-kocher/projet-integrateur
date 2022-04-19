using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Type_land
{
  Rien, Ville, Plaine, Chemin, Abbaye, Continue
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
    public bool blason;
    public int coordX;
    public int coordY;
    public Meeple meeple;
    public bool visite;
    public bool carrefour;
    public bool[] posePossible = {false, false, false, false, false};
    // Start is called before the first frame update
    void Start()
    {
        visite = false;

    }
    void Update()
    {
       
    }

    public string debug;


// recuper les valeurs des quatre voisins direct en partant de celui du dessus puis dans le sens trigo
    public void Voisin(GameObject[] voisins) 
    {
        voisins[0] = GameObject.Find(coordX + "/" + (coordY+1));
        voisins[1] = GameObject.Find((coordX-1) + "/" + coordY);
        voisins[2] = GameObject.Find(coordX + "/" + (coordY-1));
        voisins[3] = GameObject.Find((coordX+1) + "/" + coordY);
    }

    public void Voisin_tuiles(GameObject[] voisins) 
    {

        /*
        voisins[0] = GameObject.Find(coordX + "/" + (coordY+1));
        voisins[1] = GameObject.Find((coordX-1) + "/" + coordY);
        voisins[2] = GameObject.Find(coordX + "/" + (coordY-1));
        voisins[3] = GameObject.Find((coordX+1) + "/" + coordY);
        */
    }

    public bool verif(Type_land h, Type_land b, Type_land d, Type_land g)
    {
        bool vide = true;
        this.coordX = (int) transform.position.x;
        this.coordY = (int) transform.position.y; 
        GameObject[] voisins = new GameObject[4];
        GameObject poser;
        poser = GameObject.Find((this.coordX) + "/" + (this.coordY));
        if(poser.GetComponent<Constraints>().bas != Type_land.Rien && poser.GetComponent<Constraints>().haut != Type_land.Rien && poser.GetComponent<Constraints>().droite != Type_land.Rien && poser.GetComponent<Constraints>().gauche != Type_land.Rien){
            return false;
        }
        Voisin(voisins);

        // Bas
        if(voisins[0] != null && voisins[0].GetComponent<Constraints>().bas != Type_land.Rien && voisins[0].GetComponent<Constraints>().bas != h){
            debug = "0";
            return false;
        }

        // gauche
        if(voisins[1] != null && voisins[1].GetComponent<Constraints>().droite != Type_land.Rien && voisins[1].GetComponent<Constraints>().droite != g){
            debug = "1";
            return false;
        }
        
        // haut
        if(voisins[2] != null && voisins[2].GetComponent<Constraints>().haut != Type_land.Rien && voisins[2].GetComponent<Constraints>().haut != b){
            debug = "2";
            return false;
        }
        
        // droite
        if(voisins[3] != null && voisins[3].GetComponent<Constraints>().gauche != Type_land.Rien && voisins[3].GetComponent<Constraints>().gauche != d){
            debug = "3";
            return false;
        }        
        if((voisins[0] != null && voisins[0].GetComponent<Constraints>().bas != Type_land.Rien) || (voisins[1] != null && voisins[1].GetComponent<Constraints>().gauche != Type_land.Rien) ||
        (voisins[2] != null && voisins[2].GetComponent<Constraints>().haut != Type_land.Rien) || (voisins[3] != null && voisins[3].GetComponent<Constraints>().droite != Type_land.Rien) ||
        this.GetComponent<tile_type_0>() != null)
            return true;
            debug = "5";
        return false;
    }

}

