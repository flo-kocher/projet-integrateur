// using System.Collections;
// using System.Collections.Generic;
// using System.Linq;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using Mirror;


// public class ChatUI : NetworkBehaviour
// {

//     public InputField chatMessage;
//     public Text chatHistory;
//     public Scrollbar scrollbar;
//     public Button SubmitBtn;

//     public string localPlayerName;

//     Dictionary<NetworkConnectionToClient, string> connNames = new Dictionary<NetworkConnectionToClient, string>();

//     public static ChatUI instance;


//     void Start(){
//         SubmitBtn.onClick.AddListener(OnEndEdit);
//     }
//     void Awake()
//     {
//         instance = this;
//     }

//     [Command(requiresAuthority = false)]
//     public void CmdSend(string message, NetworkConnectionToClient sender = null)
//     {
//         if (!connNames.ContainsKey(sender))
//             connNames.Add(sender, sender.identity.GetComponent<RoomPlayerManager>().playerName);

//         if (!string.IsNullOrWhiteSpace(message))
//             RpcReceive(connNames[sender], message.Trim());
//     }

//     [ClientRpc]
//     public void RpcReceive(string playerName, string message)
//     {
//         string prettyMessage = playerName == localPlayerName ?
//             $"<color=red>{playerName}:</color> {message}" :
//             $"<color=blue>{playerName}:</color> {message}";
//         AppendMessage(prettyMessage);
//     }

//     // Called by UI element MessageField.OnEndEdit
//     public void OnEndEdit()
//     {
//         if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit")){
           
//             SendMessage();
//         }
            
//     }

//     // Called by OnEndEdit above and UI element SendButton.OnClick
//     public void SendMessage()
//     {
//         // if (!string.IsNullOrWhiteSpace(chatMessage.text))
//         // {
//             CmdSend(chatMessage.text.Trim());
//             chatMessage.text = string.Empty;
//             chatMessage.ActivateInputField();
//             Debug.Log($"Sending Message");
//         // }
//     }

//     internal void AppendMessage(string message)
//     {
//         StartCoroutine(AppendAndScroll(message));
//     }

//     IEnumerator AppendAndScroll(string message)
//     {
//         chatHistory.text += message + "\n";

//         // it takes 2 frames for the UI to update ?!?!
//         yield return null;
//         yield return null;

//         // slam the scrollbar down
//         scrollbar.value = 0;
//     }


// }
