using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本主要用于管理音频的播放，
//          包括背景音乐和音效。
//==========================

public class AudioManager : Singleton<AudioManager>,IManager
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;

    #region internal
    public void Init()
    {
        Debug.Log("AudioManager Init");
    }

    internal void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    internal void PlaySound(AudioClip clip,Vector3 pos,float volume)
    {
        soundSource.transform.position = pos;

        PlaySound(clip, volume);
    }

    internal void PlaySound(AudioClip clip,float volume)
    {
        soundSource.PlayOneShot(clip,volume);
    }
    #endregion
}
