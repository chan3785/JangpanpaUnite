using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Setting : MonoBehaviour
{
    public static bool isBGM, isSound, isVib, isTutorial, button_sound, button_vib = true;
    public static bool isStory = true;
    [SerializeField] Toggle bgmTog, soundTog, storyTog, vibTog, tutTog;
    [SerializeField] AudioSource bgm, sound;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        setAudioToggle(bgmTog, bgm, isBGM);
        setAudioToggle(soundTog, sound, isSound);
        setToggle(vibTog, isVib);
        setToggle(storyTog, isStory);
    }

    // 게임 상태를 저장하는 함수
    public void SaveGame()
    {
        // 여기에 저장할 정보를 넣으면 됩니다.
        /*
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);

        PlayerPrefs.Save();

        Debug.Log("게임 상태가 저장되었습니다.");
        */
    }
    public void setAudioToggle(Toggle toggle, AudioSource audioSource, bool isActive)
    {
        if (toggle.isOn)
        {
            button_sound = true;
            audioSource.mute = false;
            isActive = true;
        }
        else
        {
            button_sound = false;
            audioSource.mute = true;
            isActive = false;
        }

    }
    public void setToggle(Toggle toggle, bool isActive)
    {
        if (toggle.isOn)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }

    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.IO;

//public class Setting : MonoBehaviour
//{
//    public static bool isStory = true;
//    public bool isBGM, isSound, isVib, isTutorial;
//    [SerializeField] Toggle bgmTog, soundTog, storyTog, vibTog, tutTog;
//    [SerializeField] AudioSource bgm, sound;


//    // Start is called before the first frame update
//    void Start()
//    {
//        // Settings.health = PlayerPrefs.GetInt("Health", 0);
//        // Settings.power = PlayerPrefs.GetInt("Power", 0);
//        // Settings.speed = PlayerPrefs.GetInt("Speed", 0);
//        // Settings.reflex = PlayerPrefs.GetInt("Reflex", 0);
//        // Settings.defence = PlayerPrefs.GetInt("Defence", 0);
//        // Settings.invincibility = PlayerPrefs.GetInt("Invincibility", 0);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        setAudioToggle(bgmTog, bgm, isBGM);
//        setAudioToggle(soundTog, sound, isSound);
//        setToggle(vibTog, ref isVib);
//        setToggle(storyTog, ref isStory);

//        Debug.Log(isStory);
//    }

//    // 게임 상태를 저장하는 함수
//    public void SaveGame()
//    {
//        PlayerPrefs.SetInt("Health", Settings.health);
//        PlayerPrefs.SetInt("Power", Settings.power);
//        PlayerPrefs.SetInt("Speed", Settings.speed);
//        PlayerPrefs.SetInt("Reflex", Settings.reflex);
//        PlayerPrefs.SetInt("Defence", Settings.defence);
//        PlayerPrefs.SetInt("Invincibility", Settings.invincibility);

//        PlayerPrefs.Save();

//        Debug.Log("게임 상태가 저장되었습니다.");
//    }

//    public void setAudioToggle(Toggle toggle, AudioSource audioSource, bool isActive)
//    {
//        if (toggle.isOn)
//        {
//            audioSource.mute = false;
//            isActive = true;
//        }
//        else
//        {
//            audioSource.mute = true;
//            isActive = false;
//        }

//    }
//    public void setToggle(Toggle toggle, ref bool isActive)
//    {
//        if (toggle.isOn)
//        {
//            isActive = true;
//        }
//        else
//        {
//            isActive = false;
//        }

//    }
//}