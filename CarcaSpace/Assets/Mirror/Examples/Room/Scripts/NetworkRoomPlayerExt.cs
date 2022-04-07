using UnityEngine;
<<<<<<< HEAD
using UnityEngine.SceneManagement;
=======
>>>>>>> origin/alpha_merge

namespace Mirror.Examples.NetworkRoom
{
    [AddComponentMenu("")]
    public class NetworkRoomPlayerExt : NetworkRoomPlayer
    {
        public override void OnStartClient()
        {
<<<<<<< HEAD
            //Debug.Log($"OnStartClient {gameObject}");
=======
            // Debug.LogFormat(LogType.Log, "OnStartClient {0}", SceneManager.GetActiveScene().path);

            base.OnStartClient();
>>>>>>> origin/alpha_merge
        }

        public override void OnClientEnterRoom()
        {
<<<<<<< HEAD
            //Debug.Log($"OnClientEnterRoom {SceneManager.GetActiveScene().path}");
=======
            // Debug.LogFormat(LogType.Log, "OnClientEnterRoom {0}", SceneManager.GetActiveScene().path);
>>>>>>> origin/alpha_merge
        }

        public override void OnClientExitRoom()
        {
<<<<<<< HEAD
            //Debug.Log($"OnClientExitRoom {SceneManager.GetActiveScene().path}");
        }

        public override void IndexChanged(int oldIndex, int newIndex)
        {
            //Debug.Log($"IndexChanged {newIndex}");
=======
            // Debug.LogFormat(LogType.Log, "OnClientExitRoom {0}", SceneManager.GetActiveScene().path);
>>>>>>> origin/alpha_merge
        }

        public override void ReadyStateChanged(bool oldReadyState, bool newReadyState)
        {
<<<<<<< HEAD
            //Debug.Log($"ReadyStateChanged {newReadyState}");
        }

        public override void OnGUI()
        {
            base.OnGUI();
=======
            // Debug.LogFormat(LogType.Log, "ReadyStateChanged {0}", newReadyState);
>>>>>>> origin/alpha_merge
        }
    }
}
