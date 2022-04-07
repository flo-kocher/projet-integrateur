using UnityEngine;
using UnityEngine.UI;

namespace Mirror.Examples.MultipleMatch
{
    public class PlayerGUI : MonoBehaviour
    {
        public Text playerName;

        public void SetPlayerInfo(PlayerInfo info)
        {
<<<<<<< HEAD
            playerName.text = $"Player {info.playerIndex}";
=======
            playerName.text = "Player " + info.playerIndex;
>>>>>>> origin/alpha_merge
            playerName.color = info.ready ? Color.green : Color.red;
        }
    }
}