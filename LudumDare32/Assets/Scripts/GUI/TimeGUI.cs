using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeGUI : MonoBehaviour {

    [HideInInspector]
    public float t = 0f;

    Text time;
	// Use this for initialization
	void Start () {
        time = GetComponent(typeof(Text)) as Text;
        GameController.Instance.tg = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameController.Instance.isPlaying) return;

        t += Time.deltaTime;
        time.text = ((int)t).ToString();
	}
}
