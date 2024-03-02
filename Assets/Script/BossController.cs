using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    
    DialogueManager bossDM;
    private void Start()
    {
        bossDM = FindObjectOfType<DialogueManager>();
    }
    public void BossStory()
    {
        StartCoroutine(ExecuteAfterTime(7.0f));
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        Debug.Log(Enemy.enemyKillCount);
        yield return new WaitForSecondsRealtime(time);  // 지정된 시간만큼 대기합니다.

        Enemy.enemyKillCount = 20;
        Debug.Log(Enemy.enemyKillCount);
        if (Enemy.enemyKillCount >= 20 && !DialogueManager.isDialogue)
        {
            Time.timeScale = 0;
            bossDM.ShowDialogue(GetComponent<InteractionEvent>().GetDialogue());
        }
    }
    
}

