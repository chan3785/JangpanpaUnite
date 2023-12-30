using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup_Management : MonoBehaviour
{
    public GameObject yes;
    public GameObject no;
    public GameObject panel;
    public GameObject skill1;
    public GameObject skill2;
    public GameObject skill3;
    public GameObject skill4;
    public GameObject skill5;
    public GameObject skill6;
    public GameObject cur_skill;

    public void unlock() // 장수해금 팝업 관리
    {
        if (!yes.gameObject.activeSelf && no.gameObject.activeSelf) 
        {
            panel.gameObject.SetActive(true);
        }
    }

    public void skill() // 스킬설명 팝업 관리
    {
        skill1.gameObject.SetActive(false);
        skill2.gameObject.SetActive(false);
        skill3.gameObject.SetActive(false);
        skill4.gameObject.SetActive(false);
        skill5.gameObject.SetActive(false);
        skill6.gameObject.SetActive(false);
        cur_skill.gameObject.SetActive(true);
    }
}
