using UnityEngine;
using System.Collections;

public class GenerateVoxelBlock : MonoBehaviour {

    [SerializeField]
    int x;

    [SerializeField]
    int y;

    [SerializeField]
    int z;

    [SerializeField]
    float mass;
    float size;

    [SerializeField]
    GameObject prefab;

	// Use this for initialization
	void Start () {
        size = prefab.transform.localScale.x;
        GameObject tmpGo;

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                for (int k = 0; k < z; k++)
                {
                    if ((k == z - 1 || k == 0) || (i == x - 1 || i == 0))
                    {
                        tmpGo = (GameObject)GameObject.Instantiate(prefab);
                        tmpGo.transform.parent = this.gameObject.transform;
                        tmpGo.transform.localPosition = new Vector3(i * size - size, j * size - size, k * size - size);
                        tmpGo.GetComponent<Rigidbody>().mass = mass;
                        //if (j % 2 != 0)
                        //{
                        //    tmpGo.layer = LayerMask.NameToLayer("Brothers");
                        //}
                        //else
                        //{
                        //    tmpGo.layer = LayerMask.NameToLayer("Brothers");
                        //}
                    }

                }

                
            }
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
