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

    public List<Transform> lvls = new List<Transform>();

    public bool isPlaying = false;

    public Sprite[] deadScreen;

    public GameObject lvl1;
    public GameObject lvl2;
    GUIController gui;

    public TimeGUI tg;
    public float speed = 40f;

    float oldZones = 0;

    float oldZ;

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
        gui = FindObjectOfType<GUIController>();
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
            instance.RefreshMembers();
            Destroy(this.gameObject);
        }

        music = new List<AudioSource>(GetComponents<AudioSource>());
        oldZ = player.transform.position.z;
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) DelayRestartGame(0f);
        float diffZ = player.transform.position.z - oldZ;

        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z - diffZ);
        foreach (Transform t in lvls)
        {
            if(t != null)
                t.position = new Vector3(t.position.x, t.position.y, t.position.z - diffZ);
        }
        oldZ = player.transform.position.z;

        foreach (AudioSource item in music)
        {
            item.volume = 0.5f * Time.timeScale;
        }

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
            desiredTimeSacle = 0.8f / slowMoZones;
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

    public void AddPoint(int n)
    {
        score += n;
        AudioController.instance.PlayCoin();
        //gui.UpdateScore(n);
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
        yield return new WaitForSeconds(1.3f * seconds);
        
        RestartGame();
    }
}
