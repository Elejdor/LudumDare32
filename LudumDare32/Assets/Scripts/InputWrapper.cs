using UnityEngine;
using System.Collections;

public class InputWrapper : MonoBehaviour {
	
	private float horizontalAxis;
	private float verticalAxis;
	private static InputWrapper instance;
	
	public static InputWrapper Instance
	{
		get
		{
			return instance;
		}
	}
	
	void Awake()
	{
		instance = this;
		horizontalAxis = 0;
		verticalAxis = 0;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		horizontalAxis = Input.GetAxisRaw("Horizontal");
		verticalAxis = Input.GetAxisRaw("Vertical");
	}
	
	#region InputWrappers
	public float HorizontalAxis
	{
		get
		{
			return horizontalAxis;
		}
	}
	
	public float VerticalAxis
	{
		get
		{
			return verticalAxis;
		}
	}
	
	public float Accelerate
	{
		get
		{
			if (Input.GetKey(KeyCode.Space))
			{
				return 1.0f;
			}
			
			return 0;
		}
	}
	#endregion
}
