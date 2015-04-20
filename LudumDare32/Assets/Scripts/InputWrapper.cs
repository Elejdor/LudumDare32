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
            horizontalAxis = KinectHandDelta();
            horizontalAxis = Mathf.Clamp((Mathf.Pow(horizontalAxis, 2) * Mathf.Sign(horizontalAxis) + Mathf.Pow(horizontalAxis, 5)) * (2.2f), -2f, 2f);
            verticalAxis = KinectHandHeight();
            verticalAxis = Mathf.Clamp(verticalAxis * 3, -2f, 2f);
        }
    }
	
	#region InputWrappers
	public float HorizontalAxis
	{
		get
		{
			return 2.0f * horizontalAxis;
		}
	}
	
	public float VerticalAxis
	{
		get
		{            
            return -2.0f * verticalAxis;
		}
	}
	
	public float Accelerate
	{
		get
		{
            if (!BodyManager.intance.isKinect)
            {
                if (Input.GetKey(KeyCode.Space) || Input.GetButton("Fire1"))
                {
                    return 2.0f;
                }
            }
            else if (BodyManager.intance.isKinect && BodyManager.intance.bodyData != null)
            {
                float depth = KinectHandDepth();

                return (depth * 0.01f + Mathf.Pow(depth, 7) * 20);
            }

			return 0;
		}
	}
    public bool Unpaused()
    {
        if (Input.GetKeyDown(KeyCode.Space) || IsOK() || Input.GetButtonUp("Fire1"))
            return true;

        return false;
    }
    public Vector3 HeadVector()
    {
        if (BodyManager.intance.isKinect && BodyManager.intance.bodyData != null)
        {
            Windows.Kinect.CameraSpacePoint head;
            Dictionary<Windows.Kinect.JointType, Windows.Kinect.Joint> joints;
            joints = BodyManager.intance.bodyData[0].Joints;
            head = joints[Windows.Kinect.JointType.Head].Position;
            return new Vector3(head.X, head.Y, head.Z);
        }
        return Vector3.zero;
    }
    protected float KinectHandDelta()
    {
        Windows.Kinect.CameraSpacePoint leftHand;
        Windows.Kinect.CameraSpacePoint rightHand;
        Dictionary<Windows.Kinect.JointType, Windows.Kinect.Joint> joints;
        joints = BodyManager.intance.bodyData[0].Joints;
        leftHand = joints[Windows.Kinect.JointType.HandLeft].Position;
        rightHand = joints[Windows.Kinect.JointType.HandRight].Position;

        return leftHand.Y - rightHand.Y;;
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

        Vector3 lh;
        lh.x = leftHand.X;
        lh.y = leftHand.Y;
        lh.z = leftHand.Z;
        Vector3 rh;
        rh.x = rightHand.X;
        rh.y = rightHand.Y;
        rh.z = rightHand.Z;
        Vector3 s;
        s.x = shoulderPosition.X;
        s.y = shoulderPosition.Y;
        s.z = shoulderPosition.Z;

        float value = Mathf.Max((s - lh).magnitude, (s - rh).magnitude);
        value -= 0.01f;
        return value;
    }
    protected bool IsOK()
    {
        if (BodyManager.intance.isKinect && BodyManager.intance.bodyData != null)
        {
            bool right = BodyManager.intance.bodyData[0].HandRightState.Equals(Windows.Kinect.HandState.Lasso);
            bool left = BodyManager.intance.bodyData[0].HandLeftState.Equals(Windows.Kinect.HandState.Lasso);

            return right && left;
        }

        return false;
    }
	#endregion
}
