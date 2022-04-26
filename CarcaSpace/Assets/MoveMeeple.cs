using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe statique (qu'on peut appeler partout) pour créer les étoiles puis les
// meeples
public static class MoveMeeple : object
{
    // Coorodnnées à transform selon ou l'on veut
    static Vector2 haut = new Vector2(0.5f, 0.83f);
    static Vector2 bas = new Vector2(0.5f, 0.17f);
    static Vector2 droite = new Vector2(0.83f, 0.5f);
    static Vector2 gauche = new Vector2(0.17f, 0.5f);
    static Vector2 milieu = new Vector2(0.5f, 0.5f);
    // Tableau défini pour les booleens et les Vector2
    static Vector2[] tabPos = { haut, gauche, bas, droite, milieu };



    // Méthode qui créer les étoiles avec le tableau de booleens et la postion X/Y
    // de la tuile
/*
    public static void makeStars(bool[] tab, float x, float y)
    {
        GameObject temp = null;
        var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject i in list)
        {
            if (i.name == "tempStar")
                temp = i;
        }
        for (int i = 0; i < 5; i++)
        {
            if (tab[i])
            {
                GameObject clone = GameObject.Instantiate(temp);
                clone.SetActive(true);
                clone.name = "Star" + i;
                clone.transform.position =
                    new Vector3(x + tabPos[i].x, y + tabPos[i].y, -0.1f);
                clone.transform.SetParent(GameObject.Find("Stars").transform);
            }
        }
    }
*/



    // Suppresion des étoiles
    public static void rmStars()
    {
        GameObject parent = GameObject.Find("Stars");
        for (int i = 1; i < parent.transform.childCount; i++)
            MonoBehaviour.Destroy(parent.transform.GetChild(i).gameObject);
    }
    // Suppression du dernier meeple créer
    public static void rmMeeple(GameObject meeple)
    {
        MonoBehaviour.Destroy(meeple);
    }

}
