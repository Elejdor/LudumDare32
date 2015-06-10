using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour {

    Vector3 movementForce = Vector3.zero;

    [SerializeField]
    public CameraFollow cameraFollow;

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
        GameController.Instance.isPlaying = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(!GameController.Instance.isPlaying)
        {
            HandlePauseInput();
            return;
        }
		HandleMovment();
	}

    void HandlePauseInput()
    {
        if (iw.Unpaused())
        {
            GameController.Instance.isPlaying = true;
            AudioController.instance.PlayFirst();
        }
    }
	void HandleMovment()
	{
        movementForce.x = iw.HorizontalAxis;
        movementForce.y = iw.VerticalAxis;
        movementForce.z = iw.Accelerate;

        Leap();
        cameraFollow.SetVelocityPercentage(movementForce);

        movementForce.x *= maxForces.x;
        movementForce.y *= maxForces.y;
        movementForce.z *= maxForces.z;

        //rb.AddForce(movementForce);// * Time.deltaTime * 25);
        rb.AddForce(movementForce * Time.deltaTime * 40);
	}

    /// <summary>
    /// do it before multiplying by scalars
    /// </summary>
    void Leap()
    {
        sideLeap = Mathf.Lerp(sideLeap, movementForce.x * maxLeaps.x, 0.3f * Time.timeScale);
        frontLeap = Mathf.Lerp(frontLeap, movementForce.y * maxLeaps.y, 0.3f * Time.timeScale);

        modelPivot.transform.rotation = Quaternion.EulerRotation(-frontLeap, 0, -sideLeap);
    }
}
