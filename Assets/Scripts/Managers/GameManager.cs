using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public UnityAction OnPaint;

    public UnityAction<Color> PaintThing;

    public bool ShouldCheck;
    public bool ShouldPaint;

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

        if (ShouldPaint)
            PaintThing.Invoke(Color.white);
    }
}
