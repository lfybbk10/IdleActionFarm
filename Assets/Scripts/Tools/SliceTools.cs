using System.Collections.Generic;
using EzySlice;
using UnityEngine;
using Plane = EzySlice.Plane;

public static class SliceTools
{
    public static List<GameObject> Slice(this GameObject gameObject, Vector3 position, Material crossSectionMaterial)
    {
        MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();

        SlicedHull slicedHull = gameObject.Slice(position,Vector3.up,crossSectionMaterial);

        GameObject upperHull = slicedHull.CreateUpperHull();
        upperHull.GetComponent<MeshRenderer>().materials = meshRenderer.materials;
        GameObject lowerHull = slicedHull.CreateLowerHull();
        lowerHull.GetComponent<MeshRenderer>().materials = meshRenderer.materials;

        return new List<GameObject>(){upperHull,lowerHull};
    }
}
