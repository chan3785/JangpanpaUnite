 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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

    private bool canHit;
    private float hitTimer;

    public static bool isThrowing;
    private float throwTimer;
    public GameObject stone;
    private bool canParryStone;
    // Start is called before the first frame update

    public GameObject boss;

    public AudioSource aud;

    public AudioClip[] audios = new AudioClip[9];
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("k");
        if (collision.CompareTag("Player"))
        {
            Debug.Log("hit");
            if (canHit)
            {
                //Debug.Log("hit");
                hit();
                //Player.is_atk = false;
                canHit = false;
                hitTimer = 0;
            }
        }
        if (collision.CompareTag("playerStone"))
        {
            //Debug.Log("hit");
            if(canParryStone)
            {
                ani.SetBool("parry", true);
                ani.Play("parry");
                stoneThrow();
                canParryStone = false;
            }
            else
            {
                if(canHit)
                {
                    ani.SetBool("hurt", true);
                    ani.Play("hurt");
                    hp -= 2;
                    
                    if (hp <= 0)
                    {
                        dead();
                    }
                    canHit = false;
                    hitTimer = 0;
                }
            }
        }
    }

    
    void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
        fullHp = 20;
        walkForward = true;
        hp = fullHp;
        walkPattern1 = false; walkPattern2 = false;
        isAtk = false;
        parryProbability = 30;
        canHit = true;
        isThrowing = false;
        stone.SetActive(false);
    }

    private void activateTimer()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }

    private void activateThrowTimer()
    {
        throwTimer -= Time.deltaTime;
        Debug.Log(throwTimer);
    }

    private void activateAtkTimer()
    {
        atkTimer += Time.deltaTime;
        //Debug.Log(atkTimer);
    }

    private void activateHitTimer()
    {
        hitTimer += Time.deltaTime;
        //Debug.Log(hitTimer);
    }
    private void decrease_atk1Num()
    {
        atk1Num--;
        Debug.Log(atk1Num);
    }
    private void decrease_atk2Num() 
    {  atk2Num--;
    Debug.Log(atk2Num);
    
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


    public void stoneThrow()
    {
        
        
            stone.SetActive(true);
            stone.SendMessage("bossThrow");
            isThrowing = true;
        
    }
    public void walk()
    {
        transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
        ani.SetBool("walk", true);
        ani.Play("walk");

    }
    private void backStep()
    {
        if (transform.position.x < 1)
        {
            //walkBack = false;
            ani.SetBool("walkBack", true);
            ani.Play("walkBack");
            throwTimer = 5;
            transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
            canParryStone = true;
        }
        else
        {
            
            if (!isThrowing)
            {
                activateThrowTimer();
                if (throwTimer < 4f)
                {
                    ani.SetBool("throw", true);
                    stoneThrow();
                    throwTimer = 5;
                }
            }
            ani.SetBool("walk", false);
            //transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);



        }
        
    }

    
    
    
    private void shield()
    {
        Debug.Log("shield");
        ani.SetBool("shield", true);
        ani.Play("shield");
        
    }
    private void parry()
    {
        ani.SetBool("parry", true);
        ani.Play("parry");
        Player.canBehave = false;
    }
    public void hurt()
    {

        ani.SetBool("hurt", true);
        ani.Play("hurt");
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
            ani.SetBool("die", true);
            ani.Play("die");
            dead();
        }
            


        Debug.Log("enemy hurt: " + hp);
        
    }
    
    private void dead()
    {
        ani.Play("die");
    }

    public void anidead()
    {
        boss.SetActive(false);
    }
    private void hit()
    {
        Debug.Log("hit in");
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
                ani.SetBool("walk", false);

            }
        }


        else if (hp == fullHp / 2 && !walkPattern1)
        {
            walkBack = true;
            walkPattern1 = true; walkPattern2 = false;isThrowing = false;
            

        }
        else if (hp == 6 && !walkPattern2)
        {
            walkBack = true;
            walkPattern1 =false; walkPattern2 = true;isThrowing=false;
            

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

            if (!canHit)
            {
                activateHitTimer();
                if(hitTimer >= 0.5)
                {
                    canHit = true;
                    hitTimer = 0;
                }
            }
        }
        //Debug.Log(canHit);
        
        


        if (Input.GetKeyDown(KeyCode.Space))
        {
            //hurtt();
            hurt();
            
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

    private void aniHurtInit()
    {
        ani.SetBool("hurt", false);
    }


    private void aniThrowInit()
    {
        ani.SetBool("throw", false);
    }
    
    private void aniWalkBackInit()
    {
        ani.SetBool("walkBack", false);
    }
    private void audioAtk1()
    {
        aud.clip = audios[0];
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }


    private void audioAtk2_1()
    {
        aud.clip = audios[3];
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }

    private void audioAtk2_2()
    {
        aud.clip = audios[4];
        
        aud.Play();
        
    }

    private void audioShield()
    {
        aud.clip = audios[7];
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }

    private void audioParry()
    {
        aud.clip = audios[8];
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }

    private void audioThrowStone()
    {
        aud.clip = audios[5];
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }



}
