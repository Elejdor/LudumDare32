using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
        if (instance == null)
		    instance = this;
		horizontalAxis = 0;
		verticalAxis = 0;
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");
    }
	
	#region InputWrappers
	public float HorizontalAxis
	{
		get
		{
			return 2f * horizontalAxis;
		}
	}
	
	public float VerticalAxis
	{
		get
		{            
            return -2f * verticalAxis;
		}
	}
	
	public float Accelerate
	{
		get
		{
            //if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
            //{
                return 2.0f;
            //}

			//return 0;
		}
	}
    public bool Unpaused()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonUp("Fire1"))
            return true;

        return false;
    }
	#endregion
}
