using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;
using Mirror;


public class ChatUI : NetworkBehaviour
{


    [SerializeField] public GameObject chatUI = null;
    [SerializeField] public TMP_Text chatText = null;
    [SerializeField] public TMP_InputField inputField ;
     

    [SerializeField] public Button sendButton ; 


    private static event Action<string> OnMessage;

    public override void OnStartAuthority()
    {
        chatUI.SetActive(true);
        

        OnMessage += HandleNewMessage;
    }

    public override void OnStartClient(){
        base.OnStartClient();
        sendButton.onClick.AddListener(sendMessage);
    }

    

    public void sendMessage(){
        string message = inputField.GetComponent<TMP_InputField>().text;
        string playerName =  PlayerPrefs.GetString("playerName");
        Debug.Log($"sending {message}");
        Debug.Log("sending");
        Send(message,playerName);
    }

    [ClientCallback]
    private void OnDestroy()
    {
        if (!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }


    private void HandleNewMessage(string message)
    {
        chatText.text += message;
    }

    [Client]
    public void Send(string message,string playerName){
    //     message = inputField.text ;
    //     Debug.Log($"sending {message}");
    //     Debug.Log("sending");

        // if (!Input.GetKeyDown(KeyCode.Return)) { 
        //     Debug.Log("ici 1 ");
        //     return; 
        // }

        if (string.IsNullOrWhiteSpace(message)) { 
            Debug.Log("ici 2");
            return; 
        }
        Debug.Log("ici 3");
        CmdSendMessage(message,playerName);
        inputField.text = string.Empty;
    }

    [Command(requiresAuthority = false)]
    private void CmdSendMessage(string message,string playerName)
    {
        Debug.Log("CmdSend");
        RpcHandleMessage($"[{playerName}]: {message}");;
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        
        HandleNewMessage(message);
        Debug.Log($"je recois le message {message}") ; 
    }
}