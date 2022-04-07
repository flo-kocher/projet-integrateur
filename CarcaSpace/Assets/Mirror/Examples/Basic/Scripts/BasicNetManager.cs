<<<<<<< HEAD
using UnityEngine;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
=======
using System.Collections.Generic;
using UnityEngine;

/*
	Documentation: https://mirror-networking.com/docs/Articles/Components/NetworkManager.html
>>>>>>> origin/alpha_merge
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace Mirror.Examples.Basic
{
    [AddComponentMenu("")]
    public class BasicNetManager : NetworkManager
    {
<<<<<<< HEAD
=======
        [Header("Canvas UI")]

        [Tooltip("Assign Main Panel so it can be turned on from Player:OnStartClient")]
        public RectTransform mainPanel;

        [Tooltip("Assign Players Panel for instantiating PlayerUI as child")]
        public RectTransform playersPanel;

>>>>>>> origin/alpha_merge
        /// <summary>
        /// Called on the server when a client adds a new player with NetworkClient.AddPlayer.
        /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
<<<<<<< HEAD
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
=======
        public override void OnServerAddPlayer(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            base.OnServerAddPlayer(conn);
            Player.ResetPlayerNumbers();
        }

        /// <summary>
        /// Called on the server when a client disconnects.
        /// <para>This is called on the Server when a Client disconnects from the Server. Use an override to decide what should happen when a disconnection is detected.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
<<<<<<< HEAD
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
=======
        public override void OnServerDisconnect(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            base.OnServerDisconnect(conn);
            Player.ResetPlayerNumbers();
        }
<<<<<<< HEAD
=======

>>>>>>> origin/alpha_merge
    }
}
