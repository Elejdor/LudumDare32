using UnityEngine;
using System.Collections;

public class Ship : MonoBehaviour {


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Die();
        }
    }
	// Use this for initialization
	void Start () {
        GameController.Instance.player = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Die()
    {        
        this.GetComponent<ShipMovement>().cameraFollow.Die();
        GameController.Instance.DelayRestartGame(1);
        Destroy(this.gameObject);
    }

}
