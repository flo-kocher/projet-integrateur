using System.Collections;
using System ;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking ;
using Mirror ; 
using System.Security.Cryptography;
using System.Text;




[System.Serializable]
public class Match{
    //Code de la partie 
    public string MatchId ;
    public bool inMatch;
    //nb Joueurs Max choisi par le host 
    int nbPlayers ; 
    //si le match est full ou pas 
    public bool matchFull;
    //Liste des joueurs dans la partie 
    public List<PlayerManager> players = new List<PlayerManager>();
    
    //constructeur
    public Match(int nbPlayers,string MatchId , PlayerManager  player){
        matchFull = false ; 
        inMatch = false ;
        this.MatchId = MatchId ;
        this.nbPlayers = nbPlayers ;
        players.Add(player);
        
    }

    public Match(){
        
    }
}



// [System.Serializable]
// public class SyncListMatch : SyncList<Match> {
// }

// [System.Serializable]
// public class SyncListGameObject : SyncList<GameObject> {
// }


public class MatchMaker : NetworkBehaviour  
{
    public static MatchMaker instance;


    //Liste de parties 
    public SyncList<Match> matches = new SyncList<Match> ();

    //Liste match ids  
    public SyncList<String> matchIDS = new SyncList<String>();

    [SerializeField] int maxPlayers = 5 ; 

    void Start(){
        instance = this ; 
    }
    // Retourne un code pour la partie 
    public static string GetRandomMatchId(){
        string _id = string.Empty ; 
        for(int i = 0 ; i<5 ; i++){
            int random  = UnityEngine.Random.Range(0,36);
            // ca veut dire que c'est une lettre
            if(random<26){
                //Pour avoir en Majuscules
                _id +=(char)(random + 65 );
            }else{
                _id += (random - 26).ToString();
            }
        }
        return _id ; 
    }

    public bool HostGame(int _playerNumber, string _matchId,PlayerManager player,out int playerIndex){     
        playerIndex = -1;
        //a changer
        Debug.Log("Je suis dans host matchmake \n");
        //one verifie si le meme id n'esxiste pas deja dans la liste
        if(!matchIDS.Contains(_matchId)){
            matchIDS.Add(_matchId);
            Match match = new Match(_playerNumber,_matchId,player) ;
            matches.Add(match);
            Debug.Log("Match Generated \n");
            player.currentMatch = match;
            playerIndex = 1;
            
            return true ;
        }else{
            Debug.Log("Match Id already exists \n");
            return false ; 
        }

    }

    public bool JoinGame(string _matchId,PlayerManager _player,out int playerIndex){     
        playerIndex = -1;
        //one verifie si le meme id n'esxiste pas deja dans la liste
       if (matchIDS.Contains (_matchId)) {
            for (int i = 0; i < matches.Count; i++) {
                if (matches[i].MatchId == _matchId) {
                    // on verifie s'il n'est pas deja dans la partie et si la partie est deja full 
                    if (!matches[i].inMatch && !matches[i].matchFull) {
                        // on l'ajoute a la liste des  joueurs
                        matches[i].players.Add (_player);
                        _player.currentMatch = matches[i];
                        playerIndex = matches[i].players.Count;

                        matches[i].players[0].PlayerCountUpdated (matches[i].players.Count);
                        // on met a jour matchfull 
                        if (matches[i].players.Count == maxPlayers) {
                            matches[i].matchFull = true;
                        }
                        break;
                    } else {
                        return false;
                    }
                }
            }

            Debug.Log ($"Match joined");
            return true;
        } else {
            Debug.Log ($"Match ID does not exist");
            return false;
        }
    }
    
    public void BeginGame (string _matchID) {
        for (int i = 0; i < matches.Count; i++) {
            if (matches[i].MatchId == _matchID) {
                matches[i].inMatch = true;
                foreach (var player in matches[i].players) {
                    player.StartGame ();
                }
                break;
            }
        }
    }

    public void PlayerDisconnected (PlayerManager player, string _matchID) {
        for (int i = 0; i < matches.Count; i++) {
            if (matches[i].MatchId == _matchID) {
                int playerIndex = matches[i].players.IndexOf (player);
                if (matches[i].players.Count > playerIndex) matches[i].players.RemoveAt (playerIndex);
                Debug.Log ($"Player disconnected from match {_matchID} | {matches[i].players.Count} players remaining");

                if (matches[i].players.Count == 0) {
                    Debug.Log ($"No more players in Match. Terminating {_matchID}");
                    matches.RemoveAt (i);
                    matchIDS.Remove (_matchID);
                } else {
                    matches[i].players[0].PlayerCountUpdated (matches[i].players.Count);
                }
                break;
            }
        }
    }

}

//Fonction qui sert a creer un gamecode
public static class MatchExtensions {

    // les elemets ayant le meme guid seront synchro ensemble 
        public static Guid ToGuid (this string id) {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider ();
            byte[] inputBytes = Encoding.Default.GetBytes (id);
            byte[] hashBytes = provider.ComputeHash (inputBytes);

            return new Guid (hashBytes);
        }
    }
