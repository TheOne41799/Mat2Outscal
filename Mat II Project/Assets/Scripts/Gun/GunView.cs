using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunView : MonoBehaviour
{
    [SerializeField] private GunModel gunModel;


    private void Start()
    {
        MuzzleFlashDeactivate();
    }


    public void Shoot()
    {
        if (gunModel.BulletController != null)
        {
            gunModel.BulletController.Initialize(gunModel.BulletVelocity, gunModel.BulletDirection);

            StartCoroutine(MuzzleFlashCoroutine());
        }
    }


    private IEnumerator MuzzleFlashCoroutine()
    {
        MuzzleFlashActivate();

        yield return new WaitForSeconds(gunModel.MuzzleFlashTime);

        MuzzleFlashDeactivate();
    }


    private void MuzzleFlashActivate()
    {
        gunModel.MuzzleFlashSprites.SetActive(true);
        gunModel.MuzzleFlashLight.SetActive(true);
    }


    private void MuzzleFlashDeactivate()
    {
        gunModel.MuzzleFlashSprites.SetActive(false);
        gunModel.MuzzleFlashLight.SetActive(false);
    }
}










