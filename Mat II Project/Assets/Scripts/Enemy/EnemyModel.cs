using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyModel : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 4f;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }


    [SerializeField] private Rigidbody2D enemyRB;
    public Rigidbody2D EnemyRB { get => enemyRB; }


    private Vector3 playerPosition;
    public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }


    [SerializeField] private float rotationSpeed = 5f;
    public float RotationSpeed { get => rotationSpeed; }


    [SerializeField] private int enemyHealth = 5;
    public int EnemyHealth { get => enemyHealth; set => enemyHealth = value; }


    [SerializeField] private int dealDamage = 1;
    public int DealDamage { get => dealDamage; }


    [SerializeField] private SpriteRenderer enemySpriteRenderer;
    public SpriteRenderer EnemySpriteRenderer { get => enemySpriteRenderer; set => enemySpriteRenderer = value; }


    private Color originalColor;
    public Color OriginalColor { get => originalColor; set => originalColor = value; }


    [SerializeField] private float enemyStunnedTime = 0.1f;
    public float EnemyStunnedTime { get => enemyStunnedTime; }


    [SerializeField] private ParticleSystem deathParticleEffect;
    public ParticleSystem DeathParticleEffect { get => deathParticleEffect; set => deathParticleEffect = value; }
}




