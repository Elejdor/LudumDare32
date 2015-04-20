using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {

    private static GUIController _instance;
    public static GUIController instance { get { return _instance; } set { _instance = value; } }

    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text timeText;

    void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(this);
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        float time = float.Parse(timeText.text);
        time -= Time.deltaTime;
        timeText.text = time.ToString();
	}
    public void UpdateScore(int points)
    {
        scoreText.text = points.ToString();
    }
}
