using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;

public class Board : MonoBehaviour
{
    Move plateau;

    //List<GameObject> board = new List<GameObject>();
    // Start is called before the first frame update

    //ajouter le tag "Plateau" a tout les elements de la grid qui deviennent invisibles
    //il faudra creer le tag avant dans project settings

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] onlyInactive = GameObject.FindObjectsOfType<GameObject>(true).Where(sr => !sr.gameObject.activeInHierarchy).ToArray();
        for (var i = 0; i < onlyInactive.Length; i++)
        {
            if(onlyInactive[i].name.Contains("/"))
            {
                //board.Add(onlyInactive[i].name.Contains("/"));
                //Debug.Log("hidden objects : " + onlyInactive[i]);
                //Debug.Log("nbr elements : " + onlyInactive.Length);
            }
        }

        plateau = this.GetComponent<Move>();

        // regler le soucis de ca ...
        if(plateau != null)
        {
            Debug.Log("Plateau : " + plateau.board[0]);
            Debug.Log("Plateau : " + plateau.board.Count);
        }
    }
}
