using UnityEngine;
using System.Collections;

public class WallTrigger : MonoBehaviour {

    public bool first;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        if (first)
            AudioController.instance.PlaySecond();
        else
            AudioController.instance.PlayFirst();
    }
}
