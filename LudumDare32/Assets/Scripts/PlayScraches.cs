using UnityEngine;
using System.Collections;

public class PlayScraches : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("s");
        if (col.collider.tag == "Wall")
        {
            AudioController.instance.PlayScraches();
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.collider.tag == "Wall")
        {
            AudioController.instance.StopScraches();
        }
    }
}
