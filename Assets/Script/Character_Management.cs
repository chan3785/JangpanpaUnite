using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Management : MonoBehaviour
{
    public GameObject cur_charcter;
    public GameObject zhangFei;
    public GameObject caoRen;
    public GameObject caoHong;
    public GameObject zhangLiao;

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
        if (cur_charcter == caoRen)
        {
            Settings.caoRen = true;
        }
        if (cur_charcter == caoHong)
        {
            Settings.caoHong = true;
        }
        if (cur_charcter == zhangLiao)
        {
            Settings.zhangLiao = true;
        }
    }

    void Update() {
        Debug.Log(Settings.caoRen);    
    }
}
