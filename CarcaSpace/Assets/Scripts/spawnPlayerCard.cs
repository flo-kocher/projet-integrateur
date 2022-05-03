using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;
using Mirror ; 


public class spawnPlayerCard : NetworkBehaviour
{
    public GameObject PlayerCard ; 
    // Start is called before the first frame update

    public void changeColor(GameObject newCard){
        newCard.transform.GetChild(1).GetComponent<Image>().color =  new Color32(255,255,225,100); 
    }

    [Command]
    public void CmdSpawnCard(int index){
        Debug.Log("spawning player card ");
        GameObject newCard = Instantiate(PlayerCard);
        newCard.transform.SetParent(GameObject.Find("Players").transform);
        newCard.transform.GetChild(0).GetComponent<Text>().text = "Player" + index;
        
        newCard.transform.GetChild(1).GetComponent<Button>().onClick.AddListener( () => changeColor(newCard));
        NetworkServer.Spawn(PlayerCard,connectionToClient);
        RpcShowCard(PlayerCard);
    }

    [ClientRpc]
    public void RpcShowCard(GameObject Card){
        if (hasAuthority)
            {
                Card.SetActive(true);
            }
            else
            {
                Card.SetActive(true); 
            }

    }
}
