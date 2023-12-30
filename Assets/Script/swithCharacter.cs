using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swithCharacter : MonoBehaviour
{
    public GameObject text;

    void Start() 
    {
        
    }
    
   public void changeText() 
   {
        text.GetComponent<Text>().text = "장수변경";
   }
}
