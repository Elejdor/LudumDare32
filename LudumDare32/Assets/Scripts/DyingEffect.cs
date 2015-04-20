using UnityEngine;
using System.Collections;

public class DyingEffect : MonoBehaviour {
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

    public void SetVelocity(Vector3 vel)
    {
        StartCoroutine(Detach(vel));
    }

    IEnumerator Detach(Vector3 vel)
    {
        yield return new WaitForEndOfFrame();
        GameController.Instance.slowMoZones = 2;
        foreach (Rigidbody item in GetComponentsInChildren<Rigidbody>())
        {
            item.transform.parent = null;
            item.velocity = vel;
            //item.AddForce(100*vel);
            //item.AddExplosionForce(10, this.transform.position - new Vector3(0,0, -10), 100);
        }
    }
}
