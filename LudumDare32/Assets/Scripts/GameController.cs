using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {

        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
