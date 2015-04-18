using UnityEngine;
using System.Collections;

using Windows.Kinect;

public class BodyManager : MonoBehaviour {

    private static BodyManager _instance;
    public static BodyManager intance { get { return _instance; } protected set { _instance = value; } }

    private KinectSensor _sensor;
    private BodyFrameReader _bodyFrameReader;
    private Body[] _bodyData;

    public Body[] bodyData { get { return _bodyData; } }
    public bool isKinect { get { return _sensor != null; } }
    
	// Use this for initialization
    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(this);

        _sensor = KinectSensor.GetDefault();
        if (_sensor != null)
        {
            _bodyFrameReader = _sensor.BodyFrameSource.OpenReader();

            if (!_sensor.IsOpen)
                _sensor.Open();
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if(_bodyFrameReader != null)
        {
            using (BodyFrame frame = _bodyFrameReader.AcquireLatestFrame())
            {
                if (frame != null)
                {
                    if (_bodyData == null) _bodyData = new Body[_sensor.BodyFrameSource.BodyCount];

                    frame.GetAndRefreshBodyData(_bodyData);
                }
            }
        }
	}
    void OnApplicationQuit()
    {
        if (_bodyFrameReader != null)
        {
            _bodyFrameReader.Dispose();
            _bodyFrameReader = null;
        }

        if (_sensor != null)
        {
            if (_sensor.IsOpen)
            {
                _sensor.Close();
            }

            _sensor = null;
        }
    }
}
