using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunModel : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.1f;
    public float FireRate { get => fireRate; set => fireRate = value; }


    [SerializeField] private float bulletVelocity = 50f;
    public float BulletVelocity { get => bulletVelocity; set => bulletVelocity = value; }


    private float nextFireTime;
    public float NextFireTime { get => nextFireTime; set => nextFireTime = value; }


    [SerializeField] private Transform muzzle;
    public Transform Muzzle { get => muzzle; }


    private Vector2 bulletDirection;
    public Vector2 BulletDirection { get => bulletDirection; set => bulletDirection = value; }


    [SerializeField] private GameObject bulletPrefab;
    public GameObject BulletPrefab { get => bulletPrefab; }


    private BulletController bulletController;
    public BulletController BulletController { get => bulletController; set => bulletController = value; }


    [SerializeField] private GameObject shellPrefab;
    public GameObject ShellPrefab { get => shellPrefab; }


    [SerializeField] private Transform ejectionPoint;
    public Transform EjectionPoint { get => ejectionPoint; }


    [SerializeField] private float ejectionForce = 3f;
    public float EjectionForce { get => ejectionForce; }


    [SerializeField] private float randomForceVariance = 1f;
    public float RandomForceVariance { get => randomForceVariance; }


    [SerializeField] private float randomAngleVariance = 70f;
    public float RandomAngleVariance { get => randomAngleVariance; }


    [SerializeField] private float minUpwardForceShellEjection = 0.5f;
    public float MinUpwardForceShellEjection {  get => minUpwardForceShellEjection;}


    [SerializeField] private float maxUpwardForceShellEjection = 3f;
    public float MaxUpwardForceShellEjection { get => maxUpwardForceShellEjection; }


    [SerializeField] private float minShellRotationSpeed = -60f;
    public float MinShellRotationSpeed { get => minShellRotationSpeed; }


    [SerializeField] private float maxShellRotationSpeed = 60f;
    public float MaxShellRotationSpeed { get => maxShellRotationSpeed; }
}














