using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
public class SoundManager : MonoBehaviour
{
    public AudioSource bgm;
    public static SoundManager instance;

    [SerializeField] Sound[] effectSounds;
    [SerializeField] AudioSource[] effectPlayer;
    [SerializeField] Sound[] bgmSounds;
    [SerializeField] AudioSource bgmPlayer;
    void Awake()
    {
        var soundManagers = FindObjectsOfType<SoundManager>();
        if (soundManagers.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void PlayBGM(string p_name)
    {
        for (int i = 0; i < bgmSounds.Length; i++)
        {
            if (p_name == bgmSounds[i].name)
            {
                bgmPlayer.clip = bgmSounds[i].clip;
                bgmPlayer.Play();
                return;
            }
        }
        Debug.LogError(p_name + "�� �ش��ϴ� bgm�� ����");
    }
    void Start()
    {
        //bgm.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void StopBGM()
    {
        bgmPlayer.Stop();
    }

    void PauseBGM()
    {
        bgmPlayer.Pause();
    }

    void unPauseBGM()
    {
        bgmPlayer.UnPause();
    }

    void PlayEffectSound(string p_name)
    {
        for (int i = 0; i < effectSounds.Length; i++)
        {
            if (p_name == effectSounds[i].name)
            {
                for (int j = 0; j < effectPlayer.Length; j++)
                {
                    if (!effectPlayer[j].isPlaying)
                    {
                        effectPlayer[j].clip = effectSounds[i].clip;
                        effectPlayer[j].Play();
                        return;
                    }
                }
                Debug.LogError("��� ȿ���� �÷��̾ ������Դϴ�.");
                return;
            }
        }
        Debug.LogError(p_name + "�� �ش��ϴ� ȿ������ �����ϴ�.");
    }

    void StopAllEffectSound()
    {
        for (int i = 0; i < effectPlayer.Length; i++)
        {
            effectPlayer[i].Stop();
        }
    }

    /// p_type : 0 -> ��� ���
    /// p_type : 1 -> ȿ���� ���
    public void PlaySound(string p_name, int p_type)
    {
        if (p_type == 0) PlayBGM(p_name);
        else if (p_type == 1) PlayEffectSound(p_name);
    }
}
