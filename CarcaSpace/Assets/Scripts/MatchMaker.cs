using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror ; 


[System.Serializable]
public class Match{
    public string MatchId ;
    public SyncListGameObject players = new SyncListGameObject();
    //constructeur
    public Match(string MatchId , GameObject  player){
        this.MatchId = MatchId ;
        players.Add(player);
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

    void Start(){

    }
    // Retourne un code pour la partie 
    public static string GetRandomMatchId(){
        string _id = string.Empty ; 
        for(int i = 0 ; i<5 ; i++){
            int random  = Random.Range(0,36);
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

    public bool HostGame(){
        
    }

}
