using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameoverPopup : MonoBehaviour
{

    public void retryButton()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void backButton()
    {
        
        SceneManager.LoadScene("StartScene");
    }

}
