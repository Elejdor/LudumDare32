using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

	Vector3 forceDirection = Vector3.zero;
	
	[SerializeField]
	float maxForce;
	
	
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
		HandleMovment();
	}
	
	void HandleMovment()
	{
		horizontalForce = iw.HorizontalAxis;
		verticalForce = iw.VerticalAxis;
		
		forceDirection.x = horizontalForce;
		forceDirection.y = verticalForce;
		Debug.Log(verticalForce);
		forceDirection.Normalize();
		
		rb.AddForce(forceDirection * (Mathf.Abs(verticalForce) + Mathf.Abs(horizontalForce)) * maxForce);
	}
}
