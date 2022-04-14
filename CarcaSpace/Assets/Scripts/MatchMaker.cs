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
    public string MatchId ;
    public bool inMatch;
    int nbPlayers ; 
    public List<PlayerManager> players = new List<PlayerManager>();
    
    //constructeur
    public Match(int nbPlayers,string MatchId , PlayerManager  player){
        this.MatchId = MatchId ;
        players.Add(player);
        this.nbPlayers = nbPlayers ;
    }

    public Match(){
        
    }
}



[System.Serializable]
public class SyncListMatch : SyncList<Match> {

}

[System.Serializable]
public class SyncListGameObject : SyncList<GameObject> {

}


public class MatchMaker : NetworkBehaviour  
{
    public static MatchMaker instance;
    //Liste de parties 
    public SyncListMatch matches = new SyncListMatch();

    public SyncList<string> matchIDS = new SyncList<string>();

    void Start(){

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

    public bool HostGame(int playerNumber, string matchId,PlayerManager player){     
        //one verifie si le meme id n'esxiste pas deja dans la liste
        if(!matchIDS.Contains(matchId)){
            matchIDS.Add(matchId);
            matches.Add(new Match(playerNumber,matchId,player));
            Debug.Log("Match Generated \n");
            return true ;
        }else{
            Debug.Log("Match Id already exists \n");
            return false ; 
        }

    }

    public bool JoinGame(string _matchId,PlayerManager _player){     
        //one verifie si le meme id n'esxiste pas deja dans la liste
        if(!matchIDS.Contains(_matchId)){
            for(int i = 0 ; i < matches.Count ; i ++ ){
                if(matches[i].MatchId == _matchId){
                    matches[i].players.Add(_player);
                    break ; 
                }
            }
            Debug.Log("Match Joined \n");
            return true ;
        }else{
            Debug.Log("Match Id does not  \n");
            return false ; 
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
