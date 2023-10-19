using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class PaintableDoor : MonoBehaviour
{
    [SerializeField]
    private Color doorColor;

    [SerializeField]
    private RoomFramePair[] roomFramePairs = new RoomFramePair[2];

    private Collider doorCollider;
    private Renderer doorRenderer;
    
    void Start()
    {
        doorCollider = GetComponent<BoxCollider>();
        doorRenderer = GetComponent<Renderer>();

        doorRenderer.material.SetColor("_BaseColor", Color.white);

        GameManager.Instance.OnPaint += CheckDoorIsOpen;
    }

    public void PaintFrame(PaintableRoom room, Color color)
    {
        foreach (RoomFramePair pair in roomFramePairs)
        {
            if (pair.room == room)
            {
                foreach (PaintableObject frame in pair.frameObjects)
                    frame.Paint(doorColor);
            }
        }
    }

    void CheckDoorIsOpen()
    {
        bool shouldClose = true;
        for (int i = 0; i < roomFramePairs.Length; i++)
        {
            bool isOpen = true;
            foreach (PaintableObject frame in roomFramePairs[i].frameObjects)
            {
                Debug.Log("Door Color:\t" + doorColor + "\nFrame Color:\t" + frame.Color);

                if (frame.Color != doorColor)
                {
                    isOpen = false;
                    break;
                }
            }
            if (isOpen)
            {
                shouldClose = false;
                doorRenderer.enabled = false;
                doorCollider.enabled = false;
                roomFramePairs[i].room.PaintRoom(doorColor);
            }
        }

        if (shouldClose)
        {
            doorRenderer.enabled = true;
            doorCollider.enabled = true;
        }

        Debug.Log("Should close: " + shouldClose);
    }
}