using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Mirror ; 

public class CreatingGame : MonoBehaviour
{

    public static CreatingGame instance ; 

    //Boutons pour selectionner nb de joueurs ;
    [SerializeField] Button addPlayers;
    [SerializeField] Button subPlayers;

    [SerializeField] List<Selectable> UISelectables = new List<Selectable> ();


    //ou l'on recup le nb de joueurs choisi
    [SerializeField] InputField playerNumberInput ; 

    

    GameObject localPlayerLobbyUI;

    // ajouter 1 au nb de joueur
    public void addPlayer()
    {
        int nbPlayers = int.Parse(playerNumberInput.text);
        if(nbPlayers < 6){
            nbPlayers++ ;
            if(nbPlayers == 2){
                // mask2.showMaskGraphic = true;
                addPlayers.interactable=false;
            }
        }
        Debug.Log(nbPlayers);
        playerNumberInput.text = nbPlayers.ToString();
    }

    // soustraire 1 au nb de joueur
    public void substractPlayer()
    {

        int nbPlayers = int.Parse(playerNumberInput.text);

        if(nbPlayers >= 0)
        {
            nbPlayers--;
            if(nbPlayers == 2)
            {
                // mask2.showMaskGraphic = false;
                subPlayers.interactable=false;
                
            }
        }
        playerNumberInput.text = nbPlayers.ToString();
    }


    void Start() {
        instance = this ; 
    }
    public void Host () {
        UISelectables.ForEach (x => x.interactable = false);
        
        // A faire dans la scene lobby 
        // Debug.Log("Hosting Game ! ");
        // Debug.Log($"Player is  {PlayerManager.localPlayer}");
        // PlayerManager.localPlayer.HostGame();
    }

   

    public void HostSuccess (bool success, string matchID) {
        if (success) {
            //switch scenes ici
            SceneManager.LoadScene("Lobby");

            if (localPlayerLobbyUI != null) Destroy (localPlayerLobbyUI);
            //localPlayerLobbyUI = SpawnPlayerUIPrefab (Player.localPlayer);
            //on spawn les joueurs ici dans le lobby 
            // il faudra afficher id de la partie dans le lobby 
        } else {
            UISelectables.ForEach (x => x.interactable = true);
        }
    }
    public void DisconnectGame () {
        if (localPlayerLobbyUI != null) Destroy (localPlayerLobbyUI);
        PlayerManager.localPlayer.DisconnectGame ();

        
        UISelectables.ForEach (x => x.interactable = true);
    }

    ////POur marc a faire
    //  public GameObject SpawnPlayerUIPrefab (PlayerManager player) {
    //     // GameObject newUIPlayer = Instantiate (UIPlayerPrefab, UIPlayerParent);
    //     // newUIPlayer.GetComponent<UIPlayer> ().SetPlayer (player);
    //     // newUIPlayer.transform.SetSiblingIndex (player.playerIndex - 1);

    //     // return newUIPlayer;
    // }

    public void BeginGame () {
        PlayerManager.localPlayer.BeginGame ();
    }
}
