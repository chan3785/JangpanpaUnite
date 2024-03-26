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

    public GameObject[] hps = new GameObject[9];
    public GameObject[] subHps = new GameObject[9];
    public GameObject[] black = new GameObject[6];

    private int hp, subHp;
    public float atkSpeed;
    private float atk1Len, atk3Len;
    public static bool parry;

    public static bool canBehave;
    public Animator anim;
    private AudioSource aud;

    private bool canHurt;

    public GameObject boss;
    public GameObject bossZhiangLiao;

    public GameObject[] sword = new GameObject[10];
    public GameObject[] spear = new GameObject[10];
    public GameObject canBehavetxt;
    public GameObject EnemyManager;

    private bool isGuard,canParry;
    private float StatusEffectTimer;

    public GameObject stone;

    public AudioClip[] audios = new AudioClip[9];

    public bool aud2;
    private bool isAtk;

    public GameObject popup;
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
        useSubHp();
        boss.SendMessage("Hurt", "Atk1");
        bossZhiangLiao.SendMessage("Hurt", "Atk1");
        //sword[EnemySpawnManager.curSwordNum].SendMessage("Hurt", "Atk1");
        EnemyManager.SendMessage("Hurt", "Atk1");



    }

    private void Atk2()
    {
        anim.SetBool("Atk2_", true);  
    }

    private void activateAtk2()
    {
        anim.SetBool("Atk2", true);
        useSubHp();
        boss.SendMessage("Hurt", "Atk2");
        //sword[EnemySpawnManager.curSwordNum].SendMessage("Hurt", "Atk2");
        bossZhiangLiao.SendMessage("Hurt", "Atk2");
        EnemyManager.SendMessage("Hurt", "Atk2");


    }

    private void Hurt( string type)
    {
       // Debug.Log(type);
        if (canHurt)
        {
            isAtk = false;
            if (type == "nAtk")
            {
                Damage(2);

                
            }
            else if (type == "sAtk")
            {
                Damage(1);
            }
            else if (type == "Throwing")
            {

            }
            else if (type == "swordAtk")
            {
                Damage(1);
            }
            else if (type == "zAtk")
            {
                Damage(1);
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
                   // Debug.Log("parry");
                    anim.SetBool("succeed",true);

                }
                else
                {
                    //Debug.Log("parry");
                    shield();
                    anim.SetBool("succeed", true);
                }
            }
            else
            {
                ani_init();
                anim.Play("Hurt");
                //anim.SetBool("hurt", true);
                hp -= dam;
                //Debug.Log("player: "+hp);
                subHp = 0;
            }
        }
        for (int i = 0; i < hp; i++)
        {
            hps[i].SetActive(true);
        }
        for (int i = hp; i < 3 + Settings.health; i++)
        {
            hps[i].SetActive(false);
        }
        if (hp <= 0)
        {
            Die();
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
        subHp++;
    }

    private void Die()
    {
        //anim.Play("died");
        Time.timeScale = 0;
        print("died");
        //SceneManager.LoadScene("MainScene");
        popup.SetActive(true);
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

    private void useSubHp()
    {
        //Debug.Log("use subhp");
        if (subHp > 0)
        {
            hp+=subHp; subHp=0;
        }
    }
    private void Awake()
    {
        hp = 3 + Settings.health; ; subHp = 0;
        atkSpeed = 1;
        atk1Len = 0.5f; atk3Len = 0.5f;
        anim = GetComponent<Animator>();
        canHurt = true;
        canBehave =

        canBehavetxt = GameObject.Find("playerBehave");
        EnemyManager = GameObject.Find("SpawnManager");
        //sword = GameObject.Find("CaoCao(Sword)");

        isGuard = false;canParry = false;

        StatusEffectTimer = 0;
        isAtk = false;


        hps[0] = GameObject.Find("HPBar (1)");
        hps[1] = GameObject.Find("HPBar (2)");
        hps[2] = GameObject.Find("HPBar (3)");
        hps[3] = GameObject.Find("HPBar (4)");
        hps[4] = GameObject.Find("HPBar (5)");
        hps[5] = GameObject.Find("HPBar (6)");
        hps[6] = GameObject.Find("HPBar (7)");
        hps[7] = GameObject.Find("HPBar (8)");
        hps[8] = GameObject.Find("HPBar (9)");



        subHp = 0;
        subHps[0] = GameObject.Find("SubHPBar (1)");
        subHps[1] = GameObject.Find("SubHPBar (2)");
        subHps[2] = GameObject.Find("SubHPBar (3)");
        subHps[3] = GameObject.Find("SubHPBar (4)");
        subHps[4] = GameObject.Find("SubHPBar (5)");
        subHps[5] = GameObject.Find("SubHPBar (6)");
        subHps[6] = GameObject.Find("SubHPBar (7)");
        subHps[7] = GameObject.Find("SubHPBar (8)");
        subHps[8] = GameObject.Find("SubHPBar (9)");

        black[0] = GameObject.Find("base (9)");
        black[1] = GameObject.Find("base (8)");
        black[2] = GameObject.Find("base (7)");
        black[3] = GameObject.Find("base (6)");
        black[4] = GameObject.Find("base (5)");
        black[5] = GameObject.Find("base (4)");
    }
    private void Start()
    {
        anim.speed = atkSpeed;
        canBehavetxt.SetActive(false);
        stone.SetActive(false);
        bossThrowingStone.isThrown = false;
        aud = gameObject.GetComponent<AudioSource>();

        for (int i = 8; i >= hp; i--)
        {
            hps[i].SetActive(false);
        }
        subHps[0].SetActive(false);
        subHps[1].SetActive(false);
        subHps[2].SetActive(false);
        subHps[3].SetActive(false);
        subHps[4].SetActive(false);
        subHps[5].SetActive(false);
        subHps[6].SetActive(false);
        subHps[7].SetActive(false);
        subHps[8].SetActive(false);

        for (int i = 0; i < Settings.health; i++)
        {
            black[i].SetActive(false);
        }
    }


  

    private void Update()
    {
        subHps[0].SetActive(false);
        subHps[1].SetActive(false);
        subHps[2].SetActive(false);
        subHps[3].SetActive(false);

        for (int i = 0; i < subHp; i++)
        {
            subHps[i].SetActive(true);
        }

        if (!canBehave)        //�����̻� Ÿ�̸� �۵�
        {
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
