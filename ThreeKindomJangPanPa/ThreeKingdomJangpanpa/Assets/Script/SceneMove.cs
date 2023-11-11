using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public void Main_Scene()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Game_Scene()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Setting_Scene()
    {
        SceneManager.LoadScene("SettingScene");
    }
    public void Start_Scene()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void Training_Scene()
    {
        SceneManager.LoadScene("TrainingScene");
    }
    public void Tutorial_Scene()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}