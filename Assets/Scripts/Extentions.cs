using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorConvertor
{
    public static Vector3 ToUnityVector3(this System.Numerics.Vector3 systemVector3)
    {
        return new Vector3(systemVector3.X, systemVector3.Y, systemVector3.Z);
    }

    public static Vector3 ToUnityVector3(this System.Numerics.Vector2 systemVector2)
    {
        return new Vector3(systemVector2.X, systemVector2.Y, 0);
    }

    public static Vector2 ToUnityVector2(this System.Numerics.Vector2 systemVector3)
    {
        return new Vector2(systemVector3.X, systemVector3.Y);
    }
}
