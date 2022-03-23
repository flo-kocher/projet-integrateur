using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blink : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Color plein = new Color(1f,1f,1f,1f);
        Color vide = new Color(1f,1f,1f,0f);
        float lerp = Mathf.PingPong(speed*Time.time,1);
	      GetComponent<Renderer>().material.color = Color.Lerp(plein,vide,lerp);
    }
}
