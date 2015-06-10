using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIController : MonoBehaviour {



    float displayedScore = 0;
    int desiredScore = 0;

    [SerializeField]
    Text scoreText;


    void Awake()
    {
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = ((int)displayedScore).ToString();
        displayedScore = Mathf.Lerp(displayedScore, desiredScore, 0.3f);
	}

    public void UpdateScore(int addPoints)
    {
        desiredScore = GameController.Instance.score;
    }

    IEnumerator WaitAndAddScore()
    {
        yield return new WaitForSeconds(1);
        scoreText.text = GameController.Instance.score.ToString();
    }
}
