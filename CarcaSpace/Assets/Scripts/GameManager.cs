using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [Header("Turn Management")]
    public GameObject endTurnButton;
    public enum GameState {
        PlayerTurn,
        OthersTurn,
        GameEnd,
    }


    private bool inAction = false;
    public static GameManager Instance;
    public int Current_player = 0;
    private bool gameEnded = false;
    public GameState state;

    private void Awake(){
        Instance = this;
    }
    private List<PlayerManager> _players = new List<PlayerManager>();
    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (!NetworkServer.active){
            yield return null;
        }

        while(_players.Count != NetworkServer.connections.Count || !_players.TrueForAll(x => x.connectionToClient.isReady))
        {
            yield return null;
        }

        endTurnButton.SetActive(false); 
        _players[Current_player].isOurTurn = true;

    }
    public void AddPlayer(PlayerManager player){
        _players.Add(player);
        Debug.Log("nombre de joueur: " +_players.Count);

    }

    public void Update(){   
        if (_players[Current_player].isOurTurn){
           endTurnButton.SetActive(true);
        }
    }

    public void finir_tour(){
    }


    [Command]
    public void CmdFinirTour()
    {
        RpcModifTour();
    }

    [ClientRpc]
    public void RpcModifTour()
    {
        _players[Current_player].isOurTurn= !_players[Current_player].isOurTurn;

        // Si c'est le tour du joueur local
        if (_players[Current_player].isOurTurn)
        {
            // TODO
        }
    }

}
