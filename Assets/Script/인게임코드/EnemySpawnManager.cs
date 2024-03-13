using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private GameObject[] Enemies = new GameObject[10];

    public GameObject[] Swords = new GameObject[10];
    public GameObject[] Spears = new GameObject[10];
    private bool[] swordState = new bool[10];
    public GameObject CaoRen;

    private float[] swordPosition = new float[10];
    //private float swordPosition;
    private int swordNum;
    public static int curSwordNum;

   
    private int spearNum;
    private int curEnemyNum;
    private int EnemyNum;

    private void die()
    {

        
        Enemies[curEnemyNum++].SetActive(false);
        position(curEnemyNum);
        //curEnemyNum++;
        if(curEnemyNum == 10)
        {
            CaoRen.SetActive(true);
        }

    }

    private void Hurt(string type)
    {
        Enemies[curEnemyNum].SendMessage("Hurt", type);

    }
    private void position(int c)
    {
        for(int i= 0;i<EnemyNum-c ;i++)
        {
            Enemies[i+c].SendMessage("setPosition", -(10 - i * 2));
        }
    }
    private void swordDied()
    {

        

        swordState[curSwordNum] = false;
        Swords[curSwordNum].SetActive(false);
        curSwordNum++;
        for (int i = curSwordNum; i < 10; i++)
        {
            
            swordPosition[i] = -(10 - (i-curSwordNum) * 2);
            Swords[i].SendMessage("setPosition", swordPosition[i]);
        }
        if(curSwordNum == 10)
        {
            CaoRen.SetActive(true);
        }

    }
    private void spawnSword()
    {

        /*
        swordState[swordNum++] = true;
        for (int i = 0; i < 10; i++)
        {
            Swords[i].SetActive(swordState[i]);
            Swords[i].SendMessage("setPosition", swordPosition[i]);
        }
        Debug.Log("SwordNum: " + swordNum);
        */
        Enemies[EnemyNum] = Swords[swordNum++];
        Enemies[EnemyNum++].SetActive(true);
        position(curEnemyNum);
    }


    private void spawnSpear()
    {
        Enemies[EnemyNum] = Spears[spearNum++];
        Enemies[EnemyNum++].SetActive(true);
        position(curEnemyNum);


    }
    private void setPosition()
    {
        
        Swords[swordNum++].SendMessage("setPosition",swordPosition);
    }

    private void Awake()
    {
        curSwordNum = 0;
        swordNum = 0;
        spearNum = 0;
        curEnemyNum = 0; EnemyNum = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        //CaoRen = GameObject.Find("Á¶ÀÎ (1)");
        for (int i = 0; i < 10; i++)
        {
            Swords[i].SetActive(false);
            swordState[i] = false ;
            swordPosition[i] = -(10 - i*2);
        }

        
        
        CaoRen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            spawnSword();
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            spawnSpear();

        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {

            //swordDied();
            die();
        }
    }
}
