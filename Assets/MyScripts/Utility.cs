﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{    public static LayerMask LayerNumberToMask(int layerNum)
    {
        return 1 << layerNum;
    }
}
