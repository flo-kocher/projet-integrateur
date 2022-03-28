using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui clone un meeple lorsqu'on appuie sur une étoile
public class CreateMeeple : MonoBehaviour {
  static int compteur = 0;
  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {}

  public void function() {
    compteur++;
    GameObject temp = null;
    var list = Resources.FindObjectsOfTypeAll<GameObject>();
    foreach (GameObject i in list) {
      if (i.name == "tempMeeple")
        temp = i;
    }
    GameObject clone = GameObject.Instantiate(temp);
    clone.SetActive(true);
    clone.name = "Meeple" + compteur;
    clone.transform.position = new Vector3(transform.position.x + 0.6f,
                                           transform.position.y - 0.04f, 0.25f);
    clone.transform.SetParent(GameObject.Find("Meeples").transform);
    MoveMeeple.rmStars();
  }
}
