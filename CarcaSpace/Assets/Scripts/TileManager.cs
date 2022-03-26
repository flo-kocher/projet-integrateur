using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;


public class TileManager: NetworkBehaviour{

    private List<GameObject> all_tiles = new List<GameObject>();


    public List<GameObject>  instatiateTiles(){
        GameObject go = GameObject.Find("Temp");
        GameObject temp = Instantiate(go) ; 
        int x = 0 ;
        for(int i =1 ;i< 25; i++){
            switch(i){
                case 1 : 
                   temp.AddComponent<tile_type_1>();
                   all_tiles.Add(temp);
                   break;
                case 2 : 
                   temp.AddComponent<tile_type_2>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 3 : 
                   temp.AddComponent<tile_type_3>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 4 : 
                   temp.AddComponent<tile_type_4>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 5 : 
                   temp.AddComponent<tile_type_5>();
                   all_tiles.Add(temp);
                   break;
                case 6 : 
                   temp.AddComponent<tile_type_6>();
                   all_tiles.Add(temp);
                   break;
                case 7 : 
                   temp.AddComponent<tile_type_7>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 8 : 
                   temp.AddComponent<tile_type_8>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 9 : 
                   temp.AddComponent<tile_type_9>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 10 : 
                   temp.AddComponent<tile_type_10>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 11 : 
                   temp.AddComponent<tile_type_11>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 12 : 
                   temp.AddComponent<tile_type_12>();
                   all_tiles.Add(temp);
                   all_tiles.Add(temp);
                   break;
                case 13 : 
                   temp.AddComponent<tile_type_13>();
                   all_tiles.Add(temp); 
                   break;                       
                case 14 :
                    for(x=0;x<1;x++){
                        temp.AddComponent<tile_type_14>();
                        all_tiles.Add(temp);
                    }
                    break;
                case 15 :
                    for(x=0;x<2;x++){
                        temp.AddComponent<tile_type_15>();
                        all_tiles.Add(temp);
                    }
                    break;
                case 16 :
                    for(x=0;x<4;x++){
                        temp.AddComponent<tile_type_16>();
                        all_tiles.Add(temp);
                    }
                    break;
                case 17 :
                    for(x=0;x<2;x++){
                        temp.AddComponent<tile_type_17>();
                        all_tiles.Add(temp);
                    }
                    break;
                case 18 :
                    for(x=0;x<2;x++){
                        temp.AddComponent<tile_type_18>();
                        all_tiles.Add(temp);
                    } 
                    break;
                case 19 :
                    for(x=0;x<2;x++){
                        temp.AddComponent<tile_type_19>();
                        all_tiles.Add(temp);
                    } 
                    break;
                //a revoir
                case 20 :
                    for(x=0;x<3;x++){
                        temp.AddComponent<tile_type_20>();
                        all_tiles.Add(temp);
                    } 
                    break;
                case 21 :
                    temp.AddComponent<tile_type_21>();
                    all_tiles.Add(temp);
                    break;
                case 22 :
                    for(x=0;x<3;x++){
                        temp.AddComponent<tile_type_22>();
                        all_tiles.Add(temp);
                    }  
                    break;
                case 23:
                  for(x=0;x<7;x++){
                        temp.AddComponent<tile_type_23>();
                        all_tiles.Add(temp);
                    }  
                    break;
                case 24 :
                    for(x=0;x<8;x++){
                        temp.AddComponent<tile_type_24>();
                        all_tiles.Add(temp);
                    }
                    break;
            }
                
        }
        return all_tiles;
    }

}
