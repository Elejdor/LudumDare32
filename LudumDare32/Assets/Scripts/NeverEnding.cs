using UnityEngine;
using System.Collections;

public class NeverEnding : MonoBehaviour {

    [SerializeField]
    bool lvl1;
    [SerializeField]
    Transform nextPosition;

    float childCount;
    bool wakeUpChild = true;
    int i = 0;
    int frameCount = 0;
	// Use this for initialization
	void Start () {
        childCount = transform.childCount;
        GameController.Instance.lvls.Add(transform);
	}
	
	// Update is called once per frame
	void Update () {
        ++frameCount;
        if (!wakeUpChild) return;
        if (frameCount % 10 != 0) return;

        if (i >= childCount) { wakeUpChild = false; return; }
        GameObject child = transform.GetChild(i).gameObject;
        child.SetActive(true);
        ++i;
	}

    void OnTriggerEnter(Collider col)
    {
        GameController.Instance.lvls.Remove(transform);

        Destroy(gameObject);
        if (lvl1)
            GameObject.Instantiate(GameController.Instance.lvl1, nextPosition.position, GameController.Instance.lvl1.transform.rotation);
        else
            GameObject.Instantiate(GameController.Instance.lvl2, nextPosition.position, GameController.Instance.lvl2.transform.rotation);
    }
}
