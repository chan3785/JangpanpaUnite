using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Rendering;
using UnityEngine;




    public class Ingame_Shield_Button : MonoBehaviour
    {
    private GameObject player;

        private bool is_Click;
        private float time;
        private float parrying_time = 0.5f;
        public bool can_Parrying;
        // Start is called before the first frame update
        void Start()
        {
        player = GameObject.Find("ZhangFei");
            
            
        }

        // Update is called once per frame
        void Update()
        {
            

            if (is_Click)
            {
                if (!Sword.Parrying||time > parrying_time)
            {
                //Debug.Log("shiled");
                //can_Parrying = false;
                    player.SendMessage("parrying_Off");
                    //time = 0;
                }
                else
                {
                //Debug.Log("Parrying");
                //can_Parrying = true;
                time += Time.deltaTime;
                player.SendMessage("parrying_On");
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
        //Debug.Log(1);
            is_Click = true;
            player.SendMessage("Guard");
        }

        public void button_Up()
        {
            is_Click = false;
            player.SendMessage("Guard_off");
        }
    }
