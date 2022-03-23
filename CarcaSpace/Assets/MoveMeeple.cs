using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveMeeple : object {
  // Coorodnnées à transform selon ou l'on veut
  static Vector2 nord = new Vector2(0.5f,0.83f);
  static Vector2 sud = new Vector2(0.5f,0.17f);
  static Vector2 est = new Vector2(0.83f,0.5f);
  static Vector2 ouest = new Vector2(0.17f,0.5f);
  static Vector2 milieu = new Vector2(0.5f,0.5f);
  // Tableau défini pour les booleens et les Vector2
  static Vector2[] tabPos = { nord, sud, est, ouest, milieu };
  
  public static void makeStars(bool[] tab) {
    GameObject temp = null;
    var list = Resources.FindObjectsOfTypeAll<GameObject>();
    foreach (GameObject i in list) {
      if (i.name == "tempStar")
        temp = i;
    }
    for(int i = 0; i<5; i++) {
      if (tab[i]) { 
        GameObject clone = GameObject.Instantiate(temp);
        clone.name = "Star" + i;
        clone.transform.position = tabPos[i];
        clone.transform.SetParent(GameObject.Find("Stars").transform);
        clone.SetActive(true);
      }
    }
  }
}
