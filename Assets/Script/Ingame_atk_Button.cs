using System.Collections;
using System.Collections.Generic;
//using UnityEditorInternal;
using UnityEngine;



public class Ingame_atk_Button : MonoBehaviour
{
    private GameObject player;

    private bool is_Click;
    private float time;
    private float min_time = 0.5f;

    Animator CaoRen;

    // Start is called before the first frame update
    void Start()
    {
        if(Settings.zhangFei)
        {
            player = GameObject.Find("ZhangFei");
        }
        else if(Settings.caoRen)
        {
            player = GameObject.Find("CaoRen");
            CaoRen = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(is_Click)
        {
            time += Time.deltaTime;
        }
        else
        {
            time = 0;
        }
    }

    public void button_Down()
    {
        is_Click = true;
    }

    public void button_Up()
    {
        is_Click = false;
        if(time > min_time)
        {
            //Debug.Log("atk2");
            player.SendMessage("Attack2");
            player.SendMessage("Atk2");
        }
        else
        {
            //Debug.Log("atk1");
            player.SendMessage("Attack1");
            player.SendMessage("Atk1");
        }
    }
}
