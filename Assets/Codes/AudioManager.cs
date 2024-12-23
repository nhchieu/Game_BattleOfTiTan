using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#nhac nen (Background Music)")]
    public AudioClip[] bgmClip;

    AudioSource bgmPlayer;

    [Header("#hieu ung (Sound Effect)")]
    public AudioClip[] sfxClip;
    public float sfxVolume;
    AudioSource sfxPlayer;

    private void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        //nhac nen
        GameObject bgmObject = new GameObject("BGM");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        //bgmPlayer.volume = bgmVolume;
        bgmPlayer.loop = true;



        //hieu ung am thanh
        GameObject sfxObject = new GameObject("BGM");
        sfxObject.transform.parent = transform;
        sfxPlayer = sfxObject.AddComponent<AudioSource>();
        sfxPlayer.playOnAwake = false;
        sfxPlayer.volume = sfxVolume;
    }

    public void BgmOn(int a, float b)
    {

        bgmPlayer.clip = bgmClip[a];
        bgmPlayer.volume = b;
        Debug.Log(bgmPlayer.volume);
        bgmPlayer.Play();
    }

    //ham hieu ung am thanh
    public void sfx(int a)
    {
        sfxPlayer.clip = sfxClip[a];
        sfxPlayer.Play();
    }
    public void ChanceVolume(float volume)
    {
        bgmPlayer.volume = volume;
    }
    public void PauseMusic()
    {
        bgmPlayer.Pause();
    }
    public void ResumeMusic()
    {
        bgmPlayer.UnPause();
    }

    public void StopMusic()
    {
        bgmPlayer.Stop();
        sfxPlayer.Stop();
    }


}
