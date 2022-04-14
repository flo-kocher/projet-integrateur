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
        players.Add(player);
        this.nbPlayers = nbPlayers ;
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
    public SyncListMatch matches = new SyncListMatch();

    //Liste match ids  
    public SyncList<string> matchIDS = new SyncList<string>();

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

    public bool HostGame(int _playerNumber, string _matchId,PlayerManager player){     
        //one verifie si le meme id n'esxiste pas deja dans la liste
        if(!matchIDS.Contains(_matchId)){
            matchIDS.Add(matchId);
            Match match = new Match(playerNumber,_matchId,player) ;
            matches.Add(match);
            playerIndex = 1;
            Debug.Log("Match Generated \n");
            return true ;
        }else{
            Debug.Log("Match Id already exists \n");
            return false ; 
        }

    }

    public bool JoinGame(string _matchId,PlayerManager _player){     

        
        //one verifie si le meme id n'esxiste pas deja dans la liste
       if (matchIDs.Contains (_matchID)) {

                for (int i = 0; i < matches.Count; i++) {
                    if (matches[i].matchID == _matchID) {
                        if (!matches[i].inMatch && !matches[i].matchFull) {
                            matches[i].players.Add (_player);
                            _player.currentMatch = matches[i];
                            playerIndex = matches[i].players.Count;

                            matches[i].players[0].PlayerCountUpdated (matches[i].players.Count);

                            if (matches[i].players.Count == maxMatchPlayers) {
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
            if (matches[i].matchID == _matchID) {
                matches[i].inMatch = true;
                foreach (var player in matches[i].players) {
                    player.StartGame ();
                }
                break;
            }
        }
    }

}

//Fonction qui sert a creer un gamecode
public static class MatchExtensions {
        public static Guid ToGuid (this string id) {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider ();
            byte[] inputBytes = Encoding.Default.GetBytes (id);
            byte[] hashBytes = provider.ComputeHash (inputBytes);

            return new Guid (hashBytes);
        }
    }
