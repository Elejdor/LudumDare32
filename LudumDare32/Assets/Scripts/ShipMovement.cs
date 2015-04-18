using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	Vector3 forceDirection = Vector3.zero;
	float maxForce;
	float desiredForce;
	float horizontalForce;
	float verticalForce;
	InputWrapper iw;
	
	
	Rigidbody rb;
	
	// Use this for initialization
	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody>();
		iw = InputWrapper.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void HandleMovment()
	{
		horizontalForce = iw.HorizontalAxis;
		verticalForce = iw.VerticalAxis;
		
		forceDirection.x = horizontalForce;
		forceDirection.y = verticalForce;
		forceDirection.Normalize();
		
		rb.AddForce(forceDirection * (verticalForce + horizontalForce));
	}
}
