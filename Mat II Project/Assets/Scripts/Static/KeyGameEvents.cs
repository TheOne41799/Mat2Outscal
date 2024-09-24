using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyGameEvents
{
    public static event Action<Vector3> OnPlayerPositionUpdated;
    public static event Action OnPlayerDeath;
    public static event Action<int> OnEnemyDeath;
    public static event Action<float> OnDIMValueUpdated;


    public static void BroadcastPlayerPosition(Vector3 playerPosition)
    {
        if (OnPlayerPositionUpdated != null)
        {
            OnPlayerPositionUpdated(playerPosition);
        }
    }


    public static void BroadcastPlayerDeath()
    {
        if (OnPlayerDeath != null)
        {
            OnPlayerDeath();
        }
    }


    public static void BroadcastEnemyDeath(int value)
    {
        if (OnEnemyDeath != null)
        {
            OnEnemyDeath(value);
        }
    }


    public static void BroadcastDIMValue(float dimValue)
    {
        if (OnDIMValueUpdated != null)
        {
            OnDIMValueUpdated(dimValue);
        }
    }
}








