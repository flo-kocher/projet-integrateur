using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;

public class RoomPlayerManager : NetworkBehaviour
{

    public static RoomPlayerManager localPlayer;


    // NetworkIdentity networkIdentity = NetworkClient.connection.identity;
    // localPlayer = networkIdentity.GetComponent<RoomPlayerManager>();
    [SyncVar] public string matchID;
    [SyncVar] public int playerIndex;

    NetworkMatch networkMatch;

    [SyncVar] public Match currentMatch;

    Guid netIDGuid;

    void Awake () {
        networkMatch = GetComponent<NetworkMatch> ();
    }
    public override void OnStartServer () {
        netIDGuid = netId.ToString ().ToGuid ();
        networkMatch.matchId = netIDGuid;
    }

    public override void OnStartClient () {
        base.OnStartClient();
        if (isLocalPlayer) {
            localPlayer = this;
        }
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        
    }
    public override void OnStopClient () {
        Debug.Log ($"Client Stopped");
        ClientDisconnect ();
    }
    public override void OnStopServer () {
        Debug.Log ($"Client Stopped on Server");
        ServerDisconnect ();
    }

    /* 
            HOST MATCH
        */

    public void HostGame () {
        string matchId = MatchMaker.GetRandomMatchId();
        Debug.Log($"Created Match With ID {matchId}");
        //il faudra get le nb de jouer set par le host et le mettre en arg a la place du 0 
        CmdHostGame(matchId,2);
    }

    [Command]
    void CmdHostGame( string _matchId , int playerNumber ){
        matchID = _matchId ;

        Debug.Log($"Match id est {matchID} et nb joueur {playerNumber}\n");
        Debug.Log($"Player is  {this}");
        Debug.Log($"Player index is  {playerIndex}");
        if(MatchMaker.instance.HostGame(playerNumber , matchID , this,out playerIndex)){
            Debug.Log("Game hosted successfully\n");
            networkMatch.matchId = _matchId.ToGuid();
            TargetHostGame(true, matchID, playerIndex);

        }else{
            Debug.Log("Host failed \n");
            TargetHostGame(false, matchID, playerIndex);
        }
    }


    [TargetRpc]
    void TargetHostGame(bool sucess,string _matchId,int _playerIndex){
        playerIndex = _playerIndex;
        matchID = _matchId;
        Debug.Log($"Match ID is {matchID}");
        MultiplayerMenu.instance.hostSucc(sucess);
    }

    public void JoinGame (string _inputID) {
        CmdJoinGame (_inputID);
    }

    [Command]
    void CmdJoinGame (string _matchID) {
        matchID = _matchID;
        if (MatchMaker.instance.JoinGame (_matchID,localPlayer,out playerIndex)) {
            Debug.Log ($"<color=green>Game Joined successfully</color>");
            networkMatch.matchId = _matchID.ToGuid ();
            TargetJoinGame (true, _matchID, playerIndex);
        }else{
            Debug.Log ($"<color=red>Game Joined failed</color>");
            TargetJoinGame(false, _matchID, playerIndex)  ;
        }
        
    }

    [TargetRpc]
    void TargetJoinGame (bool success, string _matchID, int _playerIndex) {
        playerIndex = _playerIndex;
        matchID = _matchID;
        Debug.Log ($"MatchID: {matchID} == {_matchID}");
        //UILobby.instance.JoinSuccess (success, _matchID);
    }

    /* 
            DISCONNECT
        */

    public void DisconnectGame () {
        CmdDisconnectGame ();
    }

    [Command]
    void CmdDisconnectGame () {
        ServerDisconnect ();
    }

    void ServerDisconnect () {
        MatchMaker.instance.PlayerDisconnected (this, matchID);
        RpcDisconnectGame ();
        networkMatch.matchId = netIDGuid;
    }

    [ClientRpc]
    void RpcDisconnectGame () {
        ClientDisconnect ();
    }

    void ClientDisconnect () {
        // if (playerLobbyUI != null) {
        //     if (!isServer) {
        //         Destroy (playerLobbyUI);
        //     } else {
        //         playerLobbyUI.SetActive (false);
        //     }
        // }
    }
    /* 
            BEGIN MATCH
        */
    //La partie se lancera quand tous les joueurs sont ready 
    public void BeginGame () {
        CmdBeginGame ();
    }

    [Command]
    void CmdBeginGame () {
        MatchMaker.instance.BeginGame (matchID);
        Debug.Log ($"<color=red>Game Beginning</color>");
    }

    // je me demande si on a besoi nde ca vu qu'on lance quand les joueurs sont ready 
    // public void StartGame () { //Server
    //     TargetBeginGame ();
    // }
    // [TargetRpc]
    // void TargetBeginGame () {
    //     Debug.Log ($"MatchID: {matchID} | Beginning");
    //     //Additively load game scene
    //     SceneManager.LoadScene (2, LoadSceneMode.Additive);
    // }

    //ces deux fonctions servent a savoir si on peut lancer ou pas 

    // [Server]
    // public void PlayerCountUpdated (int playerCount) {
    //     TargetPlayerCountUpdated (playerCount);
    // }

    // [TargetRpc]
    // void TargetPlayerCountUpdated (int playerCount) {
    //     if (playerCount > 1) {
    //         UILobby.instance.SetStartButtonActive(true);
    //     } else {
    //         UILobby.instance.SetStartButtonActive(false);
    //     }
    // }

}