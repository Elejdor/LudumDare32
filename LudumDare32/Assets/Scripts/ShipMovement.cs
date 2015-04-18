﻿using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

    Vector3 movementForce = Vector3.zero;

    [SerializeField]
    GameObject modelPivot;

    [SerializeField]
    Vector2 maxLeaps;

    [SerializeField]
    Vector3 maxForces = Vector3.zero;

	InputWrapper iw;

    float sideLeap;
    float desiredSideLeap;

    float frontLeap;
    float desiredFrontLeap;

	Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody>();
		iw = InputWrapper.Instance;
        Debug.Log(iw);
	}
	
	// Update is called once per frame
	void Update () {
		HandleMovment();
	}
	
	void HandleMovment()
	{
        movementForce.x = iw.HorizontalAxis;
        movementForce.y = iw.VerticalAxis;
        movementForce.z = iw.Accelerate;

        Leap();

        movementForce.x *= maxForces.x;
        movementForce.y *= maxForces.y;
        movementForce.z *= maxForces.z;

        rb.AddForce(movementForce);        
	}

    /// <summary>
    /// do it before multiplying by scalars
    /// </summary>
    void Leap()
    {
        sideLeap = Mathf.Lerp(sideLeap, movementForce.x * maxLeaps.x, 0.3f);
        frontLeap = Mathf.Lerp(frontLeap, movementForce.y * maxLeaps.y, 0.3f);

        modelPivot.transform.rotation = Quaternion.EulerRotation(-frontLeap, 0, -sideLeap);
    }
}
