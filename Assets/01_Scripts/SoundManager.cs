using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // BGM 종류들
    public enum EBgm
    {
        BGM_CONNECTION,
        BGM_LOBBY,
        BGM_GAME
    }
    // SFX 종류들
    public enum ESfx
    {
        SFX_BUTTON,
        SFX_JUMP,

    }

    // bgm audio clip 담을 수 있는 배열
    public AudioClip[] bgms;
    // sfx audio clip 담을 수 있는 배열
    public AudioClip[] sfxs;

    // bgm을 플레이하는 AudioSource
    public AudioSource audioBgm;
    // sfx을 플레이하는 AudioSource
    public AudioSource audioSfx;

    // 나를 담을 static 변수
    public static SoundManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // BGM Play
    public void PlayBGM(EBgm bgmIdx)
    {
        audioBgm.clip = bgms[(int)bgmIdx];
        audioBgm.Play();
    }

    public void StopBGM()
    {
        audioBgm.Stop();
    }

    // SFX Play
    public void PlaySFX(ESfx sfxIdx)
    {
        // 플레이 할 sfx 설정
        audioSfx.PlayOneShot(sfxs[(int)sfxIdx]);
    }
}
