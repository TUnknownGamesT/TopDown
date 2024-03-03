using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : Destroyable
{
    private void Awake()
    {
        peacesMass = Constants.wallPeacesMass;
    }
}
