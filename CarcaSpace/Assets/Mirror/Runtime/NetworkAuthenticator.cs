using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mirror
{
<<<<<<< HEAD
    [Serializable] public class UnityEventNetworkConnection : UnityEvent<NetworkConnectionToClient> {}

    /// <summary>Base class for implementing component-based authentication during the Connect phase</summary>
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-authenticators")]
=======
    [Serializable] public class UnityEventNetworkConnection : UnityEvent<NetworkConnection> {}

    /// <summary>Base class for implementing component-based authentication during the Connect phase</summary>
    [HelpURL("https://mirror-networking.com/docs/Articles/Guides/Authentication.html")]
>>>>>>> origin/alpha_merge
    public abstract class NetworkAuthenticator : MonoBehaviour
    {
        /// <summary>Notify subscribers on the server when a client is authenticated</summary>
        [Header("Event Listeners (optional)")]
        [Tooltip("Mirror has an internal subscriber to this event. You can add your own here.")]
        public UnityEventNetworkConnection OnServerAuthenticated = new UnityEventNetworkConnection();

        /// <summary>Notify subscribers on the client when the client is authenticated</summary>
        [Tooltip("Mirror has an internal subscriber to this event. You can add your own here.")]
<<<<<<< HEAD
        public UnityEvent OnClientAuthenticated = new UnityEvent();
=======
        public UnityEventNetworkConnection OnClientAuthenticated = new UnityEventNetworkConnection();
>>>>>>> origin/alpha_merge

        /// <summary>Called when server starts, used to register message handlers if needed.</summary>
        public virtual void OnStartServer() {}

        /// <summary>Called when server stops, used to unregister message handlers if needed.</summary>
        public virtual void OnStopServer() {}

        /// <summary>Called on server from OnServerAuthenticateInternal when a client needs to authenticate</summary>
<<<<<<< HEAD
        public virtual void OnServerAuthenticate(NetworkConnectionToClient conn) {}

        protected void ServerAccept(NetworkConnectionToClient conn)
=======
        public abstract void OnServerAuthenticate(NetworkConnection conn);

        protected void ServerAccept(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            OnServerAuthenticated.Invoke(conn);
        }

<<<<<<< HEAD
        protected void ServerReject(NetworkConnectionToClient conn)
=======
        protected void ServerReject(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            conn.Disconnect();
        }

        /// <summary>Called when client starts, used to register message handlers if needed.</summary>
        public virtual void OnStartClient() {}

        /// <summary>Called when client stops, used to unregister message handlers if needed.</summary>
        public virtual void OnStopClient() {}

        /// <summary>Called on client from OnClientAuthenticateInternal when a client needs to authenticate</summary>
<<<<<<< HEAD
        public virtual void OnClientAuthenticate() {}

        protected void ClientAccept()
        {
            OnClientAuthenticated.Invoke();
        }

        protected void ClientReject()
        {
            // Set this on the client for local reference
            NetworkClient.connection.isAuthenticated = false;

            // disconnect the client
            NetworkClient.connection.Disconnect();
        }
        
        // Reset() instead of OnValidate():
        // Any NetworkAuthenticator assigns itself to the NetworkManager, this is fine on first adding it, 
        // but if someone intentionally sets Authenticator to null on the NetworkManager again then the 
        // Authenticator will reassign itself if a value in the inspector is changed.
        // My change switches OnValidate to Reset since Reset is only called when the component is first 
        // added (or reset is pressed).
        void Reset()
=======
        // TODO client callbacks don't need NetworkConnection parameter. use NetworkClient.connection!
        public abstract void OnClientAuthenticate(NetworkConnection conn);

        // TODO client callbacks don't need NetworkConnection parameter. use NetworkClient.connection!
        protected void ClientAccept(NetworkConnection conn)
        {
            OnClientAuthenticated.Invoke(conn);
        }

        // TODO client callbacks don't need NetworkConnection parameter. use NetworkClient.connection!
        protected void ClientReject(NetworkConnection conn)
        {
            // Set this on the client for local reference
            conn.isAuthenticated = false;

            // disconnect the client
            conn.Disconnect();
        }

        void OnValidate()
>>>>>>> origin/alpha_merge
        {
#if UNITY_EDITOR
            // automatically assign authenticator field if we add this to NetworkManager
            NetworkManager manager = GetComponent<NetworkManager>();
            if (manager != null && manager.authenticator == null)
            {
<<<<<<< HEAD
                // undo has to be called before the change happens
                UnityEditor.Undo.RecordObject(manager, "Assigned NetworkManager authenticator");
                manager.authenticator = this;
=======
                manager.authenticator = this;
                UnityEditor.Undo.RecordObject(gameObject, "Assigned NetworkManager authenticator");
>>>>>>> origin/alpha_merge
            }
#endif
        }
    }
}
