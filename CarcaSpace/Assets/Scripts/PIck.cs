using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
public class PIck : NetworkBehaviour
{
    // client 
    public PlayerManager PlayerManager;
  

    // Start is called before the first frame update
    void Start()
    {
        // évènement qui se lance lorsqu'on clique sur le bouton de l'UI
        gameObject.GetComponent<Button>().onClick.AddListener(getIdAndCards);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void getIdAndCards()
    {
        // cherche l'identifiant du network
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        // client envoie une requête au serveur pour générer une tuile
        PlayerManager.CmdDealTiles();
    }
}
