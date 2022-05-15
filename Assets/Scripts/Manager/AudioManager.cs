using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixerGroup _master;
    [SerializeField] private AudioMixerGroup _music;
    [SerializeField] private AudioMixerGroup _sfx;

    public enum AudioChannel
    {
        Master,
        Music,
        SFX
    }

    public AudioMixerGroup FromAudioChannel(AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                return _master;
            case AudioChannel.Music:
                return _music;
            case AudioChannel.SFX:
                return _sfx;
            default:
                return null;
        }
    }

    public AudioSource PlaySound(AudioClip clip, AudioChannel audioChannel, bool loop = false, float volume = 0.5f)
    {
        if(!clip) return null;

        AudioSource source = CreateAudioSource(clip, audioChannel, loop, volume);
        source.Play();

        if (!loop)
            Destroy(source.gameObject, clip.length);

        return source;
    }

    private AudioSource CreateAudioSource(AudioClip clip, AudioChannel audioChannel, bool loop = false, float volume = 0.5f)
    {
        AudioSource source = new GameObject(clip.name, typeof(AudioSource)).GetComponent<AudioSource>();
        source.transform.parent = transform;
        source.clip = clip;
        source.volume = volume;
        source.outputAudioMixerGroup = FromAudioChannel(audioChannel);
        source.loop = loop;

        return source;
    }
}
