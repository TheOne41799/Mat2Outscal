using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnModel : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    public GameObject Enemy { get => enemy; }

    
    [SerializeField] private float ellipseRadiusX = 10f;
    public float EllipseRadiusX { get => ellipseRadiusX;}


    [SerializeField] private float ellipseRadiusY = 5f;
    public float EllipseRadiusY { get => ellipseRadiusY;}


    [SerializeField] private float enemySpawnInterval = 2f;
    public float EnemySpawnInterval { get => enemySpawnInterval; set => enemySpawnInterval = value; }


    [SerializeField] private float minEnemySpawnInterval = 0.5f;
    public float MinEnemySpawnInterval { get => minEnemySpawnInterval; }


    [SerializeField] private float maxEnemySpawnInterval = 2f;
    public float MaxEnemySpawnInterval { get => maxEnemySpawnInterval; }


    [SerializeField] private float decreaseSpawnIntervalBy = 0.1f;
    public float DecreaseSpawnIntervalBy { get => decreaseSpawnIntervalBy; }
}















