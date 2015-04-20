using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGUI : MonoBehaviour {

    public Sprite kinect;
    public Sprite keyboard;

    Image image;
	// Use this for initialization
	void Start () {
        image = GetComponent(typeof(Image)) as Image;
        AudioController.instance.PlayIntro();
	}
	
	// Update is called once per frame
    void Update()
    {
        if (BodyManager.intance.isKinect)
            image.sprite = kinect;
        else
            image.sprite = keyboard;
        if (GameController.Instance.isPlaying)
        {
            image.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
