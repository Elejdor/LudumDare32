using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeGUI : MonoBehaviour {

    public float t = 70f;

    Text time;
	// Use this for initialization
	void Start () {
        time = GetComponent(typeof(Text)) as Text;
	}
	
	// Update is called once per frame
	void Update () {
        t -= Time.deltaTime;
        time.text = ((int)t).ToString();
	}
}
