using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    float desiredTimeScale = 1.0f;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Explode(col.transform.position));

            desiredTimeScale = 0.1f;

            Debug.Log("asd");
            StartCoroutine(NormalTime());
        }

    }

    IEnumerator NormalTime()
    {
        yield return new WaitForSeconds(0.5f * Time.timeScale);
        desiredTimeScale = 1.0f;
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = Mathf.Lerp(Time.timeScale, desiredTimeScale, 0.1f);
        Time.fixedDeltaTime = 0.01f * Time.timeScale;
	}

    IEnumerator Explode(Vector3 position)
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < transform.childCount; i++)
        {            
            transform.GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(0.008f, position, 6.0f, 5.0f);
        }
    }
}
