using System;
using System.Collections;
<<<<<<< HEAD
using System.Collections.Generic;
=======
>>>>>>> origin/alpha_merge
using UnityEngine;

namespace Mirror.Authenticators
{
<<<<<<< HEAD
    [AddComponentMenu("Network/ Authenticators/Basic Authenticator")]
    [HelpURL("https://mirror-networking.gitbook.io/docs/components/network-authenticators/basic-authenticator")]
    public class BasicAuthenticator : NetworkAuthenticator
    {
        [Header("Server Credentials")]
        public string serverUsername;
        public string serverPassword;

        [Header("Client Credentials")]
        public string username;
        public string password;

        readonly HashSet<NetworkConnection> connectionsPendingDisconnect = new HashSet<NetworkConnection>();

=======
    [AddComponentMenu("Network/Authenticators/BasicAuthenticator")]
    public class BasicAuthenticator : NetworkAuthenticator
    {
        [Header("Custom Properties")]

        // set these in the inspector
        public string username;
        public string password;

>>>>>>> origin/alpha_merge
        #region Messages

        public struct AuthRequestMessage : NetworkMessage
        {
            // use whatever credentials make sense for your game
            // for example, you might want to pass the accessToken if using oauth
            public string authUsername;
            public string authPassword;
        }

        public struct AuthResponseMessage : NetworkMessage
        {
            public byte code;
            public string message;
        }

        #endregion

        #region Server

        /// <summary>
        /// Called on server from StartServer to initialize the Authenticator
        /// <para>Server message handlers should be registered in this method.</para>
        /// </summary>
        public override void OnStartServer()
        {
            // register a handler for the authentication request we expect from client
            NetworkServer.RegisterHandler<AuthRequestMessage>(OnAuthRequestMessage, false);
        }

        /// <summary>
        /// Called on server from StopServer to reset the Authenticator
        /// <para>Server message handlers should be registered in this method.</para>
        /// </summary>
        public override void OnStopServer()
        {
            // unregister the handler for the authentication request
            NetworkServer.UnregisterHandler<AuthRequestMessage>();
        }

        /// <summary>
        /// Called on server from OnServerAuthenticateInternal when a client needs to authenticate
        /// </summary>
        /// <param name="conn">Connection to client.</param>
<<<<<<< HEAD
        public override void OnServerAuthenticate(NetworkConnectionToClient conn)
=======
        public override void OnServerAuthenticate(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            // do nothing...wait for AuthRequestMessage from client
        }

        /// <summary>
        /// Called on server when the client's AuthRequestMessage arrives
        /// </summary>
        /// <param name="conn">Connection to client.</param>
        /// <param name="msg">The message payload</param>
<<<<<<< HEAD
        public void OnAuthRequestMessage(NetworkConnectionToClient conn, AuthRequestMessage msg)
        {
            //Debug.Log($"Authentication Request: {msg.authUsername} {msg.authPassword}");

            if (connectionsPendingDisconnect.Contains(conn)) return;

            // check the credentials by calling your web server, database table, playfab api, or any method appropriate.
            if (msg.authUsername == serverUsername && msg.authPassword == serverPassword)
=======
        public void OnAuthRequestMessage(NetworkConnection conn, AuthRequestMessage msg)
        {
            // Debug.LogFormat(LogType.Log, "Authentication Request: {0} {1}", msg.authUsername, msg.authPassword);

            // check the credentials by calling your web server, database table, playfab api, or any method appropriate.
            if (msg.authUsername == username && msg.authPassword == password)
>>>>>>> origin/alpha_merge
            {
                // create and send msg to client so it knows to proceed
                AuthResponseMessage authResponseMessage = new AuthResponseMessage
                {
                    code = 100,
                    message = "Success"
                };

                conn.Send(authResponseMessage);

                // Accept the successful authentication
                ServerAccept(conn);
            }
            else
            {
<<<<<<< HEAD
                connectionsPendingDisconnect.Add(conn);

=======
>>>>>>> origin/alpha_merge
                // create and send msg to client so it knows to disconnect
                AuthResponseMessage authResponseMessage = new AuthResponseMessage
                {
                    code = 200,
                    message = "Invalid Credentials"
                };

                conn.Send(authResponseMessage);

                // must set NetworkConnection isAuthenticated = false
                conn.isAuthenticated = false;

                // disconnect the client after 1 second so that response message gets delivered
<<<<<<< HEAD
                StartCoroutine(DelayedDisconnect(conn, 1f));
            }
        }

        IEnumerator DelayedDisconnect(NetworkConnectionToClient conn, float waitTime)
=======
                StartCoroutine(DelayedDisconnect(conn, 1));
            }
        }

        IEnumerator DelayedDisconnect(NetworkConnection conn, float waitTime)
>>>>>>> origin/alpha_merge
        {
            yield return new WaitForSeconds(waitTime);

            // Reject the unsuccessful authentication
            ServerReject(conn);
<<<<<<< HEAD

            yield return null;

            // remove conn from pending connections
            connectionsPendingDisconnect.Remove(conn);
=======
>>>>>>> origin/alpha_merge
        }

        #endregion

        #region Client

        /// <summary>
        /// Called on client from StartClient to initialize the Authenticator
        /// <para>Client message handlers should be registered in this method.</para>
        /// </summary>
        public override void OnStartClient()
        {
            // register a handler for the authentication response we expect from server
<<<<<<< HEAD
            NetworkClient.RegisterHandler<AuthResponseMessage>(OnAuthResponseMessage, false);
=======
            NetworkClient.RegisterHandler<AuthResponseMessage>((Action<AuthResponseMessage>)OnAuthResponseMessage, false);
>>>>>>> origin/alpha_merge
        }

        /// <summary>
        /// Called on client from StopClient to reset the Authenticator
        /// <para>Client message handlers should be unregistered in this method.</para>
        /// </summary>
        public override void OnStopClient()
        {
            // unregister the handler for the authentication response
            NetworkClient.UnregisterHandler<AuthResponseMessage>();
        }

        /// <summary>
        /// Called on client from OnClientAuthenticateInternal when a client needs to authenticate
        /// </summary>
<<<<<<< HEAD
        public override void OnClientAuthenticate()
=======
        /// <param name="conn">Connection of the client.</param>
        public override void OnClientAuthenticate(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            AuthRequestMessage authRequestMessage = new AuthRequestMessage
            {
                authUsername = username,
                authPassword = password
            };

<<<<<<< HEAD
            NetworkClient.connection.Send(authRequestMessage);
=======
            conn.Send(authRequestMessage);
>>>>>>> origin/alpha_merge
        }

        /// <summary>
        /// Called on client when the server's AuthResponseMessage arrives
        /// </summary>
<<<<<<< HEAD
=======
        /// <param name="conn">Connection to client.</param>
>>>>>>> origin/alpha_merge
        /// <param name="msg">The message payload</param>
        public void OnAuthResponseMessage(AuthResponseMessage msg)
        {
            if (msg.code == 100)
            {
<<<<<<< HEAD
                //Debug.Log($"Authentication Response: {msg.message}");

                // Authentication has been accepted
                ClientAccept();
=======
                // Debug.LogFormat(LogType.Log, "Authentication Response: {0}", msg.message);

                // Authentication has been accepted
                ClientAccept(NetworkClient.connection);
>>>>>>> origin/alpha_merge
            }
            else
            {
                Debug.LogError($"Authentication Response: {msg.message}");

                // Authentication has been rejected
<<<<<<< HEAD
                ClientReject();
            }
        }

=======
                ClientReject(NetworkClient.connection);
            }
        }

        [Obsolete("Call OnAuthResponseMessage without the NetworkConnection parameter. It always points to NetworkClient.connection anyway.")]
        public void OnAuthResponseMessage(NetworkConnection conn, AuthResponseMessage msg) => OnAuthResponseMessage(msg);

>>>>>>> origin/alpha_merge
        #endregion
    }
}
