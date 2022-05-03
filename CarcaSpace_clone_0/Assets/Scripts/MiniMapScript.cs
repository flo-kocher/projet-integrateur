using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MiniMapScript : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int grid = GameObject.Find("Grid").GetComponent<CreateGrid>().nbTuiles;
        transform.position = new Vector3(grid/2,grid/2,-grid+0.5f);
    }

  // Update is called once per frame
  void Update() {}
}
