using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class AccessDenied : MonoBehaviour
{
    GameObject face;
    List<Material> m;
    public bool animDenied = false;
    public bool endAnimDenied = false;
    Material Cross;
    Material Red;
    Material logo;
    public float speed;
    public int wait;
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
      
      if(endAnimDenied)
      {
        init();
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
	      animDenied = false;
	    }
    }

      void init()
      {
        Color c_target = m[1].color;
	      c_target.a = 1;
	      m[1].color = Color.Lerp(m[1].color, c_target,speed*Time.deltaTime);
        face.GetComponent<Renderer>().materials = m.ToArray();
	      if (m[1].color.a >= 0.99999f)
	      {
          endAnimDenied = false;
        }
      } 
}
