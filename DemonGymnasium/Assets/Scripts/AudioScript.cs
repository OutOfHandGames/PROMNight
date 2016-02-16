using UnityEngine;
using System.Collections;


public class AudioScript : MonoBehaviour {
    public AudioSource aSource;
    public AudioClip[] aClips;

    public float minVol = .95f;
    public float maxVol = 1;
    public float minPitch = .95f;
    public float maxPitch = 1.05f;


    float delayTimer;

    void Start()
    {
        if (aSource == null)
        {
            aSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (delayTimer > 0)
        {
            delayTimer = Mathf.MoveTowards(delayTimer, 0, Time.deltaTime);
            if (delayTimer <= 0)
            {
                playSound();
            }
        }
    }

    public void playSound()
    {
        aSource.Stop();
        aSource.Play();
    }

    public void setAudioClip(int clip)
    {
        if (clip < 0 || clip >= aClips.Length)
        {
            print(clip);
            throw new System.NullReferenceException();
        }
        aSource.clip = aClips[clip];
    }

    public void setVolume(float volume)
    {
        aSource.volume = volume;
    }

    public void setPitch(float pitch)
    {
        aSource.pitch = pitch;
    }

    public void setRandomVolume()
    {
        setVolume(Random.Range(minVol, maxVol));
    }

    public void setRandomPitch()
    {
        setPitch(Random.Range(minPitch, maxPitch));
    }

    public void setRandomClip()
    {
        setAudioClip(Random.Range(0, aClips.Length));
    }

    public void playRandomSound()
    {
        setAudioSourceRandom();

        playSound();
    }

    void setAudioSourceRandom()
    {
        setRandomPitch();
        setRandomVolume();
        setRandomClip();
    }

    public void playSoundDelay(float timeDelay)
    {
        if (timeDelay <= 0f)
        {
            this.delayTimer = .0000001f;
            print("The time is less than 0, please use playSound() instead.");
            return;
        }

        this.delayTimer = timeDelay;
        
    }

    public void playSoundDelayRandom(float timeDelay)
    {
        setAudioSourceRandom();
        playSoundDelay(timeDelay);
    }

    
}
