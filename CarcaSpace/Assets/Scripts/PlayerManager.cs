using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameObject grid ; 
    public GameObject  tiles ;

    List<GameObject> tile = new List<GameObject>();

    public override void OnStartClient(){
        base.OnStartClient();

        grid = GameObject.Find("Grid");
        tiles = GameObject.Find("Tiles");
    }

    [Server]
    public override void OnStartServer(){
        base.OnStartServer();
        tile.Add(tiles);
        Debug.Log(tile);
    }
}
