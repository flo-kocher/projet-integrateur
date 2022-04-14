using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CreatingGame : MonoBehaviour
{
    private Button addPlayers;
    private Button subPlayers;

    private Text text;
    private Mask mask;

    private Button Create;
    private Button JoinFriends;
    MultiplayerMenu MultiplayerMenu ;


    public void addPlayer()
    {
        text = GameObject.Find("PlayerInput").GetComponent<Text>();
        mask = GameObject.Find("right-arrow").GetComponent<Mask>();

        int nbPlayers = int.Parse(text.text);
        nbPlayers++ ;
        if(nbPlayers == 2){
            //mask.showMaskGraphic = true;
        }
        text.text = nbPlayers.ToString();
    }

    public void substractPlayer()
    {
        text = GameObject.Find("PlayerInput").GetComponent<Text>();
        mask = GameObject.Find("left-arrow").GetComponent<Mask>();

        int nbPlayers = int.Parse(text.text);

        if(nbPlayers < 2)
        {
            nbPlayers --;
            if(nbPlayers == 2)
            {
            // mask.showMaskGraphic = false;
            }
        }
        
        text.text = nbPlayers.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        

        addPlayers = GameObject.Find("right-arrow").GetComponent<Button>();
        subPlayers = GameObject.Find("left-arrow").GetComponent<Button>();
        
        addPlayers.onClick.AddListener(addPlayer);
        subPlayers.onClick.AddListener(substractPlayer);
    }

    
}
