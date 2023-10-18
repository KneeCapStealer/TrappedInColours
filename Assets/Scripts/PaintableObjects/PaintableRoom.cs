using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PaintableRoom : MonoBehaviour
{
    private Color lastPaintColor;

    [SerializeField]
    private PaintableObject[] roomObjects;
    [SerializeField]
    private PaintableDoor[] roomDoors;

    public void PaintRoom(Color color)
    {
        if (color == lastPaintColor)
            return;

        foreach (PaintableObject obj in roomObjects)
            obj.Paint(color);

        foreach (PaintableDoor door in roomDoors)
            door.PaintFrame(color);
    }
}
