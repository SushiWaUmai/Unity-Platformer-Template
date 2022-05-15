using UnityEngine;

public class BackgroundMusicManager : Singleton<BackgroundMusicManager>
{
    [SerializeField] private AudioClip _backgroundMusic = null;

    private AudioSource _source;

    private void Start()
    {
        // This ternary operator is need to prevend the unassigned reference error.
        AudioClip clip = _backgroundMusic ? _backgroundMusic : null;
        _source = AudioManager.Instance.CreateAudioSource(clip, AudioManager.AudioChannel.Music, true);

        if(_backgroundMusic)
            _source.Play();
    }

    // pass null to stop audio
    public void UpdateBackgroundMusic(AudioClip clip)
    {
        if(!clip)
        {
            _source.Stop();
            return;
        }

        if(_source.clip != clip)
        {
            _source.Stop();
            _source.clip = clip;
            _source.Play();
        }
    }
}
