 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class enemy_Boss : Enemy
{
    public Text hp_bar;

    private int fullHp;
    private bool walkForward;

    private bool walkBack;

    private bool walkPattern1;
    private bool walkPattern2;
    private float timer;


    private bool isAtk;
    private int atk1Num,atk2Num;
    private int randValue;

    private Animator ani;

    public int parryProbability;
    private float atkTimer;
    // Start is called before the first frame update

    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("k");
        if (collision.CompareTag("Player"))
        {
            if (Player.is_atk)
            {
                Debug.Log("hit");
                hit();
                Player.is_atk = false;
            }
        }
    }

    
    void Start()
    {
        ani = GetComponent<Animator>();
        fullHp = 20;
        walkForward = true;
        hp = fullHp;
        walkPattern1 = false; walkPattern2 = false;
        isAtk = false;
        parryProbability = 30;
    }

    private void activateTimer()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }

    private void activateAtkTimer()
    {
        atkTimer += Time.deltaTime;
        //Debug.Log(atkTimer);
    }

    private void decrease_atk1Num()
    {
        atk1Num--;
        Debug.Log(atk1Num);
    }
    private void decrease_atk2Num() {  atk2Num--;
    //Debug.Log(atk2Num);
    
    }
    private void Atk1()
    {
        
        Enemy.is_atk = true;
        ani.SetInteger("atk1", atk1Num);
    }
    private void Atk2()
    {

        Enemy.is_atk = true;
        ani.SetInteger("atk2", atk2Num);
    }

    
    private void backStep()
    {
        if (transform.position.x < -1)
        {
            //walkBack = false;

            transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
        }
        else if (walkPattern1)
        {

        }
        
    }

    
    private void hurtt()
    {
        if(hp > 0) {
        hp--;
        }
        else
        {
            Debug.Log("boss_died");
        }
    }
    
    private void shield()
    {
        ani.SetBool("shield", true);
        
    }
    private void parry()
    {
        ani.SetBool("parry", true);
    }
    public void hurt()
    {
        
        
        if (Player.atk_type)
        {
            hp -= 2;
        }
        else
        {
            hp -= 1;
        }
        if (hp <= 0)
        {
            dead();
        }


        Debug.Log("enemy hurt: " + hp);
        
    }

    private void hit()
    {
        if(atkTimer < 2) 
        {
            if (Random.Range(0, 100) < parryProbability)
            {
                parry();
            }
            else
            {
                hurt();
            }
        
        }
        else if (atkTimer < 3)
        {
            parry();
        }
        else 
        {
            if (Random.Range(0, 100) < parryProbability)
            {
                parry();
            }
            else
            {
                parry();
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(walkBack);
        hp_bar.text = hp.ToString();
        if (walkForward)
        {
            walk();
            if (transform.position.x < -5)
            {
                if(walkForward)
                {
                    atk1Num = 3;
                    atk2Num = 0;
                    
                }
                walkForward=false;
            }
        }


        else if (hp == fullHp / 2 && !walkPattern1)
        {
            walkBack = true;
            walkPattern1 = true; walkPattern2 = false;
        }
        else if (hp == 6 && !walkPattern2)
        {
            walkBack = true;
            walkPattern1 =false; walkPattern2 = true;
        }

        else if (walkBack && !isAtk)
        {
            backStep();
            activateTimer();
            if (walkPattern1)
            {
                if (timer >= 10)
                {
                    timer = 0; walkForward = true; walkBack = false;
                }
            }
            if (walkPattern2)
            {
                if (timer >= 5)
                {
                    if (Random.Range(0, 2) == 1)
                    {
                        timer = 0; walkForward = true; walkBack = false;
                    }
                }
            }
            

        }
        else
        {
            if ((atk1Num > 0) || (atk2Num > 0))
            {
                isAtk = true;
                atkTimer = 0;
            }
            else
            {
                isAtk = false;
            }


            if(!isAtk)
            {
                activateTimer();
                activateAtkTimer();
                if (timer >= 1)
                {
                    timer = 0;
                    if (Random.Range(0, 2) == 1)
                    {
                        randValue = Random.Range(1, 11);

                        if (randValue >= 9)
                        {
                            atk2Num = 3;
                        }
                        else if (randValue >= 6)
                        {
                            atk2Num = 2;
                        }
                        else
                        {
                            atk2Num = 1;
                        }

                        atk1Num = Random.Range(0, 2);
                        
                    }

                }
            }

            Atk1();
            Atk2();
        }

        
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            hurtt();
            
        }

        
    }

    private void aniParryInit()
    {
        ani.SetBool("parry", false);
    }
    private void aniShieldInit()
    {
        ani.SetBool("shield", false);
    }
}
