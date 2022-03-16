using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMeeple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x-(transform.position.x%0.33f);
        float y = transform.position.y-(transform.position.y%0.33f);
        transform.position = new Vector2(x+0.17f,y+0.17f);
    }
}
