using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ProfileMenu : MonoBehaviour
{   
    private Button profile;

    protected void EnterProfileMenu()
    {
        SceneManager.LoadScene("ProfileMenu", LoadSceneMode.Single);
    }
    void Start()
    {
        profile = GameObject.Find("Profile").GetComponent<Button>();
        profile.onClick.AddListener(EnterProfileMenu);
    }

    void Update()
    {
        
    }
}
