using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

public class GameController : MonoBehaviour {

    private static GameController instance;
    public int score = 0;

    public float slowMoZones = 0;
    public float desiredTimeSacle = 1.0f;
    public GameObject player;

    public bool isPlaying = false;

    public Sprite[] deadScreen;

    float oldZones = 0;

    UnityStandardAssets.ImageEffects.MotionBlur mainMotionBlur;
    ColorCorrectionCurves ccc;

    List<AudioSource> music;

    float maxBlur = 1.0f;
    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    void RefreshMembers()
    {
        instance.mainMotionBlur = Camera.main.gameObject.GetComponent<UnityStandardAssets.ImageEffects.MotionBlur>();
        instance.ccc = Camera.main.gameObject.GetComponent<ColorCorrectionCurves>();
        instance.player = FindObjectOfType<Ship>().gameObject;
        score = 0;
        slowMoZones = 0;
    }

    void Awake()
    {

        if (!instance)
        {
            instance = this;
            RefreshMembers();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            RefreshMembers();
            Destroy(this.gameObject);
        }

        music = new List<AudioSource>(GetComponents<AudioSource>());
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame

    void Update()
    {
        desiredTimeSacle = Mathf.Min(99f, Mathf.Max(desiredTimeSacle, 0f));
        if (slowMoZones < 0)
        {
            slowMoZones = 0;
        }

        if (slowMoZones == 0 || oldZones > slowMoZones)
        {
            desiredTimeSacle = 1.0f;
            Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeSacle, 0.05f);
        }
        else
        {
            desiredTimeSacle = 0.4f / slowMoZones;
            Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeSacle, 0.3f);
        }
        ccc.saturation = Time.timeScale;
        //mainMotionBlur.blurAmount = 1-Time.timeScale;
        Time.fixedDeltaTime = 0.01f * Time.timeScale;

        oldZones = slowMoZones;

        foreach (AudioSource audioSource in music)
            audioSource.pitch = Time.timeScale;
	}

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        desiredTimeSacle = 1.0f;
        slowMoZones = 0;
    }

    public void AddPoint(int n = 1)
    {
        score += n;
        AudioController.instance.PlayCoin();
    }

    public Sprite RandomSprite()
    {
        return deadScreen[Random.Range(0, deadScreen.Length)];
    }

    public void DelayRestartGame(float seconds)
    {
        StartCoroutine(WaitAndRestart(seconds));
    }

    IEnumerator WaitAndRestart(float seconds)
    {
        yield return new WaitForSeconds(0.7f * seconds);
        
        RestartGame();
    }
}
