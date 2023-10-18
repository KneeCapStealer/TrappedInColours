using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public UnityAction OnPaint;

    public bool ShouldCheck;

    [SerializeField]
    private PaintableObject[] PaintableObjects;
    [SerializeField]
    private PaintableObject[] PaintableObjects2;


    // In order not to override Singleton Awake method
    private new void Awake()
    {
        base.Awake();
    }

    private void FixedUpdate()
    {
        if (ShouldCheck)
        {
            OnPaint.Invoke();
            Debug.Log("Is Checking");
        }
    }

    public  void DebugPaint(int index, Color color)
    {
        switch (index)
        {
            case 0:
                foreach (PaintableObject obj in PaintableObjects)
                    obj.Paint(color);

                break;

            case 1:
                foreach (PaintableObject obj in PaintableObjects2)
                    obj.Paint(color);

                break;

            default:
                Debug.LogError("you dum dum");
                break;
        }
    }
}