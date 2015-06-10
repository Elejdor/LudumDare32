using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestructionOptimizer : MonoBehaviour {

    static public List<DestructionAgregate> agregates = new List<DestructionAgregate>();

    [SerializeField]
    Transform playerTransform;
    Plane[] frustumPlanes;

    int currentIndex = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        frustumPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
        DestructionAgregate currentAgregate;
        currentIndex %= agregates.Count;

        foreach (DestructionAgregate item in agregates)
        {

            if (item == null) continue;
            if (item.IsInRange(playerTransform) && item.cubesOn == false)
            {
                item.TurnOnCubes();
            }
        }


        if (frustumPlanes.Length > 0 && agregates.Count > 0)
        {
            if (agregates[currentIndex].cubesOn)
                agregates[currentIndex].DestroyInvisibleCubes(ref frustumPlanes);            
        }

        currentIndex++;
	}

    static public void AddDA(DestructionAgregate da)
    {
        agregates.Add(da);
    }
    static public void RemoveDA(DestructionAgregate da)
    {
        agregates.Remove(da);
    }
}
