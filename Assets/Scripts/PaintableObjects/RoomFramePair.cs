using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct RoomFramePair
{
    public List<PaintableObject> frameObjects;
    public PaintableRoom room;
}
