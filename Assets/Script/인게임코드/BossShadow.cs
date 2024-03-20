using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

public class BossShadow : MonoBehaviour
{
    public GameObject boss;
    private Animator anim;
    private bool atk;
    public void spawn()
    {
        atk = false;
        //transform.Translate(new Vector3(-3 * Time.deltaTime, 3 * Time.deltaTime, 0));

    }

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("ÇÏÈÄµ·");
        anim = GetComponent<Animator>();
        atk = false;
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

    private void changeAtk()
    {
        atk = !atk;
        anim.SetBool("atk", atk);
    }
    private void delete()
    {
        boss.SendMessage("patEnd");

    }
}
