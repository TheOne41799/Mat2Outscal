using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyGameEvents
{
    public static event Action<Vector3> OnPlayerPositionUpdated;


    public static void BroadcastPlayerPosition(Vector3 playerPosition)
    {
        if (OnPlayerPositionUpdated != null)
        {
            OnPlayerPositionUpdated(playerPosition);
        }
    }
}
