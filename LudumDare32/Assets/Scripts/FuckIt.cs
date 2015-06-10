using UnityEngine;
using System.Collections;

public class FuckIt : MonoBehaviour {
    float t =10f;
	// Use this for initialization
	void Start () {
        GameController.Instance.lvls.Add(transform);
	}
	
	// Update is called once per frame
	void Update () {
        t -= Time.deltaTime;
        if (t <= 0f) Destroy(gameObject);
	}
}
