using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


 


public class bossZhiangLiao: MonoBehaviour
{
    private int hp;
    public float atkSpeed;

    public AudioClip[] audios = new AudioClip[9];

    public static bool canBehave;
    public Animator anim;

    private bool canHurt;

    public GameObject player;
    private GameObject boss;

    public int walkSpeed;
    private bool walkBack;
    private bool canAtk;

    private int atk1Num, atk2Num;

    
    private float timer;
    private int randValue;


    private bool pat1, pat2, turn;

    private float patTimer;
    public float atkTimer;
    private float delay;

    public int parryProbability;



    private AudioSource aud;

    private float hurtTimer;
    /*
    private void Atk1()
    {
        
        player.SendMessage("Hurt", "nAtk");

    }

    private void Atk2()
    {

        player.SendMessage("Hurt", "nAtk");

    }
    */

    private void activateTimer()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }
    private void activatePatTimer()
    {
        patTimer += Time.deltaTime;
        Debug.Log(patTimer);
    }

    private void activateAtkTimer()
    {
        atkTimer += Time.deltaTime;
        //Debug.Log(atkTimer);
    }

    private void Atk1()
    {

        
        anim.SetInteger("atk1", atk1Num);
    }

    private void actAtk1()
    {
        player.SendMessage("Hurt", "nAtk");
        atkTimer = 0;
    }
    private void Atk2()
    {

        
        anim.SetInteger("atk2", atk2Num);
    }

    private void actAtk2()
    {
        player.SendMessage("Hurt", "sAtk");
        atkTimer = 0;
    }
    
    private void Hurt(string type)
    {
       
            if (canHurt)
            {
                if (type == "Atk1")
                {
                    Damage(1);
                }
                else if (type == "Atk2")
                {
                    Damage(2);
                }

            }
        
        
    }

    private void Damage(int dam)
    {
        if(atkTimer <= 2)
        {
            if (Random.Range(0, 100) <= parryProbability)
            {
                shield();
            }
            else
            {
                ani_init();
                anim.Play("hurt");

                hp -= dam;
                Debug.Log("Boss: " + hp);
            }
        }
        else if(atkTimer <= 3)
        {
            shield();
        }
        else
        {
            if (Random.Range(0, 100) <= parryProbability)
            {
                parry();
            }
            else
            {
                shield();
            }
        }
    }

    private void shield()
    {
        Debug.Log("shield");
        anim.SetBool("shield", true);
        anim.Play("shield");

    }
    private void parry()
    {
        anim.SetBool("parry", true);
        anim.Play("parry");
        player.SendMessage("StatusEffect");

    }

    private void Die()
    {

    }


    public void walk()
    {
        if (!walkBack)
        {
            
            if (!walkBack && (pat1 || pat2) && patTimer >= 0)
            {
                activatePatTimer();
                
                if (pat1)
                {
                    if (patTimer >= 10)
                    {
                        patTimer = -1;
                    }

                }
                else if (pat2)
                {
                    if (patTimer >= 5)
                    {
                        if (Random.Range(1, 3) == 1)
                        {
                            patTimer = 0;
                        }
                        else
                        {
                            patTimer = -1;
                        }
                    }
                }

                
            }
            else
            {
                if (transform.position.x > -6.5)
                {
                    transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
                    anim.SetBool("walk", true);
                    atk1Num = 3;

                }
                else
                {
                    anim.SetBool("walk", false);
                    canHurt = true;
                    canAtk = true;
                }
            }

        }
        else if (walkBack)   
        {
            canHurt = false;
            if (transform.position.x < 1)
            {
                
                anim.SetBool("walkBack", true);
                if (turn)
                {
                    transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
                }
            }
            else
            {
                anim.SetBool("walkBack", false);
                walkBack = false;
                canAtk = false;
                turn = false;
                patTimer = 0;
            }
        }
        

    }

    private void Awake()  // 변수 초기화
    {
        hp = 20; 
        atkSpeed = 1;
        
        anim = GetComponent<Animator>();
        aud = gameObject.GetComponent<AudioSource>();
        canHurt = false;
        walkBack = false;
          
        canAtk = false;
        atk1Num = 0;atk2Num = 0;
        pat1 = false; pat2 = false; turn = false;
        patTimer = -1;
        parryProbability = 30;
        atkTimer = 0;


        player = GameObject.Find("ZhangFei (1)");
        boss = GameObject.Find("장료");

    }
    private void Start()
    {
        anim.speed = atkSpeed;
        
    }


    private void Update()
    {
        
        //Debug.Log(walkBack);Debug.Log(patTimer);
        if (!canAtk)
        {
            walk();
        }
        else
        {
            Atk1();
            Atk2();

            if (atk1Num <= 0 && atk2Num <= 0)
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

            
        }



        if(!pat1 && !pat2 && (atk1Num <= 0 && atk2Num <= 0))
        {
            if (hp <= 10)
            {
                pat1 = true;
                canAtk = false;
                walkBack = true;
              

            }

        }
        else if (!pat2 && (atk1Num <= 0 && atk2Num <= 0))
        {
            if (hp <= 6)
            {
                pat2 = true;
                canAtk = false;
                walkBack = true;
               

            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hp -= 1;
            Debug.Log(hp);

        }


    }



    public void hurtAble()
    {
        canHurt = true;
    }

    public void hurtDisable()
    {
        canHurt = false;
    }

    private void ani_init()
    {
       


    }


    private void dead()
    {
        anim.Play("die");
    }

    public void anidead()
    {
        boss.SetActive(false);
    }
    private void decrease_atk1Num()
    {
        atk1Num--;
        Debug.Log(atk1Num);
    }
    private void decrease_atk2Num()
    {
        atk2Num--;
        Debug.Log(atk2Num);

    }

    private void aniParryInit()
    {
        anim.SetBool("parry", false);
    }
    private void aniShieldInit()
    {
        anim.SetBool("shield", false);
    }

    private void aniHurtInit()
    {
        anim.SetBool("hurt", false);
    }


   

    private void aniWalkBackInit()
    {
        turn = true;
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



}
