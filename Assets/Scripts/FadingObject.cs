using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingObject : MonoBehaviour
{
    public List<Renderer> Renderers = new List<Renderer>();
    public Vector3 Position;
    public List<Material> Materials = new List<Material>();
    [HideInInspector]
    public float initialAlpha;

    private void Awake()
    {
        Position = transform.position;

        if(Renderers.Count == 0)
        {
            Renderers.AddRange(GetComponentsInChildren<Renderer>());
        }

        foreach(Renderer renderer in Renderers)
        {
            Materials.AddRange(renderer.materials);
        }

        initialAlpha = Materials[0].color.a;
    }

    public bool Equals(FadingObject other)
    {
        return Position.Equals(other.Position);
    }

    public override int GetHashCode()
    {
        return Position.GetHashCode();
    }
}
