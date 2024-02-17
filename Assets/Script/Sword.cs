using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Enemy
{


    

    public static bool Parrying;
    private bool isAtk;
    private float atkTime;

    public GameObject[] hps = new GameObject[5];

    private bool can_Hurt;

    Animator ani;



    // Start is called before the first frame update

    public void walk()
    {
        transform.Translate(new Vector3(-walkSpeed * Time.deltaTime, 0, 0));
    }


    public void Attack()
    {

        isAtk = true;

        ani.SetBool("Atk", true);



    }

    void Start()
    {
        hp = 5;
        walkSpeed = 5;
        atkSpeed = 0;
        ani = GetComponent<Animator>();

        hps[0] = GameObject.Find("HP (1)");
        hps[1] = GameObject.Find("HP (2)");
        hps[2] = GameObject.Find("HP (3)");
        hps[3] = GameObject.Find("HP (4)");
        hps[4] = GameObject.Find("HP (5)");

        isAtk = false;
    }




    // Update is called once per frame
    void Update()
    {

        if (transform.position.x > -5)
        {
            walk();
            can_Hurt = false;

        }
        else
        {
            if (timer >= atkSpeed)
            {
                Debug.Log("atk");
                Attack();
                timer = 0;
                atkSpeed = Random.Range(1, 3);
                is_atk = true;
                can_Hurt = true;
            }
            else
            {
                //Debug.Log("end");
                if (timer >= 0.05f)
                {
                    ani.SetBool("Atk", false);
                }
                if (timer >= 1f)
                {
                    can_Hurt = false;
                }
                timer += Time.deltaTime;

            }
        }
        //print(ani.GetBool("Atk"));

        if (isAtk)
        {

            atkTime += Time.deltaTime;
            if (atkTime > 0.2)
            {
                Parrying = true;
                //Debug.Log("start");
            }
            if (atkTime > 1f)
            {
                Parrying = false;
                //Debug.Log("end");
                isAtk = false;
                atkTime = 0;
            }

        }
        //Debug.Log(Parrying);
    }


    
}