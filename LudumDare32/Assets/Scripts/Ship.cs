using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ship : MonoBehaviour {

    [SerializeField]
    GameObject solid;
    [SerializeField]
    GameObject parts;

    DyingEffect de;

    public Image dieImage;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Die();
        }
    }
	// Use this for initialization
	void Start () {
        GameController.Instance.player = this.gameObject;
        de = parts.GetComponent<DyingEffect>();
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Die()
    {        
        this.GetComponent<ShipMovement>().cameraFollow.Die();
        //GameController.Instance.DelayRestartGame(1);
        parts.SetActive(true);
        Destroy(solid);
        de.SetVelocity(this.GetComponent<Rigidbody>().velocity);
        dieImage.gameObject.SetActive(true);
        dieImage.sprite = GameController.Instance.RandomSprite();
        AudioController.instance.PlayDeath();
    }

}
