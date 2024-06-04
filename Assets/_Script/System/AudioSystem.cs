using UnityEngine;

public class AudioSystem : Singleton<AudioSystem>
{
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private AudioSource musicSource;

    #region internal
    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    
    public void PlaySound(AudioClip clip,Vector3 pos,float volume)
    {
        soundSource.transform.position = pos;

        PlaySound(clip, volume);
    }

    public void PlaySound(AudioClip clip,float volume)
    {
        soundSource.PlayOneShot(clip,volume);
    }
    #endregion
}
