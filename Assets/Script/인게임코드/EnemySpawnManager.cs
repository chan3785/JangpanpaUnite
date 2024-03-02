using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEditor;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] Swords = new GameObject[10];
    private bool[] swordState = new bool[10];
    public GameObject CaoRen;

    private float[] swordPosition = new float[10];
    //private float swordPosition;
    private int swordNum;
    public static int curSwordNum;

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
        swordState[swordNum++] = true;
        for (int i = 0; i < 10; i++)
        {
            Swords[i].SetActive(swordState[i]);
            Swords[i].SendMessage("setPosition", swordPosition[i]);
        }
        Debug.Log("SwordNum: " + swordNum);
    }

    private void setPosition()
    {
        
        Swords[swordNum++].SendMessage("setPosition",swordPosition);
    }

    private void Awake()
    {
        curSwordNum = 0;
        swordNum = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        CaoRen = GameObject.Find("Á¶ÀÎ (1)");
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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {

            swordDied();
        }
    }
}
