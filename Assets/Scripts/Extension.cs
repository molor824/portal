using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extension
{
    public static bool IsObjectVisible(this Camera cam, Renderer renderer)
    {
        return GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(cam), renderer.bounds);
    }
}
