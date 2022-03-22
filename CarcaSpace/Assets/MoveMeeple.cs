using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMeeple : MonoBehaviour
{
    // Start is called before the first frame update
    public bool nord;
    public bool sud;
    public bool est;
    public bool ouest;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nord = false;
        sud = false;
        est = false;
        ouest = false;
        float x = transform.position.x-(transform.position.x%0.33f);
        float y = transform.position.y-(transform.position.y%0.33f);
        if (y<0.33f)
          sud = true;
        if (y>=0.66f)
          nord = true;
        if (x<0.33f)
          ouest = true;
        if (x>=0.66f) 
          est = true;
        if (!((sud && ouest) || (sud && est) || (nord && ouest) || (nord && est)))
           transform.position = new Vector2(x+0.17f,y+0.17f); 
    }
}
