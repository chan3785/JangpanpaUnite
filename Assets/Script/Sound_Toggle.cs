using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting_Toggle : MonoBehaviour
{
    public static GameObject button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Setting.button_sound) //버튼 클릭음
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }
}
