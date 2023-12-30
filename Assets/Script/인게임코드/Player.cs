using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Player : MonoBehaviour
{
    public AudioSource aud;

    public AudioClip[] audios  = new AudioClip[9];

    public GameObject[] hps = new GameObject[3];
    public GameObject[] subHps = new GameObject[3];


    private int hp, atk, atkSpeed, specialSkillGage, subHp;

    public static bool is_atk, atk_type;

    private bool can_Parrying, is_guard;

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
        // Debug.Log("1");
        if (collision.CompareTag("Enemy_atk"))
        {
            Hurt();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();

        hps[0] = GameObject.Find("HPBar (1)");
        hps[1] = GameObject.Find("HPBar (2)");
        hps[2] = GameObject.Find("HPBar (3)");


        SubHp = 0;
        subHps[0] = GameObject.Find("SubHPBar (1)");
        subHps[1] = GameObject.Find("SubHPBar (2)");
        subHps[2] = GameObject.Find("SubHPBar (3)");
        subHps[0].SetActive(false);
        subHps[1].SetActive(false);
        subHps[2].SetActive(false);


        is_atk = false;


        can_Parrying = false; is_guard = false;

        Hp = 3;
    }

    // Update is called once per frame
    void Update()
    {
        subHps[0].SetActive(false);
        subHps[1].SetActive(false);
        subHps[2].SetActive(false);
        for (int i = 0; i < subHp; i++)
        {
            subHps[i].SetActive(true);
        }

        //Debug.Log(can_Parrying);
    }

    private void Attack1()
    {
        anim.SetInteger("atkType", Random.Range(1,3));
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
                    Debug.Log("parrying");
                    anim.SetBool("succeed", true);
                }
                else
                {
                    Debug.Log("guard");
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
    }
    private void Died()
    {
        Debug.Log("died");
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
        anim.SetBool("Atk2_", false);
        anim.SetBool("hurt", false);
        anim.SetBool("succeed", false);

        is_atk = false;
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
