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
  
    public override GameObject OnRoomServerCreateRoomPlayer(NetworkConnectionToClient conn)
    {
        return base.OnRoomServerCreateRoomPlayer(conn);
    }

        public override GameObject OnRoomServerCreateGamePlayer(NetworkConnectionToClient conn, GameObject roomPlayer)
    {
        return base.OnRoomServerCreateGamePlayer(conn, roomPlayer);
    }


    // / <summary>
    // / This is called on the server when all the players in the room are ready.
    // / <para>The default implementation of this function uses ServerChangeScene() to switch to the game player scene. By implementing this callback you can customize what happens when all the players in the room are ready, such as adding a countdown or a confirmation for a group leader.</para>
    // / </summary>
    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.

        base.OnRoomServerPlayersReady();

        showStartButton = true;

    }

    public override void OnRoomClientConnect(NetworkConnection conn) {
        base.OnRoomClientConnect(conn) ; 
        Debug.Log($"player number is {RoomPlayers.Count} ");

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

   
    