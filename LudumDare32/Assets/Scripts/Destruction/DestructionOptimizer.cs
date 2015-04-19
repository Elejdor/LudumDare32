using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestructionOptimizer : MonoBehaviour {

    [SerializeField]
    List<DestructionAgregate> agregates = new List<DestructionAgregate>();

    [SerializeField]
    Transform playerTransform;
    Plane[] frustumPlanes;

    int currentIndex = 0;

	// Use this for initialization
	void Start () {
        agregates.AddRange(FindObjectsOfType<DestructionAgregate>());
	}
	
	// Update is called once per frame
	void Update () {
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        DestructionAgregate currentAgregate;
        currentIndex %= agregates.Count;

	    if (agregates.Count > 0)
        {
            bool operationDone = false;
            int tmpIndex = currentIndex;

            while (!operationDone)
            {

                currentAgregate = agregates[tmpIndex];

                if (currentAgregate.cubesOn == false)
                {
                    if (currentAgregate.IsInRange(playerTransform))
                    {
                        currentAgregate.TurnOnCubes();
                        operationDone = true;
                    }
                    else
                    {
                        tmpIndex++;
                        if (tmpIndex % agregates.Count == 0)
                            break;
                    }
                }
                else
                {
                    currentAgregate.DestroyInvisibleCubes(frustumPlanes);
                    operationDone = true;
                    if (currentAgregate.cubes.Count < 100)
                    {
                        agregates.Remove(currentAgregate);

                    }
                }
            }

        }

        currentIndex++;
	}
}
