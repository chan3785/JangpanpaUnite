using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static int health;
    public static int power;
    public static int speed;
    public static int reflex;
    public static int defence;
    public static int invincibility;
    public static bool zhangFei;
    public static bool caoRen;
    public static bool caoHong;
    public static bool zhangLiao;
    public static bool character5;
    public static bool character6;
    public static bool character7;
    
    // Start is called before the first frame update
    void Start()
    {
        zhangFei = true;
        caoRen = false; caoHong = false;
        zhangLiao= false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
