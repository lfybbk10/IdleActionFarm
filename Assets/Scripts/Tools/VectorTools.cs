using UnityEngine;

public class VectorTools
{
    public static float Angle(Vector2 from, Vector2 to)
    {
        return (Mathf.Acos(Mathf.Clamp(Vector2.Dot(from.normalized, to.normalized), -1f, 1f)) * Mathf.Rad2Deg);
    }   
}
