using UnityEngine;

/*
<<<<<<< HEAD
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
=======
	Documentation: https://mirror-networking.com/docs/Components/NetworkManager.html
>>>>>>> origin/alpha_merge
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace Mirror.Examples.MultipleMatch
{
    [AddComponentMenu("")]
    public class MatchNetworkManager : NetworkManager
    {
        [Header("Match GUI")]
        public GameObject canvas;
        public CanvasController canvasController;

        #region Unity Callbacks

        /// <summary>
        /// Runs on both Server and Client
        /// Networking is NOT initialized when this fires
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            canvasController.InitializeData();
        }

        #endregion

        #region Server System Callbacks

        /// <summary>
        /// Called on the server when a client is ready.
        /// <para>The default implementation of this function calls NetworkServer.SetClientReady() to continue the network setup process.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
<<<<<<< HEAD
        public override void OnServerReady(NetworkConnectionToClient conn)
=======
        public override void OnServerReady(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            base.OnServerReady(conn);
            canvasController.OnServerReady(conn);
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
            canvasController.OnServerDisconnect(conn);
            base.OnServerDisconnect(conn);
        }

        #endregion

        #region Client System Callbacks

        /// <summary>
        /// Called on the client when connected to a server.
        /// <para>The default implementation of this function sets the client as ready and adds a player. Override the function to dictate what happens when the client connects.</para>
        /// </summary>
<<<<<<< HEAD
        public override void OnClientConnect()
        {
            base.OnClientConnect();
            canvasController.OnClientConnect();
=======
        /// <param name="conn">Connection to the server.</param>
        public override void OnClientConnect(NetworkConnection conn)
        {
            base.OnClientConnect(conn);
            canvasController.OnClientConnect(conn);
>>>>>>> origin/alpha_merge
        }

        /// <summary>
        /// Called on clients when disconnected from a server.
        /// <para>This is called on the client when it disconnects from the server. Override this function to decide what happens when the client disconnects.</para>
        /// </summary>
<<<<<<< HEAD
        public override void OnClientDisconnect()
        {
            canvasController.OnClientDisconnect();
            base.OnClientDisconnect();
=======
        /// <param name="conn">Connection to the server.</param>
        public override void OnClientDisconnect(NetworkConnection conn)
        {
            canvasController.OnClientDisconnect();
            base.OnClientDisconnect(conn);
>>>>>>> origin/alpha_merge
        }

        #endregion

        #region Start & Stop Callbacks

        /// <summary>
        /// This is invoked when a server is started - including when a host is started.
        /// <para>StartServer has multiple signatures, but they all cause this hook to be called.</para>
        /// </summary>
        public override void OnStartServer()
        {
<<<<<<< HEAD
            if (mode == NetworkManagerMode.ServerOnly)
=======
            if (mode == NetworkManagerMode.ServerOnly) 
>>>>>>> origin/alpha_merge
                canvas.SetActive(true);

            canvasController.OnStartServer();
        }

        /// <summary>
        /// This is invoked when the client is started.
        /// </summary>
        public override void OnStartClient()
        {
            canvas.SetActive(true);
            canvasController.OnStartClient();
        }

        /// <summary>
        /// This is called when a server is stopped - including when a host is stopped.
        /// </summary>
        public override void OnStopServer()
        {
            canvasController.OnStopServer();
            canvas.SetActive(false);
        }

        /// <summary>
        /// This is called when a client is stopped.
        /// </summary>
        public override void OnStopClient()
        {
            canvasController.OnStopClient();
        }

        #endregion
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> origin/alpha_merge
