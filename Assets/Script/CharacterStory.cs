using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStory : MonoBehaviour
{
    [SerializeField] private GameObject zhangfeiStory;
    [SerializeField] private GameObject caorenStory;

    private void Start()
    {
        StoryObjectOnOff(zhangfeiStory, true);
    }

    public void NextStory()
    {
        StoryObjectOnOff(zhangfeiStory, false);
        StoryObjectOnOff(caorenStory, true);
        BossController bossController = FindObjectOfType<BossController>();
        bossController.BossStory();
    }

    void StoryObjectOnOff(GameObject story, bool onoff)
    {
        story.SetActive(onoff);
    }
}
