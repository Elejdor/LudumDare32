using UnityEngine;
using System.Collections;

public class ScaleChildrenColliders : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (SphereCollider item in GetComponentsInChildren<SphereCollider>())
        {
            item.radius *= 0.4f;
        }
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
