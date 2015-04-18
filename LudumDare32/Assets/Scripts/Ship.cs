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
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Die()
    {
        GameController.Instance.RestartGame();
    }
}
