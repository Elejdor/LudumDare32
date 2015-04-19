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
        if (!BodyManager.intance.isKinect)
        {
            horizontalAxis = Input.GetAxisRaw("Horizontal");
            verticalAxis = Input.GetAxisRaw("Vertical");
            //Debug.Log(verticalAxis);
        }
        else if (BodyManager.intance.isKinect && BodyManager.intance.bodyData != null)
        {
            float dY = KinectHandDelta().y;
            //dY = Mathf.Clamp(dY, -1f, 1f);
            horizontalAxis = Mathf.Clamp(dY * 10f, -2f, 2f);
            verticalAxis = KinectHandHeight();
            verticalAxis = Mathf.Clamp(Mathf.Pow(verticalAxis + 0.1f, 3) * 40f, -2f, 2f);
        }
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
            if (!BodyManager.intance.isKinect)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    return 1.0f;
                }
            }
            else if (BodyManager.intance.isKinect && BodyManager.intance.bodyData != null)
            {
                float depth = KinectHandDepth();
                return (Mathf.Pow(depth, 3) + Mathf.Pow(depth, 5)) * 2;
            }
			return 0;
		}
	}

    protected Vector3 KinectHandDelta()
    {
        Windows.Kinect.CameraSpacePoint leftHand;
        Windows.Kinect.CameraSpacePoint rightHand;
        Dictionary<Windows.Kinect.JointType, Windows.Kinect.Joint> joints;
        Vector3 delta;
        joints = BodyManager.intance.bodyData[0].Joints;
        leftHand = joints[Windows.Kinect.JointType.HandLeft].Position;
        rightHand = joints[Windows.Kinect.JointType.HandRight].Position;

        delta.x = leftHand.X - rightHand.X;
        delta.y = leftHand.Y - rightHand.Y;
        delta.z = leftHand.Z - rightHand.Z;

        return delta;
    }
    protected float KinectHandHeight()
    {
        Windows.Kinect.CameraSpacePoint leftHand;
        Windows.Kinect.CameraSpacePoint rightHand;
        Windows.Kinect.CameraSpacePoint shoulderPosition;
        Dictionary<Windows.Kinect.JointType, Windows.Kinect.Joint> joints;
        joints = BodyManager.intance.bodyData[0].Joints;
        leftHand = joints[Windows.Kinect.JointType.HandLeft].Position;
        rightHand = joints[Windows.Kinect.JointType.HandRight].Position;
        shoulderPosition = joints[Windows.Kinect.JointType.SpineShoulder].Position;

        return ((leftHand.Y + rightHand.Y) / 2f) - shoulderPosition.Y;
    }
    protected float KinectHandDepth()
    {
        Windows.Kinect.CameraSpacePoint leftHand;
        Windows.Kinect.CameraSpacePoint rightHand;
        Windows.Kinect.CameraSpacePoint shoulderPosition;
        Dictionary<Windows.Kinect.JointType, Windows.Kinect.Joint> joints;
        joints = BodyManager.intance.bodyData[0].Joints;
        leftHand = joints[Windows.Kinect.JointType.HandLeft].Position;
        rightHand = joints[Windows.Kinect.JointType.HandRight].Position;
        shoulderPosition = joints[Windows.Kinect.JointType.SpineShoulder].Position;
        float value = shoulderPosition.Z - ((leftHand.Z + rightHand.Z) / 2f);
        value -= 0.01f;
        return value;
    }
	#endregion
}
