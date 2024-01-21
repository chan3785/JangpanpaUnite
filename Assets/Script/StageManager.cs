using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject stageNotice_1st;
    public static bool isNotice = false;
    public void StageNotice()
    {
        if (!DialogueManager.isDialogue && !isNotice)
        {
            isNotice = true;
            Time.timeScale = 0;
            stageNotice_1st.SetActive(true);
            StartCoroutine(ExecuteAfterTime(2.0f));
        }
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSecondsRealtime(time);  // 지정된 시간만큼 대기합니다.

        stageNotice_1st.SetActive(false);
        Time.timeScale = 1;
        CharacterStory characterStory = FindObjectOfType<CharacterStory>();
        characterStory.NextStory();
    }
}
