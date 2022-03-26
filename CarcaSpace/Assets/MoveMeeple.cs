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
  
  public static void makeStars(bool[] tab, float x, float y) {
    GameObject temp = null;
    var list = Resources.FindObjectsOfTypeAll<GameObject>();
    foreach (GameObject i in list) {
      if (i.name == "tempStar")
        temp = i;
    }
    for(int i = 0; i<5; i++) {
      if (tab[i]) { 
        GameObject clone = GameObject.Instantiate(temp);
        clone.SetActive(true);
        clone.name = "Star" + i;
        clone.transform.position = new Vector3(x+tabPos[i].x,y+tabPos[i].y,-0.1f);
        clone.transform.SetParent(GameObject.Find("Stars").transform);
      }
    }
  }
  public static void rmStars() {
    GameObject parent = GameObject.Find("Stars");
    for (int i = 1; i<parent.transform.childCount; i++)
      MonoBehaviour.Destroy(parent.transform.GetChild(i).gameObject);
  }
  public static void rmMeeple() {
    GameObject parent = GameObject.Find("Meeples");
    if (parent.transform.childCount > 1)
      MonoBehaviour.Destroy(parent.transform.GetChild(parent.transform.childCount-1).gameObject);
  }
}
