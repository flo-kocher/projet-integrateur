using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
    /*Ce script sert a lier le main menu 
    a la scene sign in */
public class CarcaspaceButton : MonoBehaviour
{
    public Button carcaBouton ; 
    

    void Start()
    {
        carcaBouton.onClick.AddListener(NextScene); 
    }
    public void NextScene()
    {
        SceneManager.LoadScene("GameMenu");
    }
}
