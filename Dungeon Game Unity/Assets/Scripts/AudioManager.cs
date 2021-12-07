using UnityEngine.Audio;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        //FindObjectOfType<AudioManager>().Play("Enemysteps"); Currently glitched
        //FindObjectOfType<AudioManager>().Play("Playerdamage"); Doesn't work on incoming projectiles just melee
        //FindObjectOfType<AudioManager>().Play("UIbutton"); Not sure where code is for button clicks, works on ESC
        //FindObjectOfType<AudioManager>().Play("Chestopen"); Not sure on where code is / when to play it
        //FindObjectOfType<AudioManager>().Play("Lootsound"); Not sure on where code is / when to play it
        //FindObjectOfType<AudioManager>().Play("Trapdoor"); Not sure on where code is / when to play it
        //FindObjectOfType<AudioManager>().Play("Electric"); Not sure on where code is / when to play it
        //FindObjectOfType<AudioManager>().Play("Bossmusic"); Not sure on where code is / when to play it
    }

    public void Play (string name)
    {
        Sound s = System.Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = System.Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volume / 2f, s.volume / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitch / 2f, s.pitch / 2f));

        s.source.Stop();
    }
}
