using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class PaintableObject : MonoBehaviour
{
    public Color Color { private set; get; }

    private Material mat;
    void Start()
    {
        mat = GetComponent<Renderer>().material;

        Color = mat.GetColor("_BaseColor");

        GameManager.Instance.PaintThing += Paint;
    }

    public void Paint(Color color)
    {
        mat.SetColor("_BaseColor", color);
        Color = color;
    }
}
