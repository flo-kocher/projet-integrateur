using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class scriptJ1 : NetworkBehaviour
{
    public PlayerManager PlayerManager;

    public float refresh = 1f;
    bool monbool = false;
    //Start is called before the first frame update
    void Start()
    {           
        if (NetworkClient.connection.identity != null && NetworkClient.connection.identity.GetComponent<PlayerManager>() != null)
            monbool = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (monbool == true)
        {
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                int score = PlayerManager.list_of_struct_player[0].points;
                GetComponent<Text>().text = "test score"+score;
            }
            if (Input.GetKeyUp(KeyCode.Tab))
                GetComponent<Text>().text = "Test";  
        }
        if (monbool == false)
        {
             if (Input.GetKeyDown(KeyCode.Tab))
            {
                GetComponent<Text>().text = "Score J1 : 0";
            }
            if (Input.GetKeyUp(KeyCode.Tab))
                GetComponent<Text>().text = "";  
        }
    } 
}

