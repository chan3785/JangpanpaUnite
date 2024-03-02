using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class CaoCao : MonoBehaviour
{

    private int hp;
    public float atkSpeed;

    public AudioClip[] audios = new AudioClip[9];
    public GameObject[] hps = new GameObject[5];

    public static bool canBehave;
    public Animator anim;

    private bool canHurt;

    private AudioSource aud;

    public GameObject player,my;

    private bool canAtk;
    public float walkSpeed;

    private float atkTime,shieldTimer,position;
    public GameObject enemySpawn;

    private void setPosition(float pos)
    {
        position = pos;
    }
    private void Atk()
    {
        anim.SetBool("Atk", true);
        
    }
    private void actAtk()
    {
        player.SendMessage("Hurt", "swordAtk");
    }
    public void setAtkTimer()
    {
        atkTime = Random.Range(1, 4);
        shieldTimer = 0;
    }
    public void Hurt(string type)
    {
        if (canHurt)
        {
            if (shieldTimer <= 1)
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
            else
            {
                shield();
            }
        }
    }


    public void Damage(int dam)
    {
        aniInit();
        anim.Play("CaoCao_Hurt");
        hp -= dam;
        Debug.Log("Sword: " +  hp);
        for (int i = hp; i < 5; i++)
        {
            hps[i].SetActive(false);
        }
        if (hp <= 0)
        {
            Die();
        }
    }
    public void shield()
    {
        aniInit();
        anim.Play("CaoCao_shield");
        Debug.Log("Sword shield");
    } 

    private void Die()
    {
        

        enemySpawn.SendMessage("swordDied");
    }
    public void walk()
    {


        if (transform.position.x > position)
        {
            transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
            anim.SetBool("Walk", true);


        }
        else
        {
            anim.SetBool("Walk", false);
            if (transform.position.x <= -10)
            {
                
                canHurt = true;
                canAtk = true;
            }
        }


    }



    // Start is called before the first frame update
    private void Awake()
    {
        hp = 5;
        atkSpeed = 1;

        anim = GetComponent<Animator>();
        aud = gameObject.GetComponent<AudioSource>();
        canHurt = false;
        

        //canAtk = false;
   
        canAtk = false;
        player = GameObject.Find("ZhangFei (1)");
        my = GameObject.Find("CaoCao(Sword)");
        atkTime = 0;
        for(int i = 0;i<5;i++)
        {
            hps[i].SetActive(true);
        }
        position = -10;
    }
    void Start()
    {
        anim.speed = atkSpeed;
        enemySpawn = GameObject.Find("SpawnManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!canAtk)
        {
            walk();
        }
        else
        {
            if(atkTime <= 0)
            {
                Atk(); 
            }
            else
            {
                atkTime -= Time.deltaTime;
                shieldTimer += Time.deltaTime;
            }
        }
    }

    private void aniAtkInit()
    {
        anim.SetBool("Atk", false);
    }
    private void aniShieldInit()
    {
        anim.SetBool("Shield", false);
    }
    private void aniInit()
    {
        anim.SetBool("Atk", false); anim.SetBool("Shield", false);
    }
}
