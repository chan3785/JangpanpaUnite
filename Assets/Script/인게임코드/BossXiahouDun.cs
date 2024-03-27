using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class BossXiahouDun : MonoBehaviour
{
    
  
    public Text hpText;
    public Slider hpSlider;
    private float hp, XiahouDunHP;
    public float atkSpeed;

    public bool isAtk;

    public AudioClip[] audios = new AudioClip[9];

    public static bool canBehave;
    public Animator anim;

    private bool canHurt;

    public GameObject player;
    private GameObject boss;
    public GameObject bossHp;
    public GameObject shadow;

    public int walkSpeed;
    private bool walkBack;
    private bool canAtk;


    private bool pat;
    private int patCnt;

    private float atkTimer,atkFloat;

    public int parryProbability;

    public static int atkDamage;

    private AudioSource aud;
    // Start is called before the first frame update


    public void activateAtkTimer()
    {
        if (canAtk)
        {
            atkTimer += Time.deltaTime;
            //Debug.Log(atkTimer);
            if(atkTimer >= atkFloat )
            {
                anim.SetBool("atk", true);
                
            }
        }
    }

    public void setTimer()
    {
        anim.SetBool("atk",false);
        atkTimer = 0;
        atkFloat = Random.Range(2, 5);
    }
    public void patEnd()
    {
        shadow.SetActive(false);
        pat = false;
        patCnt++;
        Debug.Log(atkDamage);
            shadow.SetActive(false);
    }

    public void walk()
    {

        if (pat)
        {
            if (transform.position.x > 7)
            {


                shadow.SetActive(true);
                shadow.SendMessage("spawn");
                atkDamage = 2 + patCnt;

            }
            else
            {
                if (walkBack)
                {
                    transform.Translate(new Vector3(1, 0, 0));
                    anim.SetBool("walk", true);
                }
                canHurt = false;
                canAtk = false;
            }
        }
        else
        {

            
            if (transform.position.x > -7)
            {
                transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
                anim.SetBool("jump", true);


            }
            else
            {
                anim.SetBool("jump", false);
                if (transform.position.x <= -7)
                {

                    canHurt = true;
                    canAtk = true;
                    
                }
            }
            
        }
    }

    

    public void actPat()
    {

    }

    private void actAtk()
    {
        player.SendMessage("Hurt", "XiaAtk");
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
        if (atkTimer <= 2)
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
        else if (atkTimer <= 3)
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
        if(hp <= 32 && patCnt == 1)
        {
            pat = true;walkBack = true;
        }
        if (hp <= 24 && patCnt == 2)
        {
            pat = true; walkBack = true;
        }
        if (hp <= 16 && patCnt == 3)
        {
            pat = true; walkBack = true;
        }
        if (hp <= 8 && patCnt == 4)
        {
            pat = true; walkBack = true;
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


    private void Awake()     
    {
        XiahouDunHP = 40;
        hp = XiahouDunHP;
        atkSpeed = 1;

        anim = GetComponent<Animator>();
        aud = gameObject.GetComponent<AudioSource>();
        canHurt = false;
        walkBack = false;
        
        player = GameObject.Find("ZhangFei (1)");
        boss = GameObject.Find("ÇÏÈÄµ·");
        shadow = GameObject.Find("Shadow");

        pat = true;
        patCnt = 0;
        atkTimer = 0;
        atkFloat = Random.Range(2, 5);

        parryProbability = 30;
        atkDamage = 1;
    }
    void Start()
    {
        shadow.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        walk();
        activateAtkTimer();

        if (Input.GetKeyDown(KeyCode.M))
        {
            hp -= 8;
            Damage(0);
            //actAtk();
            //pat = true;
            //walkBack = true;
        }
    }

    private void aniWalkEnd()
    {
        walkBack = false;
        anim.SetBool("walk", false);
    }

    private void aniHurtInit()
    {
        anim.SetBool("hurt", false);
    }
    private void anishieldInit()
    {
        anim.SetBool("shield", false);
    }
    private void aniparryInit()
    {
        anim.SetBool("parry", false);
    }
    

    private void ani_init()
    {
        anim.SetBool("parry", false);
        anim.SetBool("shield", false);
        anim.SetBool("hurt", false);
        anim.SetBool("walk", false);
    }
}
