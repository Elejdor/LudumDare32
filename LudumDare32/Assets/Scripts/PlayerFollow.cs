using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {

    Transform player;
    Vector3 offset = Vector3.zero;

	// Use this for initialization
	void Start () {
        player = GameController.Instance.player.transform;
        offset.y = this.transform.position.y - player.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = new Vector3(transform.position.x, player.position.y + offset.y, this.transform.position.z);
	}
}
