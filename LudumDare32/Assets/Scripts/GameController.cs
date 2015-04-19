using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public int slowMoZones = 0;
    public float desiredTimeSacle = 1.0f;
    public GameObject player;

    UnityStandardAssets.ImageEffects.MotionBlur mainMotionBlur;
    ColorCorrectionCurves ccc;

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

        
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (slowMoZones == 0)
        {
            desiredTimeSacle = 1.0f;
            Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeSacle, 0.05f);
        }
        else
        {
            desiredTimeSacle = 1.0f / slowMoZones;
            Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeSacle, 0.1f);
        }
        ccc.saturation = Time.timeScale;
        //mainMotionBlur.blurAmount = 1-Time.timeScale;
        Time.fixedDeltaTime = 0.01f * Time.timeScale;
	}

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        desiredTimeSacle = 1.0f;
        slowMoZones = 0;
    }

    public void DelayRestartGame(float seconds)
    {
        StartCoroutine(WaitAndRestart(seconds));
    }


    IEnumerator WaitAndRestart(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        RestartGame();
    }
}
