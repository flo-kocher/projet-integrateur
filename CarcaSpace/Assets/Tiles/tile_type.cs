using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror ;

public abstract class tile_type : NetworkBehaviour
{

    public int north = 0;
    public int east = 1;
    public int south = 2;
    public int west = 3;
    public int middle = 4;
    public bool blazon = false;
    public static int nbrTypeTile = 24;

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
