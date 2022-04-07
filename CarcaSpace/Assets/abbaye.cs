using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Ce script doit etre attaché à une tuile lorsqu'un joueur pose un meeple dessus 
// Points est incrementé quand une tuile est posé a coté. Durant la pose de chaque tuile il faudra verifier si une abbaye est presente autour
public class Abbaye : MonoBehaviour
{
    private int points;
    private int meeple; // joueur à qui appartient le meeple sur la case.
   
    // Start is called before the first frame update
    void Start()
    {
        int coordX = this.GetComponent<Constraints>().coordX;
        int coordY = this.GetComponent<Constraints>().coordY;
        GameObject[] voisins = new GameObject[8];
        voisins[0] = GameObject.Find((coordX) + "/" + (coordY+1));
        voisins[1] = GameObject.Find((coordX+1) + "/" + (coordY+1));
        voisins[2] = GameObject.Find((coordX+1) + "/" + (coordY));
        voisins[3] = GameObject.Find((coordX+1) + "/" + (coordY-1));
        voisins[4] = GameObject.Find((coordX) + "/" + (coordY-1));
        voisins[5] = GameObject.Find((coordX-1) + "/" + (coordY-1));
        voisins[6] = GameObject.Find((coordX-1) + "/" + (coordY));
        voisins[7] = GameObject.Find((coordX-1) + "/" + (coordY+1));

        points = 1;
        for(int i=0; i<=7; i++)
            if (voisins[i] != null && voisins[i].GetComponent<Constraints>().haut != Type_land.Rien)
                points++;                    
    }
    void Update()
    {
    
    }
}




