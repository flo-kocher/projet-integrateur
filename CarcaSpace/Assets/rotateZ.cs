using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateZ : MonoBehaviour
{
    public float speed;
    public bool leve = false;
    public bool tourne = false;
    public bool couche = false;
    private float angle = 0.0f;
    private float angle_last;
 
    void Start()
    {

    }
    void Update()
    {
        
	if (Input.GetKeyDown("left"))
    {
        if(!(leve || couche || tourne))
            animate(1);
    }
	
	if (Input.GetKeyDown("right"))
    {
        if(!(leve || couche || tourne))
            animate(0);
    }

	if (leve)
	{
        transform.position = Vector3.Slerp(transform.position,new Vector3(transform.position.x, transform.position.y, -0.6f),speed*Time.deltaTime);
        Quaternion target = Quaternion.Euler(-42, 0, angle_last);
        float finish = Quaternion.Angle(transform.rotation, target);
        if (finish <= 0.01f)
        {
            transform.rotation = target;
            leve = false;
            tourne = true;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, target,speed*Time.deltaTime);
        
	}
 
    if (tourne)
    {
        Quaternion target = Quaternion.Euler(-42, 0, angle);
        float finish = Quaternion.Angle(transform.rotation, target);
        if (finish <= 0.01f) {
                tourne = false;
                couche = true;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, target,speed*Time.deltaTime);
       
    }
    
    if (couche)
    {
        transform.position = Vector3.Slerp(transform.position,new Vector3(transform.position.x, transform.position.y, -0.2f),speed*Time.deltaTime);
        Quaternion target = Quaternion.Euler(0, 0, angle);
        float finish = Quaternion.Angle(transform.rotation, target);
        if (finish <= 0.01f) {
            transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);
            couche = false;
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, target,speed*Time.deltaTime);
        
    }
    
    }
    
    void animate(int sens) {
    angle_last = angle;
	if (sens == 0)
		angle-= 90.0f;
	else
		angle+= 90.0f;
	leve = true;
    }
}
