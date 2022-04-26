using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseMenu : MonoBehaviour
{
    bool anim = false;
    public float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
      gameObject.GetComponent<Button>().onClick.AddListener(func);
    }

    // Update is called once per frame
    void Update()
    {
      if (anim)
      {
        Vector2 target = new Vector2(-800,0);
        RectTransform rt = GameObject.Find("CanvasMenu").GetComponent<RectTransform>();
        rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition,target,speed*Time.deltaTime);
        if (rt.anchoredPosition.x<=-700)
        {
          anim = false;
          this.enabled = false;
        }
      }
    }

    void func()
    {
      anim = true;
    }
}
