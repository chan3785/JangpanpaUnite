using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour
{
    void Update()
    {
        Debug.Log(Setting.isVib);
    }
    void Vibrate()
    {
        if (Setting.isVib)
        {
            Handheld.Vibrate();
            Debug.Log("진동");
        }
    }
}
