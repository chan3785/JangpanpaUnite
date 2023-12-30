using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class upgradeSkills : MonoBehaviour
{
    int count;
    public Sprite after_img;
    public Sprite before_img;

    public Image dot1;
    public Image dot2;
    public Image dot3;
    public Image dot4;
    public Image dot5;
    public Image dot6;

    void Start()
    {
        count = 0;
    }

    public void dots()
    {
        if (count <= 6)
        {
            count++;
        }
        Debug.Log(count);
        switch (count)
        {
            case 1:
                changeColor(dot1);
                break;
            case 2:
                changeColor(dot2);
                break;
            case 3:
                changeColor(dot3);
                break;
            case 4:
                changeColor(dot4);
                break;
            case 5:
                changeColor(dot5);
                break;
            case 6:
                changeColor(dot6);
                break;
        }
    }

    void changeColor(Image dot)
    {
        if (dot.GetComponent<Image>().sprite != after_img)
        {
            dot.GetComponent<Image>().sprite = after_img;
        }
    }
    public void Initialize()
    {
        Settings.health = 0;
        Settings.power = 0;
        Settings.speed = 0;
        Settings.reflex = 0;
        Settings.defence = 0;
        Settings.invincibility = 0;

        dot1.GetComponent<Image>().sprite = before_img;
        dot2.GetComponent<Image>().sprite = before_img;
        dot3.GetComponent<Image>().sprite = before_img;
        dot4.GetComponent<Image>().sprite = before_img;
        dot5.GetComponent<Image>().sprite = before_img;
        dot6.GetComponent<Image>().sprite = before_img;


        Debug.Log("Initialized");
    }

    public void health()
    { // 체력
        if (Settings.health <= 6)
        {
            Settings.health++;
        }
        Debug.Log("Health");
        Debug.Log(Settings.health);
    }

    public void power()
    { // 공격력
        if (Settings.power <= 6)
        {
            Settings.power++;
        }
        Debug.Log("Power");
        Debug.Log(Settings.power);
    }

    public void speed()
    { // 공격속도
        if (Settings.speed <= 6)
        {
            Settings.speed++;
        }
        Debug.Log("Speed");
        Debug.Log(Settings.speed);
    }

    public void reflex()
    { // 순발력
        if (Settings.reflex <= 6)
        {
            Settings.reflex++;
        }
        Debug.Log("Reflex");
        Debug.Log(Settings.reflex);
    }

    public void defence()
    { // 방어력
        if (Settings.defence <= 6)
        {
            Settings.defence++;
        }
        Debug.Log("Defence");
        Debug.Log(Settings.defence);
    }

    public void invincibility()
    { // 만인지적
        if (Settings.invincibility <= 6)
        {
            Settings.invincibility++;
        }
        Debug.Log("Invincibility");
        Debug.Log(Settings.invincibility);
    }
}
