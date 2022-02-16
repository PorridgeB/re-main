using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Formations
{
    public static Vector2 Circle(int index, int members, float radius)
    {
        var angle = index * (2 * Mathf.PI / members);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }

    public static Vector2 Rectangle(int index, int width, int height, float size)
    {
        return new Vector2(index / width, (index % height) - height / 2f) * size;
    }

    public static Vector2 Triangle(int index, int members, float length)
    {
        return new Vector2();
    }
}
