using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunView : MonoBehaviour
{
    [SerializeField] private GunModel gunModel;


    public void Shoot()
    {
        if (gunModel.BulletController != null)
        {
            gunModel.BulletController.Initialize(gunModel.BulletVelocity, gunModel.BulletDirection);
        }
    }
}










