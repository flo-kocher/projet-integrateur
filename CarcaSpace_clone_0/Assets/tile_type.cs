using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class tile_type : MonoBehaviour
{

    public int north = 0;
    public int east = 1;
    public int south = 2;
    public int west = 3;
    public int middle = 4;
    public bool blazon = false;
    public static int nbrTuile = 30;
    public static bool finish = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract int getNbrTuile();

    public abstract void decrementNbrTuile();

    public abstract bool getFinish();

    public abstract void changeFinish();

}
