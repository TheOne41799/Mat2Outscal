using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;


    public void SetVelocity(Vector2 velocity)
    {
        playerModel.PlayerRB.velocity = velocity;
    }


    public void SetRotation(float angle)
    {
        playerModel.PlayerRB.rotation = angle;
    }


    public void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }


    public void UpdateLightRadius(int enemyCountWithinRadius)
    {
        if (enemyCountWithinRadius > 0)
        {
            playerModel.PlayerLight.pointLightOuterRadius -= playerModel.LightRadiusDecreaseRate
                                                             * Time.deltaTime;

            if (playerModel.PlayerLight.pointLightOuterRadius <= playerModel.CriticalLightRadius)
            {
                DestroyPlayer();
            }
        }
        else
        {
            if (playerModel.PlayerLight.pointLightOuterRadius >= playerModel.InitialLightRadius) return;

            playerModel.PlayerLight.pointLightOuterRadius += playerModel.LightRadiusIncreaseRate * Time.deltaTime;
            playerModel.PlayerLight.pointLightOuterRadius = Mathf.Min(playerModel.PlayerLight.pointLightOuterRadius,
                                                                      playerModel.InitialLightRadius);
        }
    }
}











