using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Script qui met la minimap transparante lorsqu'on passe la souris dessus
public class MiniMapScriptOver : MonoBehaviour,
                                 IPointerEnterHandler,
                                 IPointerExitHandler {
  private Color white = new Color(1f, 1f, 1f, 1f);
  private Color trans = new Color(1f, 1f, 1f, 0.3f);
  public int speed;
  private bool anim1 = false;
  private bool anim2 = false;

  // Start is called before the first frame update
  void Start() {}

  // Update is called once per frame
  void Update() {
    if (anim1) {
      this.gameObject.GetComponent<Graphic>().color =
          Color.Lerp(this.gameObject.GetComponent<Graphic>().color, trans,
                     speed * Time.deltaTime);
    }
    if (anim2) {
      this.gameObject.GetComponent<Graphic>().color =
          Color.Lerp(this.gameObject.GetComponent<Graphic>().color, white,
                     speed * Time.deltaTime);
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    anim2 = false;
    anim1 = true;
  }

  public void OnPointerExit(PointerEventData eventData) {
    anim1 = false;
    anim2 = true;
  }
}
