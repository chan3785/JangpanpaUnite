using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Ingame_Shield_Button : MonoBehaviour
{
    private GameObject player;
    private GameObject play;

    private bool is_Click;
    private float time;
    private float parrying_time = 0.5f;
    public bool can_Parrying;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ZhangFei");
        play = GameObject.Find("ZhangFei (1)");


    }

    // Update is called once per frame
    void Update()
    {


        if (is_Click)
        {
            if ( time > parrying_time)
            {
                //Debug.Log("shiled");
                //can_Parrying = false;
                //player.SendMessage("parrying_Off");
                //time = 0;
                play.SendMessage("parryOff");
            }
            else
            {
                //Debug.Log("Parrying");
                //can_Parrying = true;
                time += Time.deltaTime;
                //player.SendMessage("parrying_On");
                play.SendMessage("parryOn");
            }

        }
        else
        {
            time = 0;
            can_Parrying = false;
        }

        //Debug.Log(can_Parrying);
    }

    public void button_Down()
    {
        if (newPlayer.canBehave) {
            //Debug.Log(1);
            is_Click = true;
            //player.SendMessage("Guard");
            play.SendMessage("guard");
        }
    }

    public void button_Up()
    {
        if (newPlayer.canBehave)
        {
            is_Click = false;
            //player.SendMessage("Guard_off");
            play.SendMessage("guardOff");
        }
    }
}