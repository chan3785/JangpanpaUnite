using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class Enemy : MonoBehaviour
    {

        Player player=new Player();

        private int hp, atk, subHp;
        private float atkSpeed = 1, timer;
        public int walkSpeed;

        private bool inaction;

        Rigidbody2D rigid;
        Animator ani;

    public static bool is_atk;

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

        private void walk()
        {
            transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
        }

        private void hurt()
        {
            if (Player.atk_type)
            {
                hp -= player.Atk*2;
            }
            else
            {
                hp -= player.Atk;
            }
            if (hp <= 0)
            {
                dead();
            }
            

            Debug.Log("enemy hurt: " + hp);

        }

        private void dead()
        {
            Debug.Log("Died");
        }

        private void Attack()
        {



            ani.SetBool("Atk", true);
        


        }
      

        public void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.CompareTag("Player"))
            {
                if (Player.is_atk)
                {
                    hurt();
                    Player.is_atk = false;
                }
            }
        }



        // Start is called before the first frame update
        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            ani = GetComponent<Animator>();



            timer = 0;

            hp = 100;
            player.Atk = 5;
            //Debug.Log(player.Atk);

        }

        // Update is called once per frame
        void Update()
        {


            if (transform.position.x > -5)
            {
                walk();


            }
            else
            {
                if (timer >= atkSpeed) {

                    Attack();
                    timer = 0;
                    is_atk = true;
                }
                else
                {
                    timer += Time.deltaTime;
                   
                }
            }
        //Debug.Log(is_atk);
        }
    }

