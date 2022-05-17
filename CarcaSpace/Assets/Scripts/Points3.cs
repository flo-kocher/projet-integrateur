using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;


public class Points3 : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Points3>().GetComponent<Text>().text = "Player 3 : 0";

    }

    // Update is called once per frame
    void Update()
    {
        // this.GetComponent<Points3>().GetComponent<Text>().text = Move.points3;
        // this.GetComponent<Points>().GetComponent<Text>().text = "okkk";
        // Debug.Log(this.GetComponent<Points>().GetComponent<Text>().text);
        // NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        // PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        /*
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //int score = PlayerManager.list_of_struct_player[0].points;
            this.GetComponent<Points>().GetComponent<Text>().text = "test score";
        }
        */
        // if (Input.GetKeyUp(KeyCode.Tab))
            

        /*
            on laisse ce code, pour qu'on ait une touche sur laquelle appuyer pour afficher les score
            on change la valeur des scores dans comptage de points qu'on on ajoute des points à un joueur
            
        */
    }
}
