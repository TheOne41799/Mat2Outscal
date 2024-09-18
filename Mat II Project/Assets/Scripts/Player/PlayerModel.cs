using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
}
















