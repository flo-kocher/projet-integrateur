using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror ; 

// Script qui va modifier la barre de timer
public class barre : NetworkBehaviour {
  float time = 0;
  public static bool first = true;
  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {
    // Tant que le timer n'as pas atteint 61s
    if(isLocalPlayer){
      if (time <= 61f) {
      float decrease = Time.deltaTime * 4.98f;
      Color c = GetComponent<Image>().color;
      if (time <= 30f) {
        c.g -= Time.deltaTime * 0.016f;
        c.r += Time.deltaTime * 0.016f;
      } else {
        c.g -= Time.deltaTime * 0.01f;
      }
      GetComponent<Image>().color = c;
      RectTransform rt = GetComponent<RectTransform>();
      rt.sizeDelta = new Vector2(rt.sizeDelta.x - decrease, rt.sizeDelta.y);
    }
    else{
      if(first){
        
        first = false;
      }
      
    }
    time += Time.deltaTime;
    }
    
  }
}
