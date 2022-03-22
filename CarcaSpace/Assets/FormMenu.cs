using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FormMenu : MonoBehaviour
{
    Button enterSignInPage;
    public void SignIn()
    {
         SceneManager.LoadScene("signInMenu", LoadSceneMode.Single);
    }

    public void LoggedIn()
    {

    }

    void Start(){
        enterSignInPage = GameObject.Find("noAccount").GetComponent<Button>();
        enterSignInPage.onClick.AddListener(SignIn);
    }

}
