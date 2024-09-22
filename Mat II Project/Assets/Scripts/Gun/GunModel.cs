using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GunModel : MonoBehaviour
{
    [SerializeField] private FireMode currentFireMode;
    public FireMode CurrentFireMode { get => currentFireMode; set => currentFireMode = value; }


    [SerializeField] private int burstCount = 3;
    public int BurstCount { get => burstCount; set => burstCount = value; }


    [SerializeField] private float burstFireRate = 0.1f;
    public float BurstFireRate { get => burstFireRate;  set => burstFireRate = value; }


    [SerializeField] private bool burstFireEnabled = false;
    public bool BurstFireEnabled { get => burstFireEnabled; set => burstFireEnabled = value; }


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


    /*[SerializeField] private GameObject bulletPrefab;
    public GameObject BulletPrefab { get => bulletPrefab; }*/


    [SerializeField] private GameObject[] bulletPrefabs;
    public GameObject[] BulletPrefab { get => bulletPrefabs; }


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


    [SerializeField] private GameObject muzzleFlashSprites;
    public GameObject MuzzleFlashSprites { get => muzzleFlashSprites; set { muzzleFlashSprites = value; } }


    [SerializeField] private GameObject muzzleFlashLight;
    public GameObject MuzzleFlashLight {  get => muzzleFlashLight; set { muzzleFlashLight = value; } }


    [SerializeField] private float muzzleFlashTime = 0.05f;
    public float MuzzleFlashTime { get => muzzleFlashTime; }


    private Vector3 initialPosition;
    public Vector3 InitialPosition { get => initialPosition; set => initialPosition = value; }


    private Vector3 targetRecoilPosition;
    public Vector3 TargetRecoilPosition { get => targetRecoilPosition; set => targetRecoilPosition = value; }


    [SerializeField] private float recoilAmount = 0.1f;
    public float RecoilAmount { get => recoilAmount; set => recoilAmount = value; }


    [SerializeField] private float recoilRecoverySpeed = 0.1f;
    public float RecoilRecoverySpeed { get => recoilRecoverySpeed; set => recoilRecoverySpeed = value;}


    [SerializeField] private int maxAmmo = 100;
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }


    [SerializeField] private int currentAmmo;
    public int CurrentAmmo { get => currentAmmo; set => currentAmmo = value; }


    [SerializeField] private int magazineSize = 30;
    public int MagazineSize { get => magazineSize; set => magazineSize = value; }


    [SerializeField] private float reloadTime = 2f;
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }


    private bool isReloading = false;
    public bool IsReloading { get => isReloading; set => isReloading = value; }
}














