using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunController : MonoBehaviour
{
    [SerializeField] private GunModel gunModel;
    [SerializeField] private GunView gunView;


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
            Shoot();
            yield return new WaitForSeconds(gunModel.BurstFireRate);
        }

        gunModel.BurstFireEnabled = false;
    }


    public void HandleAutoFire()
    {
        Shoot();
    }


    private void Shoot()
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

        int destroyTime = gunModel.BulletController.GetBulletDestroyTime();

        Destroy(bulletObject, destroyTime);
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













