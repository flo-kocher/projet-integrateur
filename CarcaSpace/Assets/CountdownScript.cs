using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    public float time = 60f;
    bool CountDownOn = true;

    void Start()
    {
        StartCoroutine (timer ());
    }

    void Update()
    {
        
    }

    IEnumerator timer()
    {
        while(time>0)
        {
            time--;
            yield return new WaitForSeconds (1f);
            GetComponent<Text> ().text = "" +time;
        }
    }
    
}