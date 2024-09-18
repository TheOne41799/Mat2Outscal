using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GunModel gunModel;
    [SerializeField] private GunView gunView;


    public void Shoot()
    {
        if (Time.time >= gunModel.NextFireTime)
        {
            InstantiateBullet();
            ExecuteShoot();
            gunModel.NextFireTime = Time.time + gunModel.FireRate;
        }
    }


    private void InstantiateBullet()
    {
        GameObject bulletObject = Instantiate(gunModel.BulletPrefab,
                                              gunModel.Muzzle.position,
                                              gunModel.Muzzle.rotation);

        gunModel.BulletController = bulletObject.GetComponent<BulletController>();
        gunModel.BulletDirection = gunModel.Muzzle.right;
    }


    private void ExecuteShoot()
    {
        gunView.Shoot();
    }
}













