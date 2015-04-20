using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[SelectionBase]
public class DestructionAgregate : MonoBehaviour {
    [SerializeField]
    int x;

    bool scored = false;

    [SerializeField]
    int y;

    [SerializeField]
    int z;

    [SerializeField]
    float mass;
    float size;

    [SerializeField]
    Transform cubesParent;

    [SerializeField]
    GameObject solid;
    Collider col;

    public bool cubesOn { get; private set; }

    [SerializeField]
    GameObject prefab;

    List<CubeScript> cubes = new List<CubeScript>();
    Queue<CubeScript> remover = new Queue<CubeScript>();

    float desiredTimeScale = 1.0f;

    IEnumerator Explode(Vector3 position)
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(0))
                transform.GetChild(0).GetChild(i).GetComponent<Rigidbody>().AddExplosionForce(0.008f, position, 6.0f, 5.0f);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            StartCoroutine(Explode(col.transform.position));

            GameController.Instance.slowMoZones++;
            if (!scored)
            {
                GameController.Instance.AddPoint();
                scored = true;
            }
            
            
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GameController.Instance.slowMoZones--;
        }
    }

	// Use this for initialization
	void Start () {
        cubesOn = false;
        GenerateCubes();
        col = this.GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void DestroyInvisibleCubes(ref Plane[] frustumPlanes)
    {
        for (int i = 0; i < cubes.Count; i++)
        {            
            if (!cubes[i].destroyed)
                cubes[i].DestroyIfInvisible(ref frustumPlanes);
            else
                remover.Enqueue(cubes[i]);
        }

        for (int i = 0; i < remover.Count; i++)
        {            
            cubes.Remove(remover.Dequeue());
        }
    }
    

    public bool IsInRange(Transform playerTransform)
    {
        return Vector3.Magnitude(playerTransform.position - this.transform.position) < 18;
    }

    public void TurnOnCubes()
    {
        Destroy(solid);
        cubesParent.gameObject.SetActive(true);
        cubesOn = true;
    }

    void GenerateCubes()
    {
        size = prefab.transform.localScale.x;
        GameObject tmpGo;
        cubesParent.gameObject.SetActive(false);
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    if ((k == z - 1 || k == 0) || (i == x - 1 || i == 0) || (j == y - 1 || j == 0))
                    {
                        tmpGo = (GameObject)GameObject.Instantiate(prefab);
                        tmpGo.transform.parent = cubesParent;
                        tmpGo.transform.localPosition = new Vector3(i * size - size, j * size - size, k * size - size);
                        tmpGo.GetComponent<Rigidbody>().mass = mass;
                        cubes.Add(tmpGo.GetComponent<CubeScript>());
                    }

                }


            }
        }
    }
}
