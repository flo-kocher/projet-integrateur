using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
public class ProfileFetch : MonoBehaviour
{
    public Text playerName;

    public PlayerStats name;

    void Start()
    {
        playerName = GameObject.Find("PlayerName").GetComponent<Text>();
        playerName = PlayerStats.playerName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*void PostData() => StartCoroutine(Upload());
    public IEnumerator Upload(){

        using(UnityWebRequest restAPI = UnityWebRequest.Get("http://185.155.93.105:11007/profile"))
        {
            restAPI.SetRequestHeader("content-type", "application/json");   
            restAPI.uploadHandler.contentType = "application/json";

            yield return restAPI.SendWebRequest();
            if(restAPI.result != UnityWebRequest.Result.Success){
                Debug.Log(restAPI.error);
                Debug.Log(restAPI.result);
            }else{
                Debug.Log("Get request succeeded !");
                Debug.Log(restAPI.result);
            }
        }
    }*/
}
