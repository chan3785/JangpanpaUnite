using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Ingame_atk_Button : MonoBehaviour
{
    private GameObject player;
    private GameObject play;

    private bool is_Click;
    private float time;
    private float min_time = 0.3f;

    private bool ActivatelongAtk;
    // Start is called before the first frame update




    void Start()
    {
        player = GameObject.Find("ZhangFei");
        play = GameObject.Find("ZhangFei (1)");
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
                //player.SendMessage("Activate_Attack2");
                play.SendMessage("activateAtk2");

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
        if (newPlayer.canBehave)
        {
            is_Click = true;
        }
    }

    public void button_Up()
    {
        if (newPlayer.canBehave)
        {
            is_Click = false;
            if (ActivatelongAtk && time > min_time)
            {
                
                //player.SendMessage("Attack2");
                play.SendMessage("Atk2");
            }
            else
            {
                
                //player.SendMessage("Attack1");
                play.SendMessage("Atk1");

            }
        }
    }
}