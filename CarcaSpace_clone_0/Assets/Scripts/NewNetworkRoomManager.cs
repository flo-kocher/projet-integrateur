using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;


/*
	Documentation: https://mirror-networking.com/docs/Components/NetworkRoomManager.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomManager.html

	See Also: NetworkManager
	Documentation: https://mirror-networking.com/docs/Components/NetworkManager.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

/// <summary>
/// This is a specialized NetworkManager that includes a networked room.
/// The room has slots that track the joined players, and a maximum player count that is enforced.
/// It requires that the NetworkRoomPlayer component be on the room player objects.
/// NetworkRoomManager is derived from NetworkManager, and so it implements many of the virtual functions provided by the NetworkManager class.
/// </summary>
public class NewNetworkRoomManager : NetworkRoomManager
{

    bool showStartButton;

    public List<NewNetworkRoomManager> RoomPlayers { get; } = new List<NewNetworkRoomManager>();

    public List<NewNetworkRoomManager> GamePlayers { get; } = new List<NewNetworkRoomManager>();

    [SerializeField] Scene menuScene ; 



    public string playerName;
  



    public override void OnStopClient () {
        base.OnStopClient();
        Debug.Log ($"Client Stopped");
    }
    public override void OnStopServer () {
        base.OnStopServer();
        Debug.Log ($"Client Stopped on Server");
    }

    public virtual void OnRoomClientEnter() {
        base.OnRoomClientEnter();
        
    }
    

    // / <summary>
    // / This is called on the server when all the players in the room are ready.
    // / <para>The default implementation of this function uses ServerChangeScene() to switch to the game player scene. By implementing this callback you can customize what happens when all the players in the room are ready, such as adding a countdown or a confirmation for a group leader.</para>
    // / </summary>
    public override void OnRoomServerPlayersReady()
    {
        // calling the base method calls ServerChangeScene as soon as all players are in Ready state.

        //base.OnRoomServerPlayersReady();

        #if UNITY_SERVER
            base.OnRoomServerPlayersReady();
#else
            showStartButton = true;
#endif

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
