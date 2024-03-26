using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


 


public class bossZhiangLiao: MonoBehaviour
{

    public Slider hpSlider;
    public Text hpText;
    private float hp, ZhiangLiaoHP;
    public float atkSpeed;


    public AudioClip[] audios = new AudioClip[9];

    public static bool canBehave;
    public Animator anim;

    private bool canHurt;

    public GameObject player;
    public GameObject color;

    private GameObject boss;
    public GameObject bossHp;

    public int walkSpeed;
    private bool walkBack;
    private bool canAtk;

    private int atk1Num, atk2Num, atk3Num, spin;

    
    private float timer;
    private int randValue;


    private bool pat1, pat2,pat3, turn;

    private float patTimer;
    public float atkTimer;
    private float delay;

    public int parryProbability;



    private AudioSource aud;

    private float hurtTimer;


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
        player.SendMessage("Hurt", "zAtk");
        atkTimer = 0;
    }


    private void Atk2()
    {
        anim.SetInteger("atk2", atk2Num);
    }

    private void actAtk2()
    {
        player.SendMessage("Hurt", "zAtk");
        atkTimer = 0;
    }

    private void Atk3()
    {
        anim.SetInteger("atk3", atk3Num);
    }
    
    private void actAtk3()
    {
        player.SendMessage("Hurt", "zAkk");
        atkTimer = 0;
    }

    private void Spin()
    {
        anim.SetInteger("spin", spin);
    }

    private void actSpin()
    {
        player.SendMessage("Hurt", "nAtk");
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
        ZhiangLiaoHP = 30;
        hp = ZhiangLiaoHP;
        atkSpeed = 1;
        
        anim = GetComponent<Animator>();
        aud = gameObject.GetComponent<AudioSource>();
        canHurt = false;
        walkBack = false;
          
        canAtk = false;
        atk1Num = 0;atk2Num = 0; atk3Num = 0;
        pat1 = false; pat2 = false; turn = false;
        patTimer = -1;
        parryProbability = 30;
        atkTimer = 0;

        player = GameObject.Find("ZhangFei (1)");
        boss = GameObject.Find("장료");
        //color.SetActive(false);

    }
    private void Start()
    {
        anim.speed = atkSpeed;
    }


    private void Update()
    {
        anim.speed = atkSpeed;
        hpSlider.value = hp / ZhiangLiaoHP;
        hpText.text = hp + " / " + ZhiangLiaoHP;
        //Debug.Log(walkBack);Debug.Log(patTimer);
        if (!canAtk)
        {
            walk();
        }
        else
        {
            Atk1();
            Atk2();
            Atk3();
            Spin();
            if (atk1Num <= 0 && atk2Num <= 0&& atk3Num <= 0)
            {
                activateTimer();
                activateAtkTimer();

                if (timer >= 3)
                {
                    timer = 0;
                    if (Random.Range(0, 3) == 1&&spin == 0)
                    {
                        atk1Num = 3;
                    }
                    else if(Random.Range(0,2)==1&&spin==0)
                    {
                        atk3Num = 1;
                    }
                    else
                    {
                        Debug.Log("spin");
                        spin = 2;
                    }
                }
            }

            
        }



        if(!pat1 && !pat2 && (atk1Num <= 0 && atk2Num <= 0 && atk3Num <= 0))
        {
            
            if (hp <= 15)
            {
                pat1 = true;
                color.SetActive(true);
                hp = 30;
            }

        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hp -= 1;
            Debug.Log(hp);

        }

        if(hp <= 0)
        {
            dead();
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
    private void decrease_atk3Num()
    {
        atk3Num--;
        Debug.Log(atk2Num);
    }
    private void decrease_spinn()
    {
        spin--;
        Debug.Log(atk2Num);
    }
    private void aniInit()
    {
        anim.SetInteger("atk1", 0); anim.SetInteger("atk2", 0);
        anim.SetInteger("atk3", 0);
        anim.SetBool("parry", false); anim.SetBool("shield", false); anim.SetBool("hurt", false); 
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
