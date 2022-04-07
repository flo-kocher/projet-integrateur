using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.Basic
{
    public class PlayerUI : MonoBehaviour
    {
        [Header("Player Components")]
        public Image image;

        [Header("Child Text Objects")]
        public Text playerNameText;
        public Text playerDataText;

<<<<<<< HEAD
=======
        Player player;

>>>>>>> origin/alpha_merge
        /// <summary>
        /// Caches the controlling Player object, subscribes to its events
        /// </summary>
        /// <param name="player">Player object that controls this UI</param>
        /// <param name="isLocalPlayer">true if the Player object is the Local Player</param>
<<<<<<< HEAD
        public void SetLocalPlayer()
        {
            // add a visual background for the local player in the UI
            image.color = new Color(1f, 1f, 1f, 0.1f);
        }

        // This value can change as clients leave and join
        public void OnPlayerNumberChanged(byte newPlayerNumber)
=======
        public void SetPlayer(Player player, bool isLocalPlayer)
        {
            // cache reference to the player that controls this UI object
            this.player = player;

            // subscribe to the events raised by SyncVar Hooks on the Player object
            player.OnPlayerNumberChanged += OnPlayerNumberChanged;
            player.OnPlayerColorChanged += OnPlayerColorChanged;
            player.OnPlayerDataChanged += OnPlayerDataChanged;

            // add a visual background for the local player in the UI
            if (isLocalPlayer)
                image.color = new Color(1f, 1f, 1f, 0.1f);
        }

        void OnDisable()
        {
            player.OnPlayerNumberChanged -= OnPlayerNumberChanged;
            player.OnPlayerColorChanged -= OnPlayerColorChanged;
            player.OnPlayerDataChanged -= OnPlayerDataChanged;
        }

        // This value can change as clients leave and join
        void OnPlayerNumberChanged(int newPlayerNumber)
>>>>>>> origin/alpha_merge
        {
            playerNameText.text = string.Format("Player {0:00}", newPlayerNumber);
        }

        // Random color set by Player::OnStartServer
<<<<<<< HEAD
        public void OnPlayerColorChanged(Color32 newPlayerColor)
=======
        void OnPlayerColorChanged(Color32 newPlayerColor)
>>>>>>> origin/alpha_merge
        {
            playerNameText.color = newPlayerColor;
        }

        // This updates from Player::UpdateData via InvokeRepeating on server
<<<<<<< HEAD
        public void OnPlayerDataChanged(ushort newPlayerData)
=======
        void OnPlayerDataChanged(int newPlayerData)
>>>>>>> origin/alpha_merge
        {
            // Show the data in the UI
            playerDataText.text = string.Format("Data: {0:000}", newPlayerData);
        }
<<<<<<< HEAD
=======

>>>>>>> origin/alpha_merge
    }
}
