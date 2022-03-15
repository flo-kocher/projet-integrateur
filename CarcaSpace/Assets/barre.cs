using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barre : MonoBehaviour
{
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if(time <= 61f)
      {
        float decrease = Time.deltaTime*16.6f;
        Color c = GetComponent<Image>().color;
        if (time <= 30f)
        {
          c.g -= Time.deltaTime*0.016f;
          c.r += Time.deltaTime*0.016f;
        }
        else
        {
          c.g -= Time.deltaTime*0.01f;
        }
        GetComponent<Image>().color = c;
        GameObject.Find("Compteur").GetComponent<Text>().color = c;
        RectTransform rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(rt.sizeDelta.x-decrease,rt.sizeDelta.y);
      }
      time += Time.deltaTime;
    }
}
