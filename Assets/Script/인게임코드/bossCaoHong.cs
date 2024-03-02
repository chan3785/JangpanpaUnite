using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossCaoHong : MonoBehaviour
{
     private int hp;
    public float atkSpeed;

    public AudioClip[] audios = new AudioClip[9];

    public static bool canBehave;
    public Animator anim;

    private bool canHurt;

    public GameObject player;
    private GameObject boss;
    public GameObject arrow1;
    public GameObject arrow2; 
    public GameObject CaoHong;
    Vector3 position;

    public int walkSpeed;
    private bool walkBack;
    private bool canAtk;

    private int atk1Num, atk2Num, atk3Num;

    
    private float timer;
    private int randValue;


    private bool pat1, pat2, turn;

    private float patTimer;
    public float atkTimer;
    private float delay;

    public int parryProbability;

    public int distance, arrow_speed;
    public float arrow;
    private float arrow_parry_timer, arrow_shield_timer;
    public float arrow_location;
    private float arrow_timer;
    private float shield_timer;
    private float arrow1_approachingTime;
    private bool isGuard;
    private bool arrow1_shot;
    private bool can_Parrying;

    //public GameObject Stone;
    //private bool canThrow, canParryStone;


    //private AudioSource aud;

    private void activateTimer()
    {
        timer += Time.deltaTime;
        //Debug.Log(timer);
    }
    private void activatePatTimer()
    {
        patTimer += Time.deltaTime;
        Debug.Log(patTimer);
        //Debug.Log(patTimer);
    }

    private void activateAtkTimer()
    {
        atkTimer += Time.deltaTime;
        //Debug.Log(atkTimer);
    }

    // public void walk()
    // {
    //     if (!walkBack)
    //     {
            
    //         if (!walkBack && (pat1 || pat2) && patTimer >= 0)
    //         {
    //             activatePatTimer();
                
    //             if (pat1)
    //             {
    //                 if (patTimer >= 10)
    //                 {
    //                     patTimer = -1;
    //                 }

    //             }
    //             else if (pat2)
    //             {
    //                 if (patTimer >= 5)
    //                 {
    //                     if (Random.Range(1, 3) == 1)
    //                     {
    //                         patTimer = 0;
    //                     }
    //                     else
    //                     {
    //                         patTimer = -1;
    //                     }
    //                 }
    //             }
                
    //         }
    //         else
    //         {
    //             if (transform.position.x > -6.5)
    //             {
    //                 transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
    //                 anim.SetBool("walk", true);
    //                 //atk1Num = 3;

    //             }
    //             else
    //             {
    //                 anim.SetBool("walk", false);
    //                 canHurt = true;
    //                 canAtk = true;
    //             }
    //         }

    //     }
    //     else if (walkBack)   
    //     {
    //         canHurt = false;
    //         if (transform.position.x < 1)
    //         {
                
    //             anim.SetBool("walkBack", true);
    //             if (turn)
    //             {
    //                 transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
    //             }
    //         }
    //         else
    //         {
    //             anim.SetBool("walkBack", false);
    //             walkBack = false;
    //             canAtk = false;
    //             turn = false;
    //             patTimer = 0;
    //         }
    //     }
    // }

    private void activateArrowTimer()
    {
        arrow_timer += Time.deltaTime;
    }

    public void walk()
    {
        if (transform.position.x > -6.5)
        {
            transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
            canHurt = true;
            canAtk = true;
        }
    }

    private void Hurt(string type)
    {
        if (canHurt)
        {
            if (type == "Atk1")
            {
                Damage(1);
            }
            else if (type =="Atk2")
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
                //ani_init();
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
        Debug.Log("atk1 decreased");
    }
    private void decrease_atk2Num()
    {
        atk2Num--;
        Debug.Log("atk2 decreased");
    }
    private void decrease_atk3Num()
    {
        atk3Num--;
         Debug.Log("atk3 decreased");
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


    private void aniThrowInit()
    {
        anim.SetBool("throw", false);
    }

    private void aniWalkBackInit()
    {
        turn = true;
    }

    private void Die()
    {

    }

    private void guardOn()
    {
        isGuard = true;
        shield_timer = arrow_timer;
        Debug.Log("shield: " + shield_timer);
    }

    private void guardOff()
    {
        isGuard = false;
    }

    public void atk1_prepare()
    {
        transform.position = new Vector3(7.5f, 0.5f, 0);
    }

    public void atk2_prepare()
    {
        transform.position = new Vector3(0, 0.5f, 0);
    }


    public void back_in_place()
    {
        transform.position = new Vector3(-6.5f, 0.5f, 0);
    }

   private void Atk1()
    {
        anim.SetInteger("atk1", atk1Num);
        
        if(arrow1.activeSelf && arrow1_shot)
        {
            anim.SetBool("can_attack", false);
            activateArrowTimer();
            if(arrow1.transform.position.x > -6)
            {
            arrow1.transform.Translate(new Vector3(-arrow_speed * Time.deltaTime, 0, 0));
            }
            
            if(arrow_timer > arrow1_approachingTime)
            {
                arrow1_shot = false;
                if(isGuard)
                {
                    if(arrow1_approachingTime - shield_timer > 0 && arrow1_approachingTime - shield_timer < 4)
                    {
                        can_Parrying = true;
                    }
                    else
                    {
                        Debug.Log("guard");
                        player.SendMessage("guard");
                        arrow_timer = 0;
                        shield_timer = 0;
                        arrow1.SetActive(false);
                        arrow1.transform.position = new Vector3(8.5f, 0.5f, 0);
                        anim.SetBool("can_attack", true);
                    }
                }
                else
                {
                    actAtk1();
                    arrow_timer = 0;
                    shield_timer = 0;
                    arrow1.SetActive(false);
                    arrow1.transform.position = new Vector3(8.5f, 0.5f, 0);
                    anim.SetBool("can_attack", true);
                }
            }
        }
    }

    private void atk1_parry()
    {
        Debug.Log("패링 함수 작동중");
        player.SendMessage("parryOn");
        if(arrow1.transform.position.x < 6.5f)
        {
            arrow1.GetComponent<SpriteRenderer>().flipX = false;
            arrow1.transform.Translate(new Vector3(arrow_speed * Time.deltaTime, 0, 0));
            Hurt("Atk1");
        }
        else
        {
            arrow_timer = 0;
            shield_timer = 0;
            arrow1.SetActive(false);
            arrow1.GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("can_attack", true);
            can_Parrying = false;
        }
    }

    private void actAtk1()
    {
        player.SendMessage("Hurt", "nAtk");
        atkTimer = 0;
    }

    public void atk1_arrow()
    {
        arrow1.SetActive(true);
        arrow1_shot = true;
    }

    private void Atk2()
    {
        anim.SetInteger("atk2", atk2Num);
        // if(atk2Num > 0 && atk3Num <= 0)
        // {
        //     anim.Play("atk2");
        // }
        
        // if(arrow2.activeSelf && arrowshot)
        // {
        //     activateArrowTimer();
        //     if(arrow2.transform.position.x > -7.5)
        //     {
        //         arrow2.transform.Translate(new Vector3(-arrow_speed * Time.deltaTime, (arrow_speed/5) * Time.deltaTime, 0));
        //     }
        //     else
        //     {
        //         arrow2.GetComponent<Animator>().SetBool("fall", true);
        //         Debug.Log("a");
        //     }

        //     // if(arrow_timer > arrow1_approachingTime / 2)
        //     // {
        //     //     arrowshot = false;
        //     //     arrow2.transform.position = new Vector3(1, -1, 0);
        //     //     arrow2.SetActive(false);
        //     //     anim.SetBool("can_attack", true);
        //     // }
        // }
    }

    private void actAtk2()
    {
        player.SendMessage("Hurt", "sAtk");
        atkTimer = 0;
    }

    public void atk2_arrow()
    {
        //arrow2.SetActive(true);
    }
    
    private void Atk3()
    {
        anim.SetInteger("atk3", atk3Num);
        //Debug.Log("atk3");
        // if(atk3Num > 0)
        // {
        //     anim.Play("atk3");
        // }
    }

    private void actAtk3()
    {
        player.SendMessage("Hurt", "nAtk");
        atkTimer = 0;
    }

    private void Awake()  // ���� �ʱ�ȭ
    {
        hp = 20; 
        atkSpeed = 1;
        
        anim = GetComponent<Animator>();
        //aud = gameObject.GetComponent<AudioSource>();
        canHurt = false;
        walkBack = false;
          
        canAtk = false;
        atk1Num = 0;atk2Num = 0;atk3Num = 0;
        pat1 = false; pat2 = false; turn = false;
        patTimer = -1;
        parryProbability = 30;
        atkTimer = 0;

        player = GameObject.Find("ZhangFei (1)");
        boss = GameObject.Find("조홍");

        position = CaoHong.transform.localPosition;
        arrow1_shot = false;
        can_Parrying = false;
    }
    
    private void Start()
    {
        anim.speed = atkSpeed;  
        arrow1_approachingTime = 15.5f / arrow_speed;
    }

    void Update()
    {
        // Debug.Log("atk1: " + atk1Num);
        // Debug.Log("atk2: " + atk2Num);
        // Debug.Log("atk3: " + atk3Num);
        if (!canAtk)
        {
            walk();
        }
        else
        {
                activateTimer();
                activateAtkTimer();

                Atk1();
                Atk2();
                Atk3();

                if(can_Parrying)
                {
                    Debug.Log("a");
                    atk1_parry();
                }

                if (atk1Num <= 0 && atk2Num <= 0 && atk3Num <= 0)
                {
                    activateTimer();
                    activateAtkTimer();

                    if (timer >= 1)
                    {
                        timer = 0;
                        atk1Num = Random.Range(0, 2);
                        atk2Num = Random.Range(0, 2);
                        atk3Num = Random.Range(0, 2);
                    }
            }   
        }
    }
}
