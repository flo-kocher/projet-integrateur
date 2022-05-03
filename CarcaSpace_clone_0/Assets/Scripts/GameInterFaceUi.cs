using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameInterFaceUi : NetworkBehaviour
{

    [SerializeField] public Button  PickButton ;
    [SerializeField] public GameObject Barre ;

      [SyncVar]
    float time = 0;

    public PlayerManager PlayerManager;
    // Start is called before the first frame update
    void Start()
    {
        PickButton.onClick.AddListener(getIdAndCards);
        Debug.Log("je suis ici");
    }

    // Update is called once per frame
    void Update() {
        // Tant que le timer n'as pas atteint 61s
        if (time <= 61f) {
        float decrease = Time.deltaTime * 4.98f;
        Color c = Barre.GetComponent<Image>().color;
        if (time <= 30f) {
            c.g -= Time.deltaTime * 0.016f;
            c.r += Time.deltaTime * 0.016f;
        } else {
            c.g -= Time.deltaTime * 0.01f;
        }
        Barre.GetComponent<Image>().color = c;
        RectTransform rt = Barre.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x - decrease, rt.sizeDelta.y);
        }
        time += Time.deltaTime;
    }

    void getIdAndCards()
    {
        // cherche l'identifiant du network
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        // client envoie une requête au serveur pour générer une tuile
        PlayerManager.CmdDealTiles();
    }
}
