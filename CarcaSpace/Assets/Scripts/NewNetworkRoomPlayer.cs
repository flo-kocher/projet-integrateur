using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;
using Mirror;

/*
	Documentation: https://mirror-networking.com/docs/Components/NetworkRoomPlayer.html
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkRoomPlayer.html
*/

/// <summary>
/// This component works in conjunction with the NetworkRoomManager to make up the multiplayer room system.
/// The RoomPrefab object of the NetworkRoomManager must have this component on it.
/// This component holds basic room player data required for the room to function.
/// Game specific data for room players can be put in other components on the RoomPrefab or in scripts derived from NetworkRoomPlayer.
/// </summary>
public class NewNetworkRoomPlayer : NetworkRoomPlayer
{
    
    //NetworkLobbyPlayer N ;

    
    
    [SyncVar] 
    public int playerNumber = 0 ;

    public int playerIndex;
    public GameObject PlayerCard ; 

    //[SyncVar]
    public SyncList<GameObject> cardList = new SyncList<GameObject>();

    #region Start & Stop Callbacks

    /// <summary>
    /// This is invoked for NetworkBehaviour objects when they become active on the server.
    /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
    /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
    /// </summary>
    public override void OnStartServer() {
        base.OnStartServer();
    }

    /// <summary>
    /// Invoked on the server when the object is unspawned
    /// <para>Useful for saving object data in persistent storage</para>
    /// </summary>
    public override void OnStopServer() {
        base.OnStopServer();
    }

    /// <summary>
    /// Called on every NetworkBehaviour when it is activated on a client.
    /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
    /// </summary>
    public override void OnStartClient() {
        base.OnStartClient();
        Debug.Log("Start Client !!!!!!!!!!!!!!!!!!!!!!!!!!");
    }

    /// <summary>
    /// This is invoked on clients when the server has caused this object to be destroyed.
    /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
    /// </summary>
    public override void OnStopClient() { 
        base.OnStopClient() ;
        playerNumber -- ;
    }

    /// <summary>
    /// Called when the local player object has been set up.
    /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
    /// </summary>
    public override void OnStartLocalPlayer() {
        base.OnStartLocalPlayer();
    }

    /// <summary>
    /// This is invoked on behaviours that have authority, based on context and <see cref="NetworkIdentity.hasAuthority">NetworkIdentity.hasAuthority</see>.
    /// <para>This is called after <see cref="OnStartServer">OnStartServer</see> and before <see cref="OnStartClient">OnStartClient.</see></para>
    /// <para>When <see cref="NetworkIdentity.AssignClientAuthority"/> is called on the server, this will be called on the client that owns the object. When an object is spawned with <see cref="NetworkServer.Spawn">NetworkServer.Spawn</see> with a NetworkConnection parameter included, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStartAuthority() {
        base.OnStartAuthority();  
        CmdSpawnCard(index);
    }

    [Command]
    public void CmdSpawnCard(int _index){
        Debug.Log("spawning player card ");
        Debug.Log($"Index in command {_index}");
        GameObject newCard = Instantiate(PlayerCard);
        newCard.transform.SetParent(GameObject.Find("Players").transform);
        newCard.transform.GetChild(0).GetComponent<Text>().text = "Player" + (_index);
        
        newCard.transform.GetChild(1).GetComponent<Button>().onClick.AddListener( () => changeColor(newCard));
        NetworkServer.Spawn(newCard,connectionToClient);
        cardList.Add(newCard);
        RpcShowPreviousCard( );
    }


    [ClientRpc]
    public void RpcShowPreviousCard(){
        // int i = 0 ;
        // Debug.Log($"Card count is {cardList.Count}");
        // if(index != cardList.Count  ){
        //     i = cardList.Count - (index) ;
        //     Debug.Log($"I is  {index}");
        // }
           
        // for ( ; i<cardList.Count;i++){
        //     Debug.Log($"Card count is {cardList.Count}");
        //     cardList[i].SetActive(true);
        // }
        for (int i = 0 ; i< cardList.Count ; i++){
            cardList[i].SetActive(false) ; 
            cardList[i].SetActive(true) ; 
        }
        Debug.Log($"Card count is {cardList.Count}");  
    }

    public void changeColor(GameObject newCard){
        Debug.Log("On click ready button ");
        newCard.transform.GetChild(1).GetComponent<Image>().color =  new Color32(255,255,225,100); 
        readyToBegin = !readyToBegin;
        ReadyStateChanged(!readyToBegin,readyToBegin);
        Debug.Log($"index is {index}");
        //GetComponent<NetworkLobbyPlayer> ().SendReadyToBeginMessage ();
    }

    /// <summary>
    /// This is invoked on behaviours when authority is removed.
    /// <para>When NetworkIdentity.RemoveClientAuthority is called on the server, this will be called on the client that owns the object.</para>
    /// </summary>
    public override void OnStopAuthority() {base.OnStopAuthority(); }

    #endregion

    #region Room Client Callbacks

    /// <summary>
    /// This is a hook that is invoked on all player objects when entering the room.
    /// <para>Note: isLocalPlayer is not guaranteed to be set until OnStartLocalPlayer is called.</para>
    /// </summary>
    public override void OnClientEnterRoom() {
        base.OnClientEnterRoom();
        if(isLocalPlayer == true){
            
            playerNumber++;
            Debug.Log($"Nb of players{playerNumber}");
            Debug.Log($"My index is {index}");
        }
        
    }

    /// <summary>
    /// This is a hook that is invoked on all player objects when exiting the room.
    /// </summary>
    public override void OnClientExitRoom() { base.OnClientExitRoom();}

    #endregion

    #region SyncVar Hooks

    /// <summary>
    /// This is a hook that is invoked on clients when the index changes.
    /// </summary>
    /// <param name="oldIndex">The old index value</param>
    /// <param name="newIndex">The new index value</param>
    public override void IndexChanged(int oldIndex, int newIndex) { 
        base.IndexChanged( oldIndex,  newIndex) ;
    }

    /// <summary>
    /// This is a hook that is invoked on clients when a RoomPlayer switches between ready or not ready.
    /// <para>This function is called when the a client player calls SendReadyToBeginMessage() or SendNotReadyToBeginMessage().</para>
    /// </summary>
    /// <param name="oldReadyState">The old readyState value</param>
    /// <param name="newReadyState">The new readyState value</param>
    public override void ReadyStateChanged(bool oldReadyState, bool newReadyState) {
        base.ReadyStateChanged( oldReadyState,  newReadyState) ;

     }

    #endregion

    #region Optional UI

    public override void OnGUI()
    {
        base.OnGUI();
    }

    #endregion
}
