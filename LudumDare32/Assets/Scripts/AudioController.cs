using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    protected static AudioController _instance;
    public static AudioController instance { get { return _instance; } protected set { _instance = value; } }

    protected AudioSource audioSourceAmbient;
    protected AudioSource audioSourceFast;

    public AudioClip intro;
    public AudioClip first;
    public AudioClip second;

    public AudioClip death;
    public AudioClip coin;
    public AudioClip scraches;

    protected bool playSounds = true;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);
        audioSourceAmbient = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSourceFast = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        audioSourceAmbient.loop = true;
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.M))
        {
            playSounds = !playSounds;

            if (!playSounds)
                audioSourceAmbient.Stop();
            else
                audioSourceAmbient.Play();
        }
	}
    public void PlayIntro()
    {
        if (!playSounds)
            return;

        audioSourceAmbient.clip = intro;
        audioSourceAmbient.Play();
    }
    public void PlayFirst()
    {
        if (!playSounds)
            return;
        audioSourceAmbient.clip = first;
        audioSourceAmbient.Play();
    }
    public void PlaySecond()
    {
        if (!playSounds)
            return;
        audioSourceAmbient.clip = second;
        audioSourceAmbient.Play();
    }

    public void PlayDeath()
    {
        if (!playSounds)
            return;
        audioSourceAmbient.Stop();
        audioSourceFast.clip = death;
        audioSourceFast.Play();
    }
    public void PlayCoin()
    {
        if (!playSounds)
            return;
        audioSourceFast.clip = coin;
        audioSourceFast.Play();
    }
    public void PlayScraches()
    {
        if (!playSounds)
            return;
        audioSourceFast.clip = scraches;
        audioSourceFast.Play();
        audioSourceFast.loop = true;
    }
    public void StopScraches()
    {
        if (!playSounds)
            return;
        audioSourceFast.Stop();
        audioSourceFast.loop = false;
    }
}
