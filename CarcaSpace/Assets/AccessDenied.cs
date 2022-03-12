using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class AccessDenied : MonoBehaviour
{
    GameObject face;
    List<Material> m;
    bool animDenied = false;
    bool endAnimDenied = false;
    bool brotatz = false;
    Material Cross;
    Material Red;
    Material logo;
    public float speed;
    public float speedShake;
    public float wait;
    float timer = 0;
    // Start is called before the first frame update
    

    void Start()
    {
      Cross = Resources.Load<Material>("Material/Cross");
      Red = Resources.Load<Material>("Material/Red");
      logo = Resources.Load<Material>("Material/logo");
      face = gameObject.transform.GetChild(2).gameObject;
      m = face.GetComponent<Renderer>().materials.ToList();
    }

    // Update is called once per frame
    void Update()
    {
      if (animDenied)
      {
        error();
      }

      if (brotatz)
      {
        rotatz();
      }
      
      if(endAnimDenied)
      {
        timer += Time.deltaTime;
        if (timer > wait)
        {
          brotatz = false;
          rotateZ r = GetComponent<rotateZ>();
          transform.rotation = Quaternion.Euler(0,0,r.angle);
          init();
        }
      }

      if (Input.GetKey("m") && !(animDenied || endAnimDenied))
      {
        animDenied = true;
      } 

    }

    void error()
    {
	    Color c_target = m[1].color;
	    c_target.a = 0;
	    m[1].color = Color.Lerp(m[1].color, c_target,speed*Time.deltaTime);
      face.GetComponent<Renderer>().materials = m.ToArray();
	    if (m[1].color.a <= 0.01f)
	    {
        endAnimDenied = true;
        brotatz = true;
	      animDenied = false;
	    }
    }

    void rotatz()
    {
      rotateZ r = GetComponent<rotateZ>();
      Quaternion left = Quaternion.Euler(0,0,r.angle+20);
      Quaternion right = Quaternion.Euler(0,0,r.angle-20);
      float lerp = Mathf.PingPong(speedShake*timer,1);
      transform.rotation = Quaternion.Slerp(left,right,lerp);
    }


      void init()
      {
        Color c_target = m[1].color;
	      c_target.a = 1;
	      m[1].color = Color.Lerp(m[1].color, c_target,speed*Time.deltaTime);
        face.GetComponent<Renderer>().materials = m.ToArray();
	      if (m[1].color.a >= 0.99f)
	      {
          timer = 0;
          endAnimDenied = false;
        }
      }

      public void animRefuse()
      {
        animDenied = true;
      }

      public bool testRefuse()
      {
        return !(animDenied || endAnimDenied);
      }
}
