using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    
    DialogueManager theDM;
    void Start()
    {
        theDM = FindObjectOfType<DialogueManager>();

        if (SceneManager.GetActiveScene().name == "GameScene" && DialogueManager.isOpening)
        {
            Time.timeScale = 0;
            theDM.ShowDialogue(GetComponent<InteractionEvent>().GetDialogue());
        }
    }
    
}