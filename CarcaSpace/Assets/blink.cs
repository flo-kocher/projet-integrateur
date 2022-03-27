using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script qui va changer le material des étoiles pour les faires clignoter en
// fonction de speed
public class blink : MonoBehaviour {
  public float speed;
  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {
    Color plein = new Color(1f, 1f, 1f, 1f);
    Color vide = new Color(1f, 1f, 1f, 0f);
    float lerp = Mathf.PingPong(speed * Time.time, 1);
    transform.GetChild(0).GetComponent<Renderer>().material.color =
        Color.Lerp(vide, plein, lerp);
  }
}
