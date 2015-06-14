using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    GameObject target;

    bool dead = false;

    Vector3 offset;
    Vector3 velocityPercentages = Vector3.zero;

    [SerializeField]
    Vector3 maxOffsets;

    Vector3 speedOffset = Vector3.zero;
    Vector3 desiredSpeedOffset = Vector3.zero;

	// Use this for initialization
	void Start () {
        offset = this.transform.position - target.transform.position;
	}
	
    void FixedUpdate()
    {
        if (!dead)
        {
            desiredSpeedOffset.x = velocityPercentages.x * maxOffsets.x;
            desiredSpeedOffset.y = velocityPercentages.y * maxOffsets.y;
            desiredSpeedOffset.z = velocityPercentages.z * maxOffsets.z;

            speedOffset = Vector3.Lerp(speedOffset, desiredSpeedOffset, 0.05f);
            this.transform.position = offset - speedOffset + target.transform.position;
        }
        else
        {
            this.transform.Translate(new Vector3(0, 0, 2.0f * Time.deltaTime));
        }
    }

	// Update is called once per frame
	void Update () {
        if (!dead)
        {
            desiredSpeedOffset.x = velocityPercentages.x * maxOffsets.x;
            desiredSpeedOffset.y = velocityPercentages.y * maxOffsets.y;
            desiredSpeedOffset.z = velocityPercentages.z * maxOffsets.z;

            speedOffset = Vector3.Lerp(speedOffset, desiredSpeedOffset, 0.05f);
            this.transform.position = offset - speedOffset + target.transform.position;
        }
        else
        {
            this.transform.Translate(new Vector3(0, 0, 2.0f * Time.deltaTime));
        }

         
	}

    public void SetVelocityPercentage(Vector3 velocity)
    {
        velocityPercentages = velocity;
    }

    public void Die()
    {
        dead = true;
    }
}
