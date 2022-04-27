using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class NetworkRoomManagerExt : NetworkRoomManager
{
    bool showStartButton;

    public List<NetworkRoomManagerExt> RoomPlayers { get; } = new List<NetworkRoomManagerExt>();

    public List<NetworkRoomManagerExt> GamePlayers { get; } = new List<NetworkRoomManagerExt>();

    [SerializeField] Scene menuScene ; 



    public string playerName;

    public override void OnStartServer () {
        base.OnStartServer();
    }

    public override void OnStartClient () {
        base.OnStartClient();
    }

    public override void OnRoomStopClient()
    {
        base.OnRoomStopClient();
    }

    public override void OnRoomStopServer()
    {
        base.OnRoomStopServer();
    }

    public override void OnStopClient () {
        base.OnStopClient();
        Debug.Log ($"Client Stopped");
    }
    public override void OnStopServer () {
        base.OnStopServer();
        Debug.Log ($"Client Stopped on Server");
    }


    public override void OnRoomServerConnect(NetworkConnectionToClient conn) {
        base.OnRoomServerConnect(conn);
    }
    // public override void OnRoomServerConnect(NetworkConnection conn)
    // {
    //     base.OnRoomServerConnect();
    //     if (numPlayers >= maxConnections)
    //     {
    //         conn.Disconnect();
    //         return;
    //     }

    //     if (SceneManager.GetActiveScene().name != menuScene)
    //     {
    //         conn.Disconnect();
    //         return;
    //     }
    // }


    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.
    #if UNITY_SERVER
        base.OnRoomServerPlayersReady();
    #else
        showStartButton = true;
    #endif
    }

    
    public override void OnGUI()
    {
        base.OnGUI();

        if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        {
            // set to false to hide it in the game scene
            showStartButton = false;

            ServerChangeScene(GameplayScene);
        }
    }
}

   
    