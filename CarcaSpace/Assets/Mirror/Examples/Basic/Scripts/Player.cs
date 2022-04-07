using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Examples.Basic
{
    public class Player : NetworkBehaviour
    {
<<<<<<< HEAD
        // Events that the PlayerUI will subscribe to
        public event System.Action<byte> OnPlayerNumberChanged;
        public event System.Action<Color32> OnPlayerColorChanged;
        public event System.Action<ushort> OnPlayerDataChanged;

        // Players List to manage playerNumber
        static readonly List<Player> playersList = new List<Player>();

        [Header("Player UI")]
        public GameObject playerUIPrefab;

        GameObject playerUIObject;
        PlayerUI playerUI = null;

        #region SyncVars
=======
        // Events that the UI will subscribe to
        public event System.Action<int> OnPlayerNumberChanged;
        public event System.Action<Color32> OnPlayerColorChanged;
        public event System.Action<int> OnPlayerDataChanged;

        // Players List to manage playerNumber
        internal static readonly List<Player> playersList = new List<Player>();

        internal static void ResetPlayerNumbers()
        {
            int playerNumber = 0;
            foreach (Player player in playersList)
            {
                player.playerNumber = playerNumber++;
            }
        }

        [Header("Player UI")]
        public GameObject playerUIPrefab;
        GameObject playerUI;
>>>>>>> origin/alpha_merge

        [Header("SyncVars")]

        /// <summary>
        /// This is appended to the player name text, e.g. "Player 01"
        /// </summary>
        [SyncVar(hook = nameof(PlayerNumberChanged))]
<<<<<<< HEAD
        public byte playerNumber = 0;
=======
        public int playerNumber = 0;

        /// <summary>
        /// This is updated by UpdateData which is called from OnStartServer via InvokeRepeating
        /// </summary>
        [SyncVar(hook = nameof(PlayerDataChanged))]
        public int playerData = 0;
>>>>>>> origin/alpha_merge

        /// <summary>
        /// Random color for the playerData text, assigned in OnStartServer
        /// </summary>
        [SyncVar(hook = nameof(PlayerColorChanged))]
        public Color32 playerColor = Color.white;

<<<<<<< HEAD
        /// <summary>
        /// This is updated by UpdateData which is called from OnStartServer via InvokeRepeating
        /// </summary>
        [SyncVar(hook = nameof(PlayerDataChanged))]
        public ushort playerData = 0;

        // This is called by the hook of playerNumber SyncVar above
        void PlayerNumberChanged(byte _, byte newPlayerNumber)
=======
        // This is called by the hook of playerNumber SyncVar above
        void PlayerNumberChanged(int _, int newPlayerNumber)
>>>>>>> origin/alpha_merge
        {
            OnPlayerNumberChanged?.Invoke(newPlayerNumber);
        }

<<<<<<< HEAD
=======
        // This is called by the hook of playerData SyncVar above
        void PlayerDataChanged(int _, int newPlayerData)
        {
            OnPlayerDataChanged?.Invoke(newPlayerData);
        }

>>>>>>> origin/alpha_merge
        // This is called by the hook of playerColor SyncVar above
        void PlayerColorChanged(Color32 _, Color32 newPlayerColor)
        {
            OnPlayerColorChanged?.Invoke(newPlayerColor);
        }

<<<<<<< HEAD
        // This is called by the hook of playerData SyncVar above
        void PlayerDataChanged(ushort _, ushort newPlayerData)
        {
            OnPlayerDataChanged?.Invoke(newPlayerData);
        }

        #endregion

        #region Server

=======
>>>>>>> origin/alpha_merge
        /// <summary>
        /// This is invoked for NetworkBehaviour objects when they become active on the server.
        /// <para>This could be triggered by NetworkServer.Listen() for objects in the scene, or by NetworkServer.Spawn() for objects that are dynamically created.</para>
        /// <para>This will be called for objects on a "host" as well as for object on a dedicated server.</para>
        /// </summary>
        public override void OnStartServer()
        {
            base.OnStartServer();

            // Add this to the static Players List
            playersList.Add(this);

            // set the Player Color SyncVar
            playerColor = Random.ColorHSV(0f, 1f, 0.9f, 0.9f, 1f, 1f);

<<<<<<< HEAD
            // set the initial player data
            playerData = (ushort)Random.Range(100, 1000);

=======
>>>>>>> origin/alpha_merge
            // Start generating updates
            InvokeRepeating(nameof(UpdateData), 1, 1);
        }

<<<<<<< HEAD
        // This is called from BasicNetManager OnServerAddPlayer and OnServerDisconnect
        // Player numbers are reset whenever a player joins / leaves
        [ServerCallback]
        internal static void ResetPlayerNumbers()
        {
            byte playerNumber = 0;
            foreach (Player player in playersList)
                player.playerNumber = playerNumber++;
        }

        // This only runs on the server, called from OnStartServer via InvokeRepeating
        [ServerCallback]
        void UpdateData()
        {
            playerData = (ushort)Random.Range(100, 1000);
        }

=======
>>>>>>> origin/alpha_merge
        /// <summary>
        /// Invoked on the server when the object is unspawned
        /// <para>Useful for saving object data in persistent storage</para>
        /// </summary>
        public override void OnStopServer()
        {
            CancelInvoke();
            playersList.Remove(this);
        }

<<<<<<< HEAD
        #endregion

        #region Client
=======
        // This only runs on the server, called from OnStartServer via InvokeRepeating
        [ServerCallback]
        void UpdateData()
        {
            playerData = Random.Range(100, 1000);
        }
>>>>>>> origin/alpha_merge

        /// <summary>
        /// Called on every NetworkBehaviour when it is activated on a client.
        /// <para>Objects on the host have this function called, as there is a local client on the host. The values of SyncVars on object are guaranteed to be initialized correctly with the latest state from the server when this function is called on the client.</para>
        /// </summary>
        public override void OnStartClient()
        {
<<<<<<< HEAD
            Debug.Log("OnStartClient");

            // Instantiate the player UI as child of the Players Panel
            playerUIObject = Instantiate(playerUIPrefab, CanvasUI.instance.playersPanel);
            playerUI = playerUIObject.GetComponent<PlayerUI>();

            // wire up all events to handlers in PlayerUI
            OnPlayerNumberChanged = playerUI.OnPlayerNumberChanged;
            OnPlayerColorChanged = playerUI.OnPlayerColorChanged;
            OnPlayerDataChanged = playerUI.OnPlayerDataChanged;

            // Invoke all event handlers with the initial data from spawn payload
=======
            // Activate the main panel
            ((BasicNetManager)NetworkManager.singleton).mainPanel.gameObject.SetActive(true);

            // Instantiate the player UI as child of the Players Panel
            playerUI = Instantiate(playerUIPrefab, ((BasicNetManager)NetworkManager.singleton).playersPanel);

            // Set this player object in PlayerUI to wire up event handlers
            playerUI.GetComponent<PlayerUI>().SetPlayer(this, isLocalPlayer);

            // Invoke all event handlers with the current data
>>>>>>> origin/alpha_merge
            OnPlayerNumberChanged.Invoke(playerNumber);
            OnPlayerColorChanged.Invoke(playerColor);
            OnPlayerDataChanged.Invoke(playerData);
        }

        /// <summary>
<<<<<<< HEAD
        /// Called when the local player object has been set up.
        /// <para>This happens after OnStartClient(), as it is triggered by an ownership message from the server. This is an appropriate place to activate components or functionality that should only be active for the local player, such as cameras and input.</para>
        /// </summary>
        public override void OnStartLocalPlayer()
        {
            Debug.Log("OnStartLocalPlayer");

            // Set isLocalPlayer for this Player in UI for background shading
            playerUI.SetLocalPlayer();

            // Activate the main panel
            CanvasUI.instance.mainPanel.gameObject.SetActive(true);
        }

        /// <summary>
        /// Called when the local player object is being stopped.
        /// <para>This happens before OnStopClient(), as it may be triggered by an ownership message from the server, or because the player object is being destroyed. This is an appropriate place to deactivate components or functionality that should only be active for the local player, such as cameras and input.</para>
        /// </summary>
        public override void OnStopLocalPlayer()
        {
            // Disable the main panel for local player
            CanvasUI.instance.mainPanel.gameObject.SetActive(false);
        }

        /// <summary>
=======
>>>>>>> origin/alpha_merge
        /// This is invoked on clients when the server has caused this object to be destroyed.
        /// <para>This can be used as a hook to invoke effects or do client specific cleanup.</para>
        /// </summary>
        public override void OnStopClient()
        {
<<<<<<< HEAD
            // disconnect event handlers
            OnPlayerNumberChanged = null;
            OnPlayerColorChanged = null;
            OnPlayerDataChanged = null;

            // Remove this player's UI object
            Destroy(playerUIObject);
        }

        #endregion
=======
            // Remove this player's UI object
            Destroy(playerUI);

            // Disable the main panel for local player
            if (isLocalPlayer)
                ((BasicNetManager)NetworkManager.singleton).mainPanel.gameObject.SetActive(false);
        }
>>>>>>> origin/alpha_merge
    }
}
