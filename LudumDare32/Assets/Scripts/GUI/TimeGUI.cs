using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeGUI : MonoBehaviour {

    public float t = 60f;

    Text time;
	// Use this for initialization
	void Start () {
        time = GetComponent(typeof(Text)) as Text;
        GameController.Instance.tg = this;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameController.Instance.isPlaying) return;

        if (t <= 0f)
        {
            time.text = 0.ToString();

            //DO SOMETHNG!!
            GameController.Instance.DelayRestartGame(2f);
        }

        t -= Time.deltaTime;
        time.text = ((int)t).ToString();

        
	}
}
