using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModel : MonoBehaviour
{
    [SerializeField] private float fireRate = 0.1f;
    public float FireRate { get => fireRate; set => fireRate = value; }


    [SerializeField] private float bulletVelocity = 100f;
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
}











