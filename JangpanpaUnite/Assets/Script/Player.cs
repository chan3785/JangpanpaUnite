using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




    public class Player : MonoBehaviour
    {


        private int hp, atk, atkSpeed, specialSkillGage, subHp;

        public static bool is_atk,atk_type;

        private bool can_Parrying,is_guard;

        Animator anim;
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }

        public int Atk
        {
            get { return atk; }
            set { atk = value; }
        }

        public int SubHp
        {
            get { return subHp; }
            set { subHp = value; }
        }

        public int Special
        {
            get { return specialSkillGage; }
            set { specialSkillGage = value; }
        }


        private void Awake()
        {


            anim = GetComponent<Animator>();
        }


        public void OnTriggerEnter2D(Collider2D collision)
        {
            // Debug.Log("1");
            if (collision.CompareTag("Enemy_atk"))
            {
                Hurt();
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            is_atk = false;

            
            can_Parrying = false;is_guard = false;

        Hp = 100;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Attack1()
        {
            anim.SetBool("Atk1", true);
            is_atk = true;
            atk_type = false;
            Invoke("ani_init", 0.5f);
            //Debug.Log(Atk);
        }

        private void Attack2()
        {
            anim.SetBool("Atk2", true);
            is_atk = true;
            atk_type = true;
            Invoke("ani_init", 0.3f);

        }

        private void Kill()
        {

        }

    private void Hurt()
    {
        if (Enemy.is_atk)
        {
            if (is_guard)
            {
                if (can_Parrying)
                {
                    Debug.Log("parrying");
                }
                else
                {
                    Debug.Log("guard");
                }
            }
            


        else
            {
                hp -= 10;
                //Debug.Log("Player Hurt: "+hp);
                if(hp <= 0)
                {
                    Died();
                    
                }

            }
            Enemy.is_atk = false;
        }
     }
    private void Died()
    {
        Debug.Log("died");
    }

    private void Guard()
        {
            anim.SetBool("Shield", true);
            is_guard = true;
        }

        private void Guard_off()
        {
            anim.SetBool("Shield", false);
            is_guard = false;
        }
        private void parrying_On()
        {
            if (!can_Parrying)
            {
                can_Parrying = true;
                //Debug.Log("now_Parrying");
            }
        }
        private void parrying_Off()
        {
            if (can_Parrying)
            {
                can_Parrying = false;
                //Debug.Log("now_guard");
            }
        }


        private void Parrying()
        {

        }

    
    private void ani_init()
        {
            anim.SetBool("Atk2", false);
            anim.SetBool("Atk1", false);
            
            is_atk = false;
        }
    }
