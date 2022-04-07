<<<<<<< HEAD
using System.Collections.Generic;
=======
using System;
using UnityEngine;
>>>>>>> origin/alpha_merge

namespace Mirror.Examples.Chat
{
    public class Player : NetworkBehaviour
    {
<<<<<<< HEAD
        public static readonly HashSet<string> playerNames = new HashSet<string>();

        [SyncVar(hook = nameof(OnPlayerNameChanged))]
        public string playerName;

        // RuntimeInitializeOnLoadMethod -> fast playmode without domain reload
        [UnityEngine.RuntimeInitializeOnLoadMethod]
        static void ResetStatics()
        {
            playerNames.Clear();
        }

        void OnPlayerNameChanged(string _, string newName)
        {
            ChatUI.instance.localPlayerName = playerName;
        }

        public override void OnStartServer()
        {
            playerName = (string)connectionToClient.authenticationData;
=======
        [SyncVar]
        public string playerName;

        public static event Action<Player, string> OnMessage;

        [Command]
        public void CmdSend(string message)
        {
            if (message.Trim() != "")
                RpcReceive(message.Trim());
        }

        [ClientRpc]
        public void RpcReceive(string message)
        {
            OnMessage?.Invoke(this, message);
>>>>>>> origin/alpha_merge
        }
    }
}
