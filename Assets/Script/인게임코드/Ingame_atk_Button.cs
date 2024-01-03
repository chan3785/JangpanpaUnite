using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;



public class Ingame_atk_Button : MonoBehaviour
{
    private GameObject player;

    private bool is_Click;
    private float time;
    private float min_time = 0.3f;

    private bool ActivatelongAtk;
    // Start is called before the first frame update




    void Start()
    {
        player = GameObject.Find("ZhangFei");
        ActivatelongAtk = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (is_Click)
        {
            time += Time.deltaTime;
            if (time > min_time && !ActivatelongAtk)
            {
                Debug.Log("activate: " + time);
                ActivatelongAtk = true;
                player.SendMessage("Activate_Attack2");
            }



        }
        else
        {
            time = 0;
            ActivatelongAtk = false;
        }
        //Debug.Log("al:" + ActivatelongAtk);
        //Debug.Log("l:"+longAtk);
    }

    public void button_Down()
    {
        is_Click = true;
    }

    public void button_Up()
    {
        is_Click = false;
        if (ActivatelongAtk && time > min_time)
        {
            //Debug.Log("atk2");
            player.SendMessage("Attack2");
        }
        else
        {
            //Debug.Log("atk1");
            player.SendMessage("Attack1");

        }
    }
}