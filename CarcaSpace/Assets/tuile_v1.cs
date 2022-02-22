using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuile : MonoBehaviour
{
    private int north = 1;
    private int east = 2;
    private int south = 3;
    private int west = 4;
    // public int middle = "road";
    public int north2;
    public int east2;
    public int south2;
    public int west2;

    public int rot = 0;
    public int right;
    public int left;


    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("north: " + north);
        // Debug.Log("south: " + south);
        // Debug.Log("east: " + east);
        // Debug.Log("west: " + west);
        // Debug.Log("middle: " + middle);
    }
 /*
    void rotation()
    {
        right = Input.GetKeyDown(KeyCode.RightArrow) + 1;
        left = Input.GetKeyDown(KeyCode.LeftArrow) - 1;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rot += right % 4;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rot += left % (-4);
        }
    }
*/
    void moveToRight()
    {
        north2 = north + 1 % 4;
        east2 = east + 1 % 4;
        south2 = south + 1 % 4;
        west2 = west + 1 % 4;
    }

    void moveToLeft()
    {
        north2 = north - 1 % 4;
        east2 = east - 1 % 4;
        south2 = south - 1 % 4;
        west2 = west - 1 % 4;
    }

    // Update is called once per frame
    void Update()
    {
        // si rotation, alors mettre à jour les coordonnées
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveToRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveToLeft();
        }

        // Debug.Log("north: " + north);
        // Debug.Log("east: " + east);
        // Debug.Log("west: " + west);
        // Debug.Log("south: " + south);
        // Debug.Log("middle: " + middle);

        // si impossibilité de poser la tuile, alors affichage d'un message d'erreur
        // if (pose() == false)
        // {
        //     Debug.Log("Impossible de poser cette tuile ici.");
        // }
    }
}
