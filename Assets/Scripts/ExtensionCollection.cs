
using UnityEngine;

public static class ExtensionCollection
{
    public static float RandomInRange(this Vector2 v2)
    {
        return Random.Range(v2.x, v2.y);
    }
}
