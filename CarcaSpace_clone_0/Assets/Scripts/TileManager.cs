using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using System;


public class TileManager: NetworkBehaviour{

    //private List<GameObject> all_tiles = new List<GameObject>();


    // public List<GameObject>  instatiateTiles(){
    //     GameObject go = GameObject.Find("go");
    //     int x = 0 ;
    //     for(int i =1 ;i< 25; i++){
    //         switch(i){
    //             case 1 : 
    //                go.AddComponent<tile_type_1>();
    //                all_tiles.Add(go);
    //                break;
    //             case 2 : 
    //                go.AddComponent<tile_type_2>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 3 : 
    //                go.AddComponent<tile_type_3>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 4 : 
    //                go.AddComponent<tile_type_4>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 5 : 
    //                go.AddComponent<tile_type_5>();
    //                all_tiles.Add(go);
    //                break;
    //             case 6 : 
    //                go.AddComponent<tile_type_6>();
    //                all_tiles.Add(go);
    //                break;
    //             case 7 : 
    //                go.AddComponent<tile_type_7>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 8 : 
    //                go.AddComponent<tile_type_8>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 9 : 
    //                go.AddComponent<tile_type_9>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 10 : 
    //                go.AddComponent<tile_type_10>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 11 : 
    //                go.AddComponent<tile_type_11>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 12 : 
    //                go.AddComponent<tile_type_12>();
    //                all_tiles.Add(go);
    //                all_tiles.Add(go);
    //                break;
    //             case 13 : 
    //                go.AddComponent<tile_type_13>();
    //                all_tiles.Add(go); 
    //                break;                       
    //             case 14 :
    //                 for(x=0;x<1;x++){
    //                     go.AddComponent<tile_type_14>();
    //                     all_tiles.Add(go);
    //                 }
    //                 break;
    //             case 15 :
    //                 for(x=0;x<2;x++){
    //                     go.AddComponent<tile_type_15>();
    //                     all_tiles.Add(go);
    //                 }
    //                 break;
    //             case 16 :
    //                 for(x=0;x<4;x++){
    //                     go.AddComponent<tile_type_16>();
    //                     all_tiles.Add(go);
    //                 }
    //                 break;
    //             case 17 :
    //                 for(x=0;x<2;x++){
    //                     go.AddComponent<tile_type_17>();
    //                     all_tiles.Add(go);
    //                 }
    //                 break;
    //             case 18 :
    //                 for(x=0;x<2;x++){
    //                     go.AddComponent<tile_type_18>();
    //                     all_tiles.Add(go);
    //                 } 
    //                 break;
    //             case 19 :
    //                 for(x=0;x<2;x++){
    //                     go.AddComponent<tile_type_19>();
    //                     all_tiles.Add(go);
    //                 } 
    //                 break;
    //             //a revoir
    //             case 20 :
    //                 for(x=0;x<3;x++){
    //                     go.AddComponent<tile_type_20>();
    //                     all_tiles.Add(go);
    //                 } 
    //                 break;
    //             case 21 :
    //                 go.AddComponent<tile_type_21>();
    //                 all_tiles.Add(go);
    //                 break;
    //             case 22 :
    //                 for(x=0;x<3;x++){
    //                     go.AddComponent<tile_type_22>();
    //                     all_tiles.Add(go);
    //                 }  
    //                 break;
    //             case 23:
    //               for(x=0;x<7;x++){
    //                     go.AddComponent<tile_type_23>();
    //                     all_tiles.Add(go);
    //                 }  
    //                 break;
    //             case 24 :
    //                 for(x=0;x<8;x++){
    //                     go.AddComponent<tile_type_24>();
    //                     all_tiles.Add(go);
    //                 }
    //                 break;
    //         }
                
    //     }
    //     return all_tiles;
    // }

}
