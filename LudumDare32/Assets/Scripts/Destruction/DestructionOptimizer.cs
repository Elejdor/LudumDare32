using UnityEngine;
using System.Collections;

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

	    if (agregates.Length > 0)
        {

            currentAgregate = agregates[currentIndex];

            if (currentAgregate.cubesOn == false)
            {
                if (currentAgregate.IsInRange(playerTransform))
                {
                    currentAgregate.TurnOnCubes();
                }
            }
            else
            {
                currentAgregate.DestroyInvisibleCubes(frustumPlanes);
            }
        }

        currentIndex++;
	}
}
