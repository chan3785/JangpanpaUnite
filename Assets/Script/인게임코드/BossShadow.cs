using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

public class BossShadow : MonoBehaviour
{
    public GameObject boss;
    private Animator anim;
    public GameObject player;
    public static bool isJump;
    private bool atk;
    public void spawn()
    {
        atk = false;
        //transform.Translate(new Vector3(-3 * Time.deltaTime, 3 * Time.deltaTime, 0));

    }

    public void Parried()
    {
        delete();
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("ZhangFei (1)");
        boss = GameObject.Find("ÇÏÈÄµ·");
        anim = GetComponent<Animator>();
        atk = false;
        isJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(transform.position.x > 2)
        {
            transform.Translate(new Vector3(-6 * Time.deltaTime, 6 * Time.deltaTime, 0));
        }
        else if(transform.position.x > -5)
        {
            transform.Translate(new Vector3(-6 * Time.deltaTime, -6 * Time.deltaTime, 0));
        }
        */
    }

    private void actAtk()
    {
        player.SendMessage("Hurt", "shadowAtk");
    }

    private void changeAtk()
    {
        isJump = false;
        atk = !atk;
        anim.SetBool("atk", atk);
    }
    private void delete()
    {
        boss.SendMessage("patEnd");

    }
}
