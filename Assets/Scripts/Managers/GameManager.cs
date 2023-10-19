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



    // In order not to override Singleton Awake method
    private new void Awake()
    {
        base.Awake();
    }
}