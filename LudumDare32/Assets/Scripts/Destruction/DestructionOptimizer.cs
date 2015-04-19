using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestructionOptimizer : MonoBehaviour {

    [SerializeField]
    DestructionAgregate[] agregates;

    [SerializeField]
    Transform playerTransform;
    Plane[] frustumPlanes;

    int currentIndex = 0;

	// Use this for initialization
	void Start () {
        agregates = FindObjectsOfType<DestructionAgregate>();
	}
	
	// Update is called once per frame
	void Update () {
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        DestructionAgregate currentAgregate;
        currentIndex %= agregates.Length;

        foreach (DestructionAgregate item in agregates)
        {

            if (item.IsInRange(playerTransform) && item.cubesOn == false)
            {
                item.TurnOnCubes();
            }
        }

        
	    if (agregates.Length > 0)
        {
            if (agregates[currentIndex].cubesOn)
                agregates[currentIndex].DestroyInvisibleCubes(ref frustumPlanes);            
        }

        currentIndex++;
	}
}
