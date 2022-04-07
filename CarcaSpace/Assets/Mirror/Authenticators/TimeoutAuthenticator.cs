using System.Collections;
using UnityEngine;

namespace Mirror.Authenticators
{
    /// <summary>
    /// An authenticator that disconnects connections if they don't
    /// authenticate within a specified time limit.
    /// </summary>
<<<<<<< HEAD
    [AddComponentMenu("Network/ Authenticators/Timeout Authenticator")]
=======
    [AddComponentMenu("Network/Authenticators/TimeoutAuthenticator")]
>>>>>>> origin/alpha_merge
    public class TimeoutAuthenticator : NetworkAuthenticator
    {
        public NetworkAuthenticator authenticator;

        [Range(0, 600), Tooltip("Timeout to auto-disconnect in seconds. Set to 0 for no timeout.")]
        public float timeout = 60;

        public void Awake()
        {
            authenticator.OnServerAuthenticated.AddListener(connection => OnServerAuthenticated.Invoke(connection));
<<<<<<< HEAD
            authenticator.OnClientAuthenticated.AddListener(OnClientAuthenticated.Invoke);
=======
            authenticator.OnClientAuthenticated.AddListener(connection => OnClientAuthenticated.Invoke(connection));
>>>>>>> origin/alpha_merge
        }

        public override void OnStartServer()
        {
            authenticator.OnStartServer();
        }

        public override void OnStopServer()
        {
            authenticator.OnStopServer();
        }

        public override void OnStartClient()
        {
            authenticator.OnStartClient();
        }

        public override void OnStopClient()
        {
            authenticator.OnStopClient();
        }

<<<<<<< HEAD
        public override void OnServerAuthenticate(NetworkConnectionToClient conn)
=======
        public override void OnServerAuthenticate(NetworkConnection conn)
>>>>>>> origin/alpha_merge
        {
            authenticator.OnServerAuthenticate(conn);
            if (timeout > 0)
                StartCoroutine(BeginAuthentication(conn));
        }

<<<<<<< HEAD
        public override void OnClientAuthenticate()
        {
            authenticator.OnClientAuthenticate();
            if (timeout > 0)
                StartCoroutine(BeginAuthentication(NetworkClient.connection));
=======
        public override void OnClientAuthenticate(NetworkConnection conn)
        {
            authenticator.OnClientAuthenticate(conn);
            if (timeout > 0)
                StartCoroutine(BeginAuthentication(conn));
>>>>>>> origin/alpha_merge
        }

        IEnumerator BeginAuthentication(NetworkConnection conn)
        {
<<<<<<< HEAD
            //Debug.Log($"Authentication countdown started {conn} {timeout}");
=======
            // Debug.Log($"Authentication countdown started {conn} {timeout}");

>>>>>>> origin/alpha_merge
            yield return new WaitForSecondsRealtime(timeout);

            if (!conn.isAuthenticated)
            {
<<<<<<< HEAD
                Debug.LogError($"Authentication Timeout - Disconnecting {conn}");
=======
                // Debug.Log($"Authentication Timeout {conn}");

>>>>>>> origin/alpha_merge
                conn.Disconnect();
            }
        }
    }
}
