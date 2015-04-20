using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreGUI : MonoBehaviour {

    Text score;
	// Use this for initialization
	void Start () {
        score = GetComponent(typeof(Text)) as Text;
        GameController.Instance.score = 0;
	}
	
	// Update is called once per frame
	void Update () {
        score.text = GameController.Instance.score.ToString();
	}
}
