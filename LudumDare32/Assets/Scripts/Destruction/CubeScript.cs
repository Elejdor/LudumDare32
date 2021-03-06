﻿using UnityEngine;
using System.Collections;

public class CubeScript : MonoBehaviour {

    Collider col;
    public bool destroyed = false;
	// Use this for initialization
	void Start () {
        col = this.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyIfInvisible(ref Plane[] frustumPlanes)
    {        
        if (frustumPlanes.Length > 0 && col != null && !GeometryUtility.TestPlanesAABB(frustumPlanes, col.bounds))
        {
            destroyed = true;
            Destroy(this.gameObject);
        }
    }


}
