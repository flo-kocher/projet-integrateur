using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script attaché au bouton pick a tile
public class PIck : MonoBehaviour {
  private tile_type[] tiles = {
    new tile_type_1(),  new tile_type_2(),  new tile_type_3(),
    new tile_type_4(),  new tile_type_5(),  new tile_type_6(),
    new tile_type_7(),  new tile_type_8(),  new tile_type_9(),
    new tile_type_10(), new tile_type_11(), new tile_type_12(),
    new tile_type_13(), new tile_type_14(), new tile_type_15(),
    new tile_type_16(), new tile_type_17(), new tile_type_18(),
    new tile_type_19(), new tile_type_20(), new tile_type_21(),
    new tile_type_22(), new tile_type_23(), new tile_type_24()
  };
  private static int compteur = 0;
  bool create = true;
  bool premier = true;

  // Start is called before the first frame update
  void Start() { gameObject.GetComponent<Button>().onClick.AddListener(func); }

  // Update is called once per frame
  void Update() {}

  // Fonction appelé lors du clic sur le bouton
  void func() {
    create = true;
    GameObject temp = null;
    var list = Resources.FindObjectsOfTypeAll<GameObject>();
    foreach (GameObject i in list) {
      if (i.name == "Temp")
        temp = i;
      if (i.name.Contains("Pioche")) {
        i.GetComponent<Move>().enabled = false;
        i.GetComponent<AccessDenied>().enabled = false;
        // Si on est entrain de rotate, alors on ne crée pas de nouvelle tuile
        if (i.GetComponent<rotateZ>().enabled == true)
          create = false;
      }
    }
    if (create) {
      GameObject clone = GameObject.Instantiate(temp);
      clone.SetActive(true);
      clone.transform.SetParent(GameObject.Find("Tiles").transform);
      compteur++;
      clone.name = "Pioche" + compteur;

      int loop = 1;
      int num = 0, comptTuile = 0;
      while (loop == 1) {
        if (premier) {
          premier = false;
          tile_type_0 tuile = new tile_type_0();
          clone.AddComponent(tuile.GetType());
          return;
        }

        System.Random rnd = new System.Random();
        // Create number between 1 and 24
        num = rnd.Next(1, 25);
        // Debug.Log("num random => "+num);
        int nbr = 0;

        nbr = tiles[num - 1].getNbrTuile();
        if (nbr > 0) {
          // Debug.Log("nombre => " + nbr);
          tiles[num - 1].decrementNbrTuile();
          // Debug.Log("nombre decrem => " + tiles[num-1].getNbrTuile());
          clone.AddComponent(tiles[num - 1].GetType());
          loop = 0;
        } else {
          if (tiles[num - 1].getFinish() == false) {
            tiles[num - 1].changeFinish();
          } else {
            comptTuile++;
          }
        }

        if (comptTuile > 24) {
          loop = 0;
        }
      }
    }
  }
}
