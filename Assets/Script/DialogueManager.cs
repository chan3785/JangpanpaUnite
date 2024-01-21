using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    [SerializeField] GameObject zhangfei, caocao_Army, caoren;
    [SerializeField] Text txt_Dialogue;
    [SerializeField] Text txt_Name;
    [SerializeField] Button nextButton;
    bool isButtonPressed = false;

    Dialogue[] dialogues;

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    public static bool isDialogue = false;
    bool isNext = false; // 특정 키 입력 대기.
    public static bool isOpening = true;

    int lineCount = 0; // 대화 카운트
    int contextCount = 0; // 대사 카운트

    private void Start()
    {
        nextButton.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        isButtonPressed = true;
    }

    public bool IsButtonPressed()
    {
        if (isButtonPressed)
        {
            isButtonPressed = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Update()
    {
        if (isDialogue)
        {
            if (isNext)
            {
                if (isButtonPressed)
                {
                    isNext = false;
                    isButtonPressed = false;
                    txt_Dialogue.text = "";
                    if (++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(TypeWritter());
                    }
                    else
                    {
                        contextCount = 0;
                        if (++lineCount < dialogues.Length)
                        {
                            StartCoroutine(TypeWritter());
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }
    }
    public bool getDialogued()
    {
        if (isDialogue == false)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public void ShowDialogue(Dialogue[] para_dialogues)
    {
        if (Setting.isStory)
        {
            if (isDialogue) return;

            isDialogue = true;
            txt_Dialogue.text = "";
            txt_Name.text = "";
            dialogues = para_dialogues;
            StartCoroutine(TypeWritter());
        }
    }

    void EndDialogue()
    {
        isDialogue = false;
        isButtonPressed = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        txt_Name.text = "";
        SettingUI(false);
        CharacterSet(zhangfei, "장비");                // 캐릭터 키고 끄는 거
        CharacterSet(caoren, "조인");
        Time.timeScale = 1;

        if (isOpening)
        {
            isOpening = false;
            StageManager stageManager = FindObjectOfType<StageManager>();
            stageManager.StageNotice();
        }

    }

    IEnumerator TypeWritter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("`", ",");

        txt_Name.text = dialogues[lineCount].name;
        CharacterSet(zhangfei, "장비");                // 캐릭터 키고 끄는 거
        CharacterSet(caoren, "조인");
        for (int i = 0; i < t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSeconds(textDelay);
        }

        isNext = true;
        yield return null;
    }
    void CharacterSet(GameObject character, string name)
    {
        if (txt_Name.text == name)
        {
            character.SetActive(true);
        }
        else
        {
            character.SetActive(false);
        }
    }


    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
    }

}
