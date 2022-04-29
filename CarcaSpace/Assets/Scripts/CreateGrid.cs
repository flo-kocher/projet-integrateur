using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror; 

// Script qui crée la grille
public class CreateGrid : NetworkBehaviour {
  // Nombre de tuiles à créer (nbTuiles*nbTuiles)
  public int nbTuiles;
  private GameObject go;
  
  public PlayerManager PlayerManager;
  // Start is called before the first frame update
  void Start() {
    // var list = Resources.FindObjectsOfTypeAll<GameObject>();
    // foreach (GameObject i in list) {
    //   if (i.name == "tempGrid")
    //     go = i;
    // }
    // for (int x = 0; x < nbTuiles; x++) {
    //   for (int y = 0; y < nbTuiles; y++) {
    //     GameObject clone = GameObject.Instantiate(go);
    //     clone.transform.SetParent(GameObject.Find("Grid").transform);
    //     clone.name = x + "/" + y;
    //     Vector2 v = new Vector2(x + 0.5f, y + 0.5f);
    //     clone.transform.position = v;
    //     clone.SetActive(true);
    //   }
    // }
    // // Alignement de la caméra pour se trouver au milieu de la grille
    // Vector3 vec = new Vector3((float)Decimal.Divide(nbTuiles, 2),
    //                           (float)Decimal.Divide(nbTuiles, -4), -5);
    // Camera.main.transform.position = vec;
    // NetworkIdentity networkIdentity = NetworkClient.connection.identity;
    // PlayerManager = networkIdentity.GetComponent<PlayerManager>();
    // PlayerManager.CmdSpawnGrid(nbTuiles);

    
    // NetworkIdentity networkIdentity = NetworkClient.connection.identity;
    // Debug.Log(networkIdentity);
    // PlayerManager = networkIdentity.GetComponent<PlayerManager>();
    // PlayerManager.CmdSpawnGrid(10);
  }

  // Update is called once per frame
  void Update() {}
}
