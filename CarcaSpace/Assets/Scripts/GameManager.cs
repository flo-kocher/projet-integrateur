using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    [Header("Turn Management")]
   
    public static GameManager Instance;
    public GameObject Apply;

    // Variables de la partie 
    public int nb_joueur; 
    private bool inAction = false;
    public int Current_player = 0;
    public bool gameEnded = false;

    private void Awake(){
        Instance = this;
    }
    public List<PlayerManager> _players = new List<PlayerManager>();
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

        _players[Current_player].isOurTurn = true;
        

    }
    
    public void AddPlayer(PlayerManager player){
        player.id = _players.Count;
        _players.Add(player);

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
