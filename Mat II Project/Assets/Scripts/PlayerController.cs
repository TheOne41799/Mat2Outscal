using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private PlayerView playerView;


    private void Start()
    {
        EquipGun();
    }


    private void Update()
    {
        HandlePlayerRotation();
        HandlePlayerShooting();
    }


    private void FixedUpdate()
    {
        HandlePlayerMovement();
    }


    private void HandlePlayerMovement()
    {
        Vector2 movementInput = InputManager.Instance.GetMovementInput();
        Vector2 velocity = movementInput.normalized * playerModel.MoveSpeed;

        playerView.SetVelocity(velocity);
    }


    private void HandlePlayerRotation()
    {
        Vector3 mousePosition = InputManager.Instance.GetMousePosition();
        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerView.SetRotation(angle);
    }


    private void HandlePlayerShooting()
    {
        if (playerModel.EquippedGunController != null && InputManager.Instance.IsLeftMouseButtonPressed())
        {
            playerModel.EquippedGunController.Shoot();
        }
    }


    private void EquipGun()
    {
        GameObject gunObject = Instantiate(playerModel.StartingGunPrefab, 
                                           playerModel.GunHold.position, 
                                           playerModel.GunHold.rotation);

        gunObject.transform.parent = playerModel.GunHold;

        playerModel.EquippedGunController = gunObject.GetComponent<GunController>();
    }
}







