using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class PlayerModel : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }


    [SerializeField] private GameObject startingGunPrefab;
    public GameObject StartingGunPrefab { get => startingGunPrefab;    }


    private GunController equippedGunController;
    public GunController EquippedGunController { get => equippedGunController; 
                                                 set => equippedGunController = value; }


    [SerializeField] private Transform gunHold;
    public Transform GunHold { get => gunHold; }


    [SerializeField] private Rigidbody2D playerRB;
    public Rigidbody2D PlayerRB { get => playerRB; }


    [SerializeField] private float positionUpdateInterval = 0.25f;
    public float PositionUpdateInterval { get => positionUpdateInterval;}


    [SerializeField] private int playerHealth = 10;
    public int PlayerHealth { get => playerHealth; set => playerHealth = value; }


    [SerializeField] private Light2D playerLight;
    public Light2D PlayerLight { get => playerLight; set => playerLight = value; }


    [SerializeField] private float initialLightRadius;
    public float InitialLightRadius { get => initialLightRadius; set => initialLightRadius = value; }


    [SerializeField] private float criticalLightRadius = 1.5f;
    public float CriticalLightRadius { get => criticalLightRadius; }


    [SerializeField] private float lightRadiusDecreaseRate = 1f;
    public float LightRadiusDecreaseRate { get => lightRadiusDecreaseRate; }


    [SerializeField] private float lightRadiusIncreaseRate = 0.7f;
    public float LightRadiusIncreaseRate { get => lightRadiusIncreaseRate; }


    [SerializeField] private LayerMask enemyLayerMask;
    public LayerMask EnemyLayerMask { get => enemyLayerMask; }    
}
















