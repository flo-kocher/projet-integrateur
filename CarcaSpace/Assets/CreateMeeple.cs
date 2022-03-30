using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror ;

// Script qui clone un meeple lorsqu'on appuie sur une étoile
public class CreateMeeple : NetworkBehaviour {

  // client 
  public PlayerManager PlayerManager;
  
  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {}

  public void function() {

    NetworkIdentity networkIdentity = NetworkClient.connection.identity;
    PlayerManager = networkIdentity.GetComponent<PlayerManager>();
    PlayerManager.CmdSpawnMeeple();
    // compteur++;
    // GameObject temp = null;
    // var list = Resources.FindObjectsOfTypeAll<GameObject>();
    // foreach (GameObject i in list) {
    //   if (i.name == "tempMeeple")
    //     temp = i;
    // }
    // GameObject clone = GameObject.Instantiate(temp);
    // clone.SetActive(true);
    // NetworkServer.Spawn(clone, connectionToClient);
    // clone.name = "Meeple" + compteur;
    // clone.transform.position = new Vector3(transform.position.x + 0.6f,
    //                                        transform.position.y - 0.04f, 0.25f);
    // clone.transform.SetParent(GameObject.Find("Meeples").transform);
    // MoveMeeple.rmStars();
  }
}
