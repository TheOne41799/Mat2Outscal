using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletModel : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 5f;
    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }


    private Vector2 direction;
    public Vector2 Direction { get => direction; set => direction = value.normalized; }
}










