using UnityEngine;

public static class TransformExtensions
{
    public static Vector3 DirectionTo(this Transform transform, Vector3 destination)
    {
        return Vector3.Normalize(destination - transform.position);
    }

    public static Vector2 XandY(this Transform transform)
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
