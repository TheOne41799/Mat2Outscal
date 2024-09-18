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
}











