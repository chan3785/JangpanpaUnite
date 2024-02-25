using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class newPlayer : MonoBehaviour
{

    private int hp, subHp;
    public float atkSpeed;
    private float atk1Len, atk3Len;
    public static bool parry;

    public static bool canBehave;
    public Animator anim;
    private AudioSource aud;

    private bool canHurt;

    public GameObject boss;
    public GameObject canBehavetxt;

    private bool isGuard,canParry;
    private float StatusEffectTimer;

    public GameObject stone;

    public AudioClip[] audios = new AudioClip[9];

    public bool aud2;
    private bool isAtk;
    private void Atk1()
    {

        if (!isAtk)
        {
            anim.SetInteger("atkType", Random.Range(1, 3));
            anim.SetBool("Atk1", true);
            aud2 = true;
            isAtk = true;
        }
 
    }

    private void actAtk1()
    {
        boss.SendMessage("Hurt", "Atk1");
    }

    private void Atk2()
    {
        anim.SetBool("Atk2_", true);  
    }

    private void activateAtk2()
    {
        anim.SetBool("Atk2", true);
        
        boss.SendMessage("Hurt", "Atk2");
        
    }

    private void Hurt( string type)
    {
        Debug.Log(type);
        if (canHurt)
        {
            isAtk = false;
            if (type == "nAtk")
            {
                Damage(1);

                
            }
            else if (type == "sAtk")
            {
                Damage(2);
            }
            else if (type == "Throwing")
            {

            }
        }
        
    }

    

    private void Damage(int dam)
    {
        
        if (canHurt)
        {
            if (isGuard)
            {
                if (canParry)
                {
                    Debug.Log("parry");
                    anim.SetBool("succeed",true);

                }
                else
                {
                    Debug.Log("parry");
                    anim.SetBool("succeed", true);
                }
            }
            else
            {
                ani_init();
                anim.Play("Hurt");
                //anim.SetBool("hurt", true);
                hp -= dam;
                Debug.Log("player: "+hp);
            }
        }
    }

    private void stoneHit()
    {
        if (isGuard)
        {
            if (canParry)
            {
                
                anim.SetBool("succeed", true);
                stone.SendMessage("playerThrow");

            }
            else
            {

                anim.SetBool("succeed", true);
            }
        }
        else
        {
            ani_init();
            anim.Play("Hurt");
            //anim.SetBool("hurt", true);
            hp -= 2;
            Debug.Log("player: " + hp);
            bossThrowingStone.isThrown = false;
            stone.SetActive(false);
        }
    }

    private void StatusEffect()
    {
        canBehave = false;
        canBehavetxt.SetActive(true);
    }

    private void shield()
    {
        
    }

    private void Die()
    {
        //anim.Play("died");
    }

    private void guard()
    {
        anim.SetBool("Shield", true);
        isGuard = true;
    }

    private void guardOff()
    {
        anim.SetBool("Shield", false);
        isGuard = false;
    }
    private void parryOn()
    {
        canParry = true;
    }
    private void parryOff()
    {
        canParry = false;
    }
    private void Awake()
    {
        hp = 3; subHp = 0;
        atkSpeed = 1;
        atk1Len = 0.5f; atk3Len = 0.5f;
        anim = GetComponent<Animator>();
        canHurt = true;
        canBehave =

        canBehavetxt = GameObject.Find("playerBehave");

        isGuard = false;canParry = false;

        StatusEffectTimer = 0;
        isAtk = false;  

    }
    private void Start()
    {
        anim.speed = atkSpeed;
        canBehavetxt.SetActive(false);
        stone.SetActive(false);
        bossThrowingStone.isThrown = false;
        aud = gameObject.GetComponent<AudioSource>();
    }


  

    private void Update()
    {
        if (!canBehave)        //상태이상 타이머 작동
        {
            Debug.Log(StatusEffectTimer);
            StatusEffectTimer += Time.deltaTime;
            if (StatusEffectTimer >= 3)
            {
                StatusEffectTimer = 0;
                canBehave = true;
                canBehavetxt.SetActive(false);
            }
        }
        //Debug.Log(canParry);
        //Debug.Log(canBehave);
    }



    public void hurtAble()
    {
        canHurt=true;
    }

    public void hurtDisable()
    {
        canHurt=false;
    }


    private void ani_init()
    {
        anim.SetBool("Atk2", false);
        anim.SetBool("Atk1", false);
        anim.SetBool("Atk2_", false);
        anim.SetBool("hurt", false);
       

        
    }

    private void aniAtk1Init()
    {
        anim.SetBool("Atk1", false);
        isAtk = false;
    }

    private void aniAtk3Init()
    {
        anim.SetBool("Atk2", false);
        anim.SetBool("Atk2_", false);
    }

    private void aniParryInit()
    {
        anim.SetBool("succeed", false);
        
    }

    private void audioParry()
    {
        aud.clip = audios[8];
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }

    private void audioAtk1()
    {
        aud.clip = audios[1];
        if (!aud.isPlaying)
        {
            if (aud2)
            {
                aud.Play();
                aud2 = false;
            }
        }
    }
    private void audioAtk2()
    {
        aud.clip = audios[3];
        if (!aud.isPlaying)
        {
            if (aud2)
            {
                aud.Play();
                aud2 = false;
            }
        }
    }
    private void audioAtk3()
    {
        aud.clip = audios[5];
        if (!aud.isPlaying)
        {
            aud.Play();
        }
    }
}
