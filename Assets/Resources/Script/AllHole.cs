﻿using UnityEngine;
using UnityEngine.UI;

public class AllHole : Mask
{
    public override bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return true;
    }
}
