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
    PlayerManager.CmdSpawnMeeple(transform.position.x,transform.position.y);
    
    Debug.Log(transform.position.x);
  }
}
