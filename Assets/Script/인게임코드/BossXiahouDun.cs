using System.Collections;
using System.Collections.Generic;
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
    }

    public void walk()
    {

        if (pat)
        {
            if (transform.position.x > 7)
            {


                shadow.SetActive(true);
                shadow.SendMessage("spawn");
                

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

    private void Awake()     
    {
        XiahouDunHP = 20;
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
            pat = true;
            walkBack = true;
        }
    }

    private void aniWalkEnd()
    {
        walkBack = false;
        anim.SetBool("walk", false);
    }
}
