using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

// Script qui clone un meeple lorsqu'on appuie sur une étoile
public class CreateMeeple : NetworkBehaviour
{

    // on récupère le PlayerManager
    public PlayerManager PlayerManager;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

	// méthode permettant de demander à faire spawn les Meeples
    public void function(GameObject go)
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
		// les paramètres sont les emplacements de l'étoile sur laquelle on clique
        PlayerManager.CmdSpawnMeeple(transform.position.x, transform.position.y,go); // liste de meeple pour savoir lequel supprimer
        //MoveMeeple m;
        //m.rmStars();
        //Debug.Log(transform.position.x);
    }

    public void meepleBack()
    {
        // if (PlayerManager.townIsClosed() == true || PlayerManager.roadIsClosed_Struct() == true)
        // {
        // 
        // }
    }
}
