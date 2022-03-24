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
        gameObject.GetComponent<Button>().onClick.AddListener(getIdAndCards);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void getIdAndCards()
    {
        // get network identity
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        // sent a request (which is a Command server side) to server to do something for us 
        PlayerManager.CmdDealTiles();
    }
}
