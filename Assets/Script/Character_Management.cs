using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Management : MonoBehaviour
{
    public GameObject yes; // �⺻������ Popup_Management�� yes�� ���� �뵵
    public GameObject cur_charcter;
    public GameObject zhangFei;
    public GameObject caoRen;
    public GameObject caoHong;
    public GameObject zhangLiao;
    AudioSource sound;

    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    public void characters()
    {
        zhangFei.gameObject.SetActive(false);
        caoRen.gameObject.SetActive(false);
        caoHong.gameObject.SetActive(false);
        zhangLiao.gameObject.SetActive(false);
        cur_charcter.gameObject.SetActive(true);
        if (!Settings.caoRen)
        {
            caoRen.gameObject.SetActive(false);
        }
        if (!Settings.caoHong)
        {
            caoHong.gameObject.SetActive(false);
        }
        if (!Settings.zhangLiao)
        {
            zhangLiao.gameObject.SetActive(false);
        }
    }

    public void select()
    {
        if (cur_charcter == zhangFei && yes.gameObject.activeSelf)
        {
            Settings.zhangFei = true;
            Settings.caoRen = false;
            Settings.caoHong = false;
            Settings.zhangLiao = false;
        }
        if (cur_charcter == caoRen && yes.gameObject.activeSelf)
        {
            Settings.zhangFei = false;
            Settings.caoRen = true;
            Settings.caoHong = false;
            Settings.zhangLiao = false;
        }
        if (cur_charcter == caoHong && yes.gameObject.activeSelf)
        {
            Settings.zhangFei = false;
            Settings.caoRen = false;
            Settings.caoHong = true;
            Settings.zhangLiao = false;
        }
        if (cur_charcter == zhangLiao && yes.gameObject.activeSelf)
        {
            Settings.zhangFei = false;
            Settings.caoRen = false;
            Settings.caoHong = false;
            Settings.zhangLiao = true;
        }
    }

    public void playSound()
    {
        sound.Play();
        Debug.Log("ȿ����");
    }

    void Update()
    {
        if (Settings.zhangFei)
        {
            zhangFei.gameObject.SetActive(true);
            Debug.Log("���");
        }
        if (Settings.caoRen)
        {
            caoRen.gameObject.SetActive(true);
            Debug.Log("����");
        }
        if (Settings.caoHong)
        {
            caoHong.gameObject.SetActive(true);
            Debug.Log("��ȫ");
        }
        if (Settings.zhangLiao)
        {
            zhangLiao.gameObject.SetActive(true);
            Debug.Log("���");
        }
    }
}