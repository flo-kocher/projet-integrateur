using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFirst : MonoBehaviour
{
    bool flip;
    public float speedFlip = 5;
    public float speedPose = 6;
    public float tempsAttente = 0.5f;
    private float timer=0;
    bool attente = false;
    bool end = false;
    // Start is called before the first frame update
    void Start()
    {
        flip = true;
        transform.position = new Vector3(Camera.main.transform.position.x + 2.525f, Camera.main.transform.position.y + 3.17f, Camera.main.transform.position.z+1.66f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!end)
          transform.position = new Vector3(Camera.main.transform.position.x + 2.525f, Camera.main.transform.position.y + 3.17f, Camera.main.transform.position.z+1.66f);
        if (flip) {
          Quaternion target = Quaternion.Euler(-42, 0, 0);
          transform.rotation =
              Quaternion.Slerp(transform.rotation, target, speedFlip * Time.deltaTime);
          float finish = Quaternion.Angle(transform.rotation, target);
          if (finish <= 0.01f) {
            transform.rotation = target;
            flip = false;
            attente = true;
          }
        }
        if (attente) {
          timer += Time.deltaTime;
          if (timer>tempsAttente) {
            attente = false;
            end = true;
          }
        }
        if (end) {
          Vector3 position =
            new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                      Camera.main.WorldToScreenPoint(transform.position).z);
          Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
          Vector3 target =
              new Vector3(worldPosition.x, worldPosition.y, -0.2f);
          transform.position =
              Vector3.Slerp(transform.position, target, speedPose * Time.deltaTime);
          Quaternion targett = Quaternion.Euler(0, 0, 0);
          transform.rotation =
              Quaternion.Slerp(transform.rotation, targett, speedPose * Time.deltaTime);
          float finish = Vector3.Angle(transform.position, target);
          if (finish <= 0.25f) {
            this.GetComponent<Move>().enabled = true;
            end = false;
            this.enabled = false;
          }
        }
    }
}
