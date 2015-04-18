using UnityEngine;
using System.Collections;

public class DeleteInvisible : MonoBehaviour {

    Rigidbody rb;
    MeshRenderer mr;

    Bounds bnds;
    GenerateVoxelBlock gvb;


	// Use this for initialization
	void Start () {
        rb = this.GetComponent<Rigidbody>();
        mr = this.GetComponent<MeshRenderer>();
        bnds = GetComponent<Collider>().bounds;
        gvb = transform.parent.gameObject.GetComponent<GenerateVoxelBlock>();
        
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void LateUpdate()
    {

        if (Input.GetKey(KeyCode.G))
        {

            if (!GeometryUtility.TestPlanesAABB(gvb.planes, bnds))
            {
                Destroy(gameObject);
            }
        }

    }
}
