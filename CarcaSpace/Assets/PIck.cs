using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PIck : MonoBehaviour
{
    private static int compteur = 0;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(func);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void func()
    {
        GameObject temp = null;
        var list = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject i in list)
        {
            if (i.name == "Temp")
                temp = i;
            if (i.name.Contains("Pioche"))
                i.GetComponent<Move>().enabled = false;
        }
        GameObject clone = GameObject.Instantiate(temp);
        clone.SetActive(true);
        clone.transform.SetParent(GameObject.Find("Tiles").transform);
        compteur++;
        clone.name = "Pioche" + compteur;
        
        int loop = 1;
        int num = 0;
        while (loop == 1)
        {
            System.Random rnd = new System.Random();
            num = rnd.Next(25);
            Debug.Log("num random => "+num);
            int nbr=0;
            switch (num)
            {
                case 1:
                    nbr = gameObject.GetComponent<tile_type_1>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_1>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_1>();
                        loop = 0;
                    }
                    break;

                case 2:
                    nbr = gameObject.GetComponent<tile_type_2>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_2>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_2>();
                        loop = 0;
                    }
                    break;

                case 3:
                    nbr = gameObject.GetComponent<tile_type_3>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_3>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_3>();
                        loop = 0;
                    }
                    break;

                case 4:
                    nbr = gameObject.GetComponent<tile_type_4>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_4>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_4>();
                        loop = 0;
                    }
                    break;

                case 5:
                    nbr = gameObject.GetComponent<tile_type_5>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_5>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_5>();
                        loop = 0;
                    }
                    break;

                case 6:
                    nbr = gameObject.GetComponent<tile_type_6>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_6>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_6>();
                        loop = 0;
                    }
                    break;

                case 7:
                    nbr = gameObject.GetComponent<tile_type_7>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_7>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_7>();
                        loop = 0;
                    }
                    break;

                case 8:
                    nbr = gameObject.GetComponent<tile_type_8>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_8>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_8>();
                        loop = 0;
                    }
                    break;

                case 9:
                    nbr = gameObject.GetComponent<tile_type_9>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_9>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_9>();
                        loop = 0;
                    }
                    break;

                case 10:
                    nbr = gameObject.GetComponent<tile_type_10>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_10>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_10>();
                        loop = 0;
                    }
                    break;

                case 11:
                    nbr = gameObject.GetComponent<tile_type_11>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_11>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_11>();
                        loop = 0;
                    }
                    break;

                case 12:
                    nbr = gameObject.GetComponent<tile_type_12>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_12>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_12>();
                        loop = 0;
                    }
                    break;

                case 13:
                    nbr = gameObject.GetComponent<tile_type_13>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_13>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_13>();
                        loop = 0;
                    }
                    break;

                case 14:
                    nbr = gameObject.GetComponent<tile_type_14>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_14>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_14>();
                        loop = 0;
                    }
                    break;

                case 15:
                    nbr = gameObject.GetComponent<tile_type_15>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_15>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_15>();
                        loop = 0;
                    }
                    break;

                case 16:
                    nbr = gameObject.GetComponent<tile_type_16>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_16>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_16>();
                        loop = 0;
                    }
                    break;

                case 17:
                    nbr = gameObject.GetComponent<tile_type_17>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_17>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_17>();
                        loop = 0;
                    }
                    break;

                case 18:
                    nbr = gameObject.GetComponent<tile_type_18>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_18>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_18>();
                        loop = 0;
                    }
                    break;

                case 19:
                    nbr = gameObject.GetComponent<tile_type_19>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_19>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_19>();
                        loop = 0;
                    }
                    break;

                case 20:
                    nbr = gameObject.GetComponent<tile_type_20>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_20>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_20>();
                        loop = 0;
                    }
                    break;

                case 21:
                    nbr = gameObject.GetComponent<tile_type_21>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_21>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_21>();
                        loop = 0;
                    }
                    break;

                case 22:
                    nbr = gameObject.GetComponent<tile_type_22>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_22>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_22>();
                        loop = 0;
                    }
                    break;

                case 23:
                    nbr = gameObject.GetComponent<tile_type_23>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_23>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_23>();
                        loop = 0;
                    }
                    break;

                case 24:
                    nbr = gameObject.GetComponent<tile_type_24>().nbrTuile;
                    if(nbr > 0){
                        nbr--;
                        gameObject.GetComponent<tile_type_24>().nbrTuile = nbr;
                        clone.AddComponent<tile_type_24>();
                        loop = 0;
                    }
                    break;

                default:
                    
                    break;

            }
            
        }

        


        
        
        //clone.AddComponent("tile_type_2"()
    }
}
