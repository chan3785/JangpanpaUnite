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
    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
        fullHp = 20;
        walkForward = true;
        hp = fullHp;
        walkPattern1 = false; walkPattern2 = false;
        isAtk = false;
    }

    private void activateTimer()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
    }
    private void decrease_atk1Num()
    {
        atk1Num--;
        Debug.Log(atk1Num);
    }
    private void decrease_atk2Num() {  atk2Num--;
    Debug.Log(atk2Num);}
    private void Atk1()
    {
        
        
        ani.SetInteger("atk1", atk1Num);
    }
    private void Atk2()
    {
        
        
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

    private void hurt()
    {
        if(hp > 0) {
        hp--;
        }
        else
        {
            Debug.Log("boss_died");
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
            }
            else
            {
                isAtk = false;
            }


            if(!isAtk)
            {
                activateTimer();
                if (timer >= 1)
                {
                    timer = 0;
                    randValue = Random.Range(1,11);

                    if(randValue >= 9)
                    {
                        atk2Num = 3;
                    }
                    else if(randValue >= 6)
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

            Atk1();
            Atk2();
        }

        
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            hurt();
            
        }

        
    }

    
}
