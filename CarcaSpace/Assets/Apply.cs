using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Apply : MonoBehaviour {

  public GameObject ButtonPick;

  // Start is called before the first frame update
  void Start() { gameObject.GetComponent<Button>().onClick.AddListener(func); }

  // Update is called once per frame
  void Update() {}

  void func() {
    this.gameObject.SetActive(false);
    var list = Resources.FindObjectsOfTypeAll<GameObject>();
    foreach (GameObject i in list) {
      if (i.name.Contains("Clone")) {
        if (i.GetComponent<rotateZ>().enabled != true)
        {
          i.GetComponent<AccessDenied>().enabled = false;
          i.GetComponent<Move>().enabled = false;
          i.GetComponent<NotifDenied>().enabled = false;
          i.GetComponent<Constraints>().enabled = false;
          i.GetComponent<MoveFirst>().enabled = false;
        }
      }
    }
    ButtonPick.SetActive(true);
  }
}
