using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotifDenied : MonoBehaviour
{
    bool show = false;
    bool hide = false;
    GameObject canvas;
    public float speedShow;
    public float wait;
    float timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
      canvas = GameObject.Find("CanvasNotif");
    }

    // Update is called once per frame
    void Update()
    {

      if(show)
      {
        canvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(canvas.GetComponent<CanvasGroup>().alpha,1,speedShow*Time.deltaTime);
        timer += Time.deltaTime;
        if (timer>wait)
        {
          show=false;
          timer = 0;
          hide = true;
        }
      }

      if(hide)
      {
        canvas.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(canvas.GetComponent<CanvasGroup>().alpha,0,speedShow*Time.deltaTime);
        if (canvas.GetComponent<CanvasGroup>().alpha < 0.1)
        {
          canvas.GetComponent<CanvasGroup>().alpha = 0;
          hide = false;
        }
      }
       
    }
    
    public void pushNotif(string str)
    {
      if (!(show||hide))
      {
        canvas.transform.GetChild(1).GetComponent<Text>().text = str;
        show = true;
      }
    }
}
