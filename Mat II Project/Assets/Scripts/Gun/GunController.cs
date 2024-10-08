using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour
{
    [SerializeField] private GunModel gunModel;
    [SerializeField] private GunView gunView;

    private Vector3 recoilSmoothing;


    private void Start()
    {
        gunModel.InitialPosition = transform.localPosition;

        gunModel.CurrentAmmo = gunModel.MagazineSize;

        UIController.Instance.UpdateAmmo(gunModel.CurrentAmmo, gunModel.MagazineSize);
    }


    private void Update()
    {
        AutoReload();

        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, 
                                                     gunModel.InitialPosition,
                                                     ref recoilSmoothing, 
                                                     gunModel.RecoilRecoverySpeed);
    }


    public FireMode GetCurrentFireMode()
    {
        return gunModel.CurrentFireMode;
    }


    public void SetCurrentFireMode(FireMode mode)
    {
        gunModel.CurrentFireMode = mode;
    }


    public void HandleSingleShot()
    {
        if (!gunModel.IsReloading) SoundManager.Instance.Play(Sounds.GUN_SINGLE);
        Shoot();
    }


    public void HandleBurstFire()
    {
        if (!gunModel.BurstFireEnabled)
        {
            StartCoroutine(HandleBurstFireCoroutine());
        }
    }


    private IEnumerator HandleBurstFireCoroutine()
    {
        gunModel.BurstFireEnabled = true;

        for (int i = 0; i < gunModel.BurstCount; i++)
        {
            if (!gunModel.IsReloading) SoundManager.Instance.Play(Sounds.GUN_BURST);
            Shoot();
            yield return new WaitForSeconds(gunModel.BurstFireRate);
        }

        gunModel.BurstFireEnabled = false;
    }


    public void HandleAutoFire()
    {
        if (!gunModel.IsReloading && !gunModel.AutoFireEnabled)
        {
            gunModel.AutoFireEnabled = true;
            StartCoroutine(PlayAutoGunSounds());
        }
    }


    private IEnumerator PlayAutoGunSounds()
    {
        while (gunModel.AutoFireEnabled && gunModel.CurrentAmmo > 0 
               && InputManager.Instance.IsLeftMouseButtonPressed())
        {
            Shoot();
            if (!gunModel.IsReloading) SoundManager.Instance.Play(Sounds.GUN_AUTO);

            yield return new WaitForSeconds(gunModel.FireRate);
        }

        gunModel.AutoFireEnabled = false;
    }


    public void ManualReload()
    {
        if (gunModel.CurrentAmmo < gunModel.MagazineSize && !gunModel.IsReloading)
        {            
            StartCoroutine(Reload());
        }
    }


    private void AutoReload()
    {
        if (gunModel.CurrentAmmo == 0)
        {
            if (!gunModel.IsReloading)
            {
                StartCoroutine(Reload());
            }
        }
    }


    private IEnumerator Reload()
    {
        gunModel.IsReloading = true;

        SoundManager.Instance.Play(Sounds.RELOAD);

        UIController.Instance.AmmoReloading();

        yield return new WaitForSeconds(gunModel.ReloadTime);

        int ammoToReload = Mathf.Min(gunModel.MaxAmmo, gunModel.MagazineSize - gunModel.CurrentAmmo);
        gunModel.CurrentAmmo += ammoToReload;
        gunModel.MaxAmmo -= ammoToReload;

        UIController.Instance.UpdateAmmo(gunModel.CurrentAmmo, gunModel.MagazineSize);

        gunModel.IsReloading = false;
    }


    private void Shoot()
    {
        if (gunModel.IsReloading) return;

        if (gunModel.CurrentAmmo > 0 && Time.time >= gunModel.NextFireTime)
        {
            InstantiateBullet();
            ExecuteShoot();
            gunModel.CurrentAmmo--;

            UIController.Instance.UpdateAmmo(gunModel.CurrentAmmo, gunModel.MagazineSize);

            gunModel.NextFireTime = Time.time + gunModel.FireRate;
        }

        ApplyRecoil();
    }


    private void ApplyRecoil()
    {
        gunModel.TargetRecoilPosition = gunModel.InitialPosition - Vector3.right * gunModel.RecoilAmount;

        transform.localPosition = gunModel.TargetRecoilPosition;
    }


    private void InstantiateBullet()
    {
        /*GameObject bulletObject = Instantiate(gunModel.BulletPrefab,
                                              gunModel.Muzzle.position,
                                              gunModel.Muzzle.rotation);*/

        GameObject bulletObject = null;


        switch (gunModel.CurrentFireMode)
        {
            case FireMode.Single:
                bulletObject = Instantiate(gunModel.BulletPrefab[0],
                                              gunModel.Muzzle.position,
                                              gunModel.Muzzle.rotation);
                break;
            case FireMode.Burst:
                bulletObject = Instantiate(gunModel.BulletPrefab[1],
                                              gunModel.Muzzle.position,
                                              gunModel.Muzzle.rotation);
                break;
            case FireMode.Auto:
                bulletObject = Instantiate(gunModel.BulletPrefab[2],
                                              gunModel.Muzzle.position,
                                              gunModel.Muzzle.rotation);
                break;
        }


        if (bulletObject != null)
        {
            gunModel.BulletController = bulletObject.GetComponent<BulletController>();
            gunModel.BulletDirection = gunModel.Muzzle.right;

            int destroyTime = gunModel.BulletController.GetBulletDestroyTime();

            Destroy(bulletObject, destroyTime);
        }
    }


    private void ExecuteShoot()
    {
        gunView.Shoot();

        Vector2 gunDirection = GetDirectionToMouse();
        EjectShell(gunDirection);
    }


    private Vector2 GetDirectionToMouse()
    {
        Vector3 mousePosition = InputManager.Instance.GetMousePosition();
        mousePosition.z = 0;

        Vector2 gunPosition = transform.position;
        Vector2 directionToMouse = (mousePosition - (Vector3)gunPosition).normalized;

        return directionToMouse;
    }


    private void EjectShell(Vector2 gunDirection)
    {
        GameObject shell = Instantiate(gunModel.ShellPrefab,
                                       gunModel.EjectionPoint.position,
                                       gunModel.EjectionPoint.rotation);

        Rigidbody2D rb = shell.GetComponent<Rigidbody2D>();

        Vector2 ejectionDirection = new Vector2(-gunDirection.y, gunDirection.x).normalized;
        float randomAngle = Random.Range(-gunModel.RandomAngleVariance, gunModel.RandomAngleVariance);
        ejectionDirection = RotateVector(ejectionDirection, randomAngle);

        Vector2 upwardForce = new Vector2(-gunDirection.y, gunDirection.x).normalized
                              * Random.Range(gunModel.MinUpwardForceShellEjection, 
                                             gunModel.MaxUpwardForceShellEjection);

        Vector2 finalEjectionDirection = ejectionDirection + upwardForce;
        float randomForce = Random.Range(gunModel.EjectionForce - gunModel.RandomForceVariance,
                                         gunModel.EjectionForce + gunModel.RandomForceVariance);

        rb.AddForce(finalEjectionDirection * randomForce, ForceMode2D.Impulse);

        float randomTorque = Random.Range(gunModel.MinShellRotationSpeed, gunModel.MaxShellRotationSpeed);
        rb.AddTorque(randomTorque);
    }


    private Vector2 RotateVector(Vector2 vec, float angleDegrees)
    {
        float angleRad = angleDegrees * Mathf.Deg2Rad;
        float cosAngle = Mathf.Cos(angleRad);
        float sinAngle = Mathf.Sin(angleRad);

        return new Vector2(
            vec.x * cosAngle - vec.y * sinAngle,
            vec.x * sinAngle + vec.y * cosAngle
        );
    }
}













