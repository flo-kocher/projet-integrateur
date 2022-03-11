using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private GameObject go;
    private bool dragging = false;
    public float moveSpeed;
    public float speed;
    private bool anim1 = false;
    private bool anim2 = false;
    private GameObject disapear;
    private rotateZ r;

    public Board plateau;

    // Start is called before the first frame update
    void Start()
    {
        plateau = this.transform.parent.GetComponent<Board>();
    }

    // Update is called once per frame
    void Update()
    {
        r = this.GetComponent<rotateZ>();
        float x = transform.position.x-(transform.position.x%1);
        float y = transform.position.y-(transform.position.y%1);
        
        if(Input.GetMouseButtonDown(0) && !(anim1||anim2) && !(r.leve || r.couche || r.tourne))
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;

            if( Physics.Raycast( ray, out hit, 100 ) )
            {
                go = hit.transform.gameObject;
		if (go == this.gameObject)
		{
        	    dragging = !dragging;
                    if (!dragging)
                    {
                        disapear = GameObject.Find((int)x + "/" + (int)y);
                        anim2 = true;
                        this.GetComponent<rotateZ>().enabled = false;
                        this.GetComponent<Constraints>().enabled = false;
        	    }
                    else
                    {
                        anim1 = true;
                    }
		}
            }
        }
        if (dragging)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }
        if (anim1) {
	    if (disapear != null)
	    {
                disapear.SetActive(true);
	        Material mat = disapear.GetComponent<Renderer>().material;
	        Color c_target = mat.color;
	        c_target.a = 1;
	        mat.color = Color.Lerp(mat.color, c_target,speed*Time.deltaTime);
	    }
            Vector3 target = new Vector3(transform.position.x, transform.position.y, -0.2f);
            transform.position = Vector3.Slerp(transform.position,target,speed*Time.deltaTime);
            float finish = Vector3.Angle(transform.position, target);
            if (finish <= 0.01f)
	          {
                this.GetComponent<Constraints>().enabled = true;
                this.GetComponent<rotateZ>().enabled = true;
                anim1 = false;
            }
        }
        if (anim2) {
	    if (disapear != null)
	    {
	        Material mat = disapear.GetComponent<Renderer>().material;
	        Color c_target = mat.color;
	        c_target.a = 0;
	        mat.color = Color.Lerp(mat.color, c_target,speed*Time.deltaTime);
	    }
            Vector3 target = new Vector3(x+0.5f,y+0.5f, 0f);
            transform.position = Vector3.Slerp(transform.position,target,speed*Time.deltaTime);
            float finish = Vector3.Angle(transform.position, target);
            if (finish <= 0.01f)
	    {
                anim2 = false;
            	if (disapear != null)
                {
                    plateau.board.Add(disapear);
                    disapear.SetActive(false);
                }
	    }
        }
    }
}
