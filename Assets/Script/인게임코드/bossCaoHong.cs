using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public GameObject arrow1; // 1번공격 화살
    public GameObject arrow2; // 2번공격 화살
    public GameObject CaoHong;
    Vector3 position;

    public int walkSpeed;
    private bool walkBack;
    private bool canAtk;

    private int atk1Num, atk2Num, atk3Num;

    private int page;
    private float timer;
    private int randValue;


    private bool pat1, pat2, turn;

    private float patTimer;
    public float atkTimer;
    private float delay;

    public int parryProbability;

    public int distance, arrow_speed; // 화살 관련 변수들
    private float arrow_timer; // 화살이 발사된 후 소요된 시간을 기록
    private float shield_timer; // 방어버튼 눌리는 시간을 기록
    private float arrow1_approachingTime;
    private float arrow2_approachingTime; // 화살이 플레이어에게 도달하는 시간을 저장하는 변수로서 start함수에서 초기화 됨
    private bool isGuard; // 방어버튼이 눌러져 있으면 true가 됨
    private bool arrow1_shot; 
    private bool arrow2_shot; // 화살이 발사되는 순간ㄴ true가 되고 화살이 플레이어에게 도달하면 false가 됨
    public static bool arrow2_fall; // 불화살이 떨어지는 애니메이션이 끝났을때 true가 됨. 관련함수: Arrow.cs의 Arrow_Fall();
    private bool can_Parrying1;
    private bool can_Parrying2; //패링

    private int money_loss; // 조홍에게 한대 맞을때마다 플레이어가 잃는 재화의 양

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
            page = 3;
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
        //player.SendMessage("StatusEffect");
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
    private void decrease_atk3Num() // 각 atknum 변수들은 애니메이터의 변수들과 연결되어서 애니메이션 재생 횟수를 조정함
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
        //Debug.Log("shield: " + shield_timer);
    }

    private void guardOff()
    {
        isGuard = false;
    }

    public void atk1_prepare()
    {
        if (transform.position.x < 7.5)
        {
            transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
            canHurt = true;
            canAtk = true;
        }
        transform.position = new Vector3(7.5f, 0.5f, 0);
    }

    public void atk2_prepare()
    {
        if (transform.position.x < 0)
        {
            transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
            canHurt = true;
            canAtk = true;
        }
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
            anim.SetBool("can_attack", false); // 애니메이션의 can_attack이라는 bool변수로서 true가 아니면 공격 애니메이션이 작동 안함.
            activateArrowTimer();
            if(arrow1.transform.position.x > -6) // 플레이어의 위치까지 화살 이동
            {
            arrow1.transform.Translate(new Vector3(-arrow_speed * Time.deltaTime, 0, 0));
            }
            
            if(arrow1.transform.position.x <= -6) //arrow_timer > arrow1_approachingTime
            {
                arrow1_shot = false;
                if(isGuard) // 방어버튼이 눌렸을시
                {
                    if(arrow1_approachingTime - shield_timer > 0 && arrow1_approachingTime - shield_timer < 0.5) // 패링조건 만족할 경우 패링 함수 실행
                    {
                        can_Parrying1 = true; // 패링 함수는 update문에 있습니다.
                    }
                    else // 만족하지 않을 경우 방어 애니메이션 실행
                    {
                        player.SendMessage("Hurt", "nAtk");
                        player.SendMessage("Shield");
                        player.SendMessage("Shield");

                        Debug.Log("guard");
                        player.SendMessage("guard");
                        arrow_timer = 0;
                        shield_timer = 0;
                        arrow1.SetActive(false);
                        arrow1.transform.position = new Vector3(8.5f, 0.5f, 0);
                        anim.SetBool("can_attack", true);
                    }
                }
                else // 방어버튼이 안눌렸을시 플레이어의 hurt 함수 실행
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
        //player.SendMessage("parryOn");
        if(arrow1.transform.position.x < 6.5f) // 화살이 조홍의 위치까지 이동 
        {
            arrow1.GetComponent<SpriteRenderer>().flipX = false;
            arrow1.transform.Translate(new Vector3(arrow_speed * Time.deltaTime, 0, 0));
        }
        else // 위치까지 도달 후 조홍의 hurt 함수 실행
        {
            arrow_timer = 0;
            shield_timer = 0;
            arrow1.SetActive(false);
            arrow1.GetComponent<SpriteRenderer>().flipX = true;
            anim.SetBool("can_attack", true);
            anim.Play("hurt");
            hp -= 1;
            Debug.Log("Boss: " + hp);
            can_Parrying1 = false;
            page = 2;
        }
    }

    private void actAtk1()
    {
        player.SendMessage("Hurt", "nAtk");
        atkTimer = 0;
        if(Goods.money > 0)
        {
            Goods.money -= 1;
        }
    }

    public void atk1_arrow() // 화살 오브젝트를 활성화하는 함수로서 이 스크립트가 아니라 공격 애니메이션을 잘 보시면 이벤트로 이 함수가 있을겁니다.
    {
        arrow1.SetActive(true);
        arrow1_shot = true;
    }

    private void Atk2() // atk2도 atk1과 거의 같은 방식입니다.
    {
        anim.SetInteger("atk2", atk2Num);
        
        if(arrow2.activeSelf && arrow2_shot)
        {
            activateArrowTimer();
            if(arrow2.transform.position.x > -7.5)
            {
                arrow2.transform.Translate(new Vector3(-arrow_speed * Time.deltaTime, (arrow_speed/5) * Time.deltaTime, 0));
            }
            else
            {
                arrow2.GetComponent<Animator>().Play("fall");
                arrow2_shot = false;
                
                if(arrow2_fall) // arrow2_fall은 화살이 떨어지는 애니메이션이 끝났을때 true가 됩니다. Arrow 스크립트에서 true가 됨.
                {
                    if(isGuard)
                    {
                        float a = arrow2_approachingTime - shield_timer;
                        Debug.Log("arrow2 - shield: " + a);
                        if(arrow2_approachingTime - shield_timer > 0 && arrow2_approachingTime - shield_timer < 4)
                        {
                            can_Parrying2 = true;
                            arrow2_fall = false;
                        }
                        else
                        {
                            Debug.Log("guard");
                            player.SendMessage("guard");
                            arrow_timer = 0;
                            shield_timer = 0;
                            arrow2.SetActive(false);
                            arrow2.transform.position = new Vector3(0, 0, 0);
                            anim.SetBool("can_attack", true);
                            arrow2_fall = false;
                        }
                    }
                    else
                    {
                        actAtk2();
                        arrow_timer = 0;
                        shield_timer = 0;
                        arrow2.SetActive(false);
                        arrow2.transform.position = new Vector3(0, 0, 0);
                        anim.SetBool("can_attack", true);
                        arrow2_fall = false;
                    }
                }
            }
        }
    }

    private void atk2_parry()
    {
        Debug.Log("패링 함수 작동중");
        player.SendMessage("parryOn");
        arrow2.transform.position = new Vector3(0, 0, 0);
        arrow2.SetActive(false);
        if(!arrow1.activeSelf)
        {
            arrow1.transform.position = new Vector3(-9, 0.5f, 0);
        }
        arrow1.SetActive(true);

        if(arrow1.transform.position.x < 0)
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
            can_Parrying2 = false;
            if (Random.Range(0, 100) <= 40)
            {
                atk2Num = 1;
            }
            else
            {
                atk2Num = 0;
                anim.SetInteger("atk2", 0);

                page = 6;
            }
        }
    }

    private void actAtk2()
    {
        player.SendMessage("Hurt", "sAtk");
        atkTimer = 0;
        if(Goods.money > 0)
        {
            Goods.money -= 1;
        }
    }

    private void atk2_arrow()
    {
        arrow2.SetActive(true);
        arrow2_shot = true;
    }
    
    private void Atk3()
    {
        anim.SetInteger("atk3", atk3Num);
    }

    private void actAtk3()
    {
        player.SendMessage("Hurt", "nAtk");
        atkTimer = 0;
        if(Goods.money > 0)
        {
            Goods.money -= 1;
        }
        if (Random.Range(0, 100) <= 50)
        {
            atk3Num = 1;
        }
        else
        {
            atk3Num = 0;
            anim.SetInteger("atk3", 0);

            page = 4;
        }
    }

    private void Awake()  // ���� �ʱ�ȭ
    {
        hp = 20; 
        atkSpeed = 1;
        page = 1;
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
        arrow2_shot = false;
        can_Parrying1 = false;
        can_Parrying2 = false;
    }
    
    private void Start()
    {
        anim.speed = atkSpeed;  
        arrow1_approachingTime = 15.5f / arrow_speed;
        arrow2_approachingTime = 7.5f / arrow_speed;
        money_loss = 1;
        Goods.money = 10; // 재화 손실 테스트 용도로 임시로 초기화
    }

    void Update()
    {
        // Debug.Log("atk1: " + atk1Num);
        // Debug.Log("atk2: " + atk2Num);
        // Debug.Log("atk3: " + atk3Num);
        if (page == 2)
        {
            walk();
            anim.SetInteger("atk1", 0);
            atk1Num= 0;
            Debug.Log(page);
        }
        else
        {
                activateTimer();
                activateAtkTimer();

                Atk1();
                Atk2();
                Atk3();

                Debug.Log("isGuard: " + isGuard);

                if(can_Parrying1)
                {
                    Debug.Log("a");
                    atk1_parry();
                }

                if(can_Parrying2)
                {
                    atk2_parry();
                }

                if (atk1Num <= 0 && atk2Num <= 0 && atk3Num <= 0)
                {
                    activateTimer();
                    activateAtkTimer();

                    if (timer >= 3)
                    {
                        timer = 0;
                        if(page == 1)
                        {
                            atk1Num = 1;
                        }
                        if(page == 3)
                        {
                            anim.SetInteger("atk1", 0);
                            atk3Num = 1;
                            
                        }
                        if(page ==5)
                    {
                        atk2Num= 1;
                        
                    }
                        //atk2Num = Random.Range(0, 2);
                        //atk3Num = Random.Range(0, 2);
                    }
                    

                }   
                        if(page == 4)
                    {
                        canHurt = false;
                        if (transform.position.x < 0)
                        {

                            //anim.SetBool("walkBack", true);
                            transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
                        }
                        else
                        {
                            canAtk = false;
                            page = 5;
                        }
                    }
            if (page == 6)
            {
                canHurt = false;
                if (transform.position.x < 7.5)
                {

                    //anim.SetBool("walkBack", true);
                    transform.Translate(new Vector3(walkSpeed * Time.deltaTime, 0, 0));
                }
                else
                {
                    canAtk = false;
                    page = 1;
                }
            }
            Debug.Log("money: " + Goods.money);
        }
    }
}
