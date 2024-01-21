using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public AudioSource aud;

    public AudioClip[] audios  = new                                                                                                                                                            AudioClip[9];

    public GameObject[] hps = new GameObject[9];
    public GameObject[] subHps = new GameObject[9];
    public GameObject[] black = new GameObject[6];


    private int hp, atk, atkSpeed, specialSkillGage, subHp;

    public static bool is_atk, atk_type;

    private bool can_Parrying, is_guard;

    public static bool canBehave;
    public GameObject canBehavetxt;
    private float timer;


    public GameObject stone;
    Animator anim;
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

    public int Special
    {
        get { return specialSkillGage; }
        set { specialSkillGage = value; }
    }


    private void Awake()
    {


        anim = GetComponent<Animator>();
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
         //Debug.Log("1");
        if (collision.CompareTag("Enemy_atk"))
        {
            Hurt();
        }
        if (collision.CompareTag("enemyStone"))
        {
            Hurt();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        Hp = 3 + Settings.health;
        
        aud = gameObject.GetComponent<AudioSource>();

        hps[0] = GameObject.Find("HPBar (1)");
        hps[1] = GameObject.Find("HPBar (2)");
        hps[2] = GameObject.Find("HPBar (3)");
        hps[3] = GameObject.Find("HPBar (4)");
        hps[4] = GameObject.Find("HPBar (5)");
        hps[5] = GameObject.Find("HPBar (6)");
        hps[6] = GameObject.Find("HPBar (7)");
        hps[7] = GameObject.Find("HPBar (8)");
        hps[8] = GameObject.Find("HPBar (9)");



        SubHp = 0;
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


        for (int i = 8; i >=Hp; i--)
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

        for(int i = 0; i< Settings.health; i++)
        {
            black[i].SetActive(false);
        }

        is_atk = false;


        can_Parrying = false; is_guard = false;canBehave = true;


       
    }

    private void Debug(int health)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
        subHps[0].SetActive(false);
        subHps[1].SetActive(false);
        subHps[2].SetActive(false);
        subHps[3].SetActive(false);

        for (int i = 0; i < subHp; i++)
        {
            subHps[i].SetActive(true);
        }

        //Debug.Log(can_Parrying);

        if (!canBehave)
        {
            canBehavetxt.SetActive(true);
            timer += Time.deltaTime;
            if (timer > 2)
            {
                canBehave = true;
            }

        }
        else
        {
            canBehavetxt.SetActive(false);
            timer = 0;
        }
        //Debug.Log(can_Parrying);
    }

    private void Attack1()
    {
        
            anim.SetInteger("atkType", Random.Range(1, 3));
            anim.SetBool("Atk1", true);
            is_atk = true;
            atk_type = false;
            Invoke("ani_init", 0.5f);
            //Debug.Log(Atk);
            useSubHp();
        
    }

    private void Attack2()
    {
        
        anim.SetBool("Atk2_", true);
        is_atk = true;
        atk_type = true;
        Invoke("ani_init", 0.3f);
        
    }

    private void useSubHp()
    {
        if (SubHp > 0)
        {
            hp++;SubHp--;
        }
    }

    private void Activate_Attack2()
    {
        anim.SetBool("Atk2", true);
        useSubHp();
    }

    private void Kill()
    {

    }

    private void Hurt()
    {
       
        if (Enemy.is_atk)
        {
            if (is_guard)
            {
                if (can_Parrying)
                {
                    anim.SetBool("succeed", true);
                }
                else
                {
                    hp -= 1;
                    subHp += 1;
                    anim.SetBool("succeed", true);

                }
            }



            else
            {
                anim.SetBool("hurt", true);
                hp -= 1;
                subHp = 0;
                Invoke("ani_init", 0.1f);
                //Debug.Log("Player Hurt: "+hp);
                if (hp <= 0)
                {
                    Died();

                }

            }
            for (int i = 0; i < hp; i++)
            {
                hps[i].SetActive(true);
            }
            for (int i = hp; i < 3; i++)
            {
                hps[i].SetActive(false);
            }
            Enemy.is_atk = false;
        }
        if(enemy_Boss.isThrowing)
        {
            if (is_guard)
            {
                if (can_Parrying)
                {
                    stone.SendMessage("playerThrow");
                    anim.SetBool("succeed", true);
                }
                else
                {
                    hp -= 2;
                    subHp += 2;
                    anim.SetBool("succeed", true);

                }
            }



            else
            {
                anim.SetBool("hurt", true);
                hp -= 2;
                subHp = 0;
                Invoke("ani_init", 0.1f);
                //Debug.Log("Player Hurt: "+hp);
                if (hp <= 0)
                {
                    Died();

                }

            }
            for (int i = 0; i < hp; i++)
            {
                hps[i].SetActive(true);
            }
            for (int i = hp; i < 3; i++)
            {
                hps[i].SetActive(false);
            }
        }
    }
    private void Died()
    {
        SceneManager.LoadScene("MainScene");
    }

    private void Guard()
    {
        anim.SetBool("Shield", true);
        is_guard = true;
    }

    private void Guard_off()
    {
        anim.SetBool("Shield", false);
        is_guard = false;
    }
    private void parrying_On()
    {
        if (!can_Parrying)
        {
            can_Parrying = true;
            //Debug.Log("now_Parrying");
        }
    }
    private void parrying_Off()
    {
        if (can_Parrying)
        {
            can_Parrying = false;
            //Debug.Log("now_guard");
        }
    }


    private void Parrying()
    {

    }


    private void ani_init()
    {
        anim.SetBool("Atk2", false);
        anim.SetBool("Atk1", false);
        //anim.SetBool("Atk2_", false);
        anim.SetBool("hurt", false);
        anim.SetBool("succeed", false);

        is_atk = false;
    }

    private void aniAtk2_Init()
    {
        anim.SetBool("Atk2_", false);
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
            aud.Play();
        }
    }
    private void audioAtk2()
    {
        aud.clip = audios[3];
        if (!aud.isPlaying)
        {
            aud.Play();
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
