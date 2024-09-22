using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private PlayerView playerView;


    private void Start()
    {
        EquipGun();
        StartCoroutine(BroadcastPlayerPositionCoroutine());
        playerModel.InitialLightRadius = playerModel.PlayerLight.pointLightOuterRadius;
        playerModel.OriginalColor = playerModel.PlayerSpriteRenderer.color;
    }


    private void Update()
    {
        HandlePlayerRotation();
        HandlePlayerShooting();
        HandleEnemyDetection();
        HandleGunSwitching();
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
        if (playerModel.EquippedGunController != null)
        {
            if (InputManager.Instance.IsLeftMouseButtonDown())
            {
                switch (playerModel.EquippedGunController.GetCurrentFireMode())
                {
                    case FireMode.Single:
                        playerModel.EquippedGunController.HandleSingleShot();
                        break;
                    case FireMode.Burst:
                        playerModel.EquippedGunController.HandleBurstFire();
                        break;
                }
            }


            if (InputManager.Instance.IsLeftMouseButtonPressed())
            {
                if (playerModel.EquippedGunController.GetCurrentFireMode() == FireMode.Auto)
                {
                    playerModel.EquippedGunController.HandleAutoFire();
                }
            }
        }
    }


    private void HandleGunSwitching()
    {
        if (playerModel.EquippedGunController != null)
        {
            if (InputManager.Instance.IsRightMouseButtonDown())
            {
                switch(playerModel.EquippedGunController.GetCurrentFireMode())
                {
                    case FireMode.Single:
                        playerModel.EquippedGunController.SetCurrentFireMode(FireMode.Burst);
                        break;
                    case FireMode.Burst:
                        playerModel.EquippedGunController.SetCurrentFireMode(FireMode.Auto);
                        break;
                    case FireMode.Auto:
                        playerModel.EquippedGunController.SetCurrentFireMode(FireMode.Single);
                        break;
                }
            }
        }
    }


    private void EquipGun()
    {
        GameObject gunObject = Instantiate(playerModel.StartingGunPrefab, 
                                           playerModel.GunHold.position, 
                                           playerModel.GunHold.rotation);

        gunObject.transform.parent = playerModel.GunHold;

        playerModel.EquippedGunController = gunObject.GetComponent<GunController>();
        playerModel.EquippedGunController.SetCurrentFireMode(FireMode.Single);
    }


    private IEnumerator BroadcastPlayerPositionCoroutine()
    {
        while (true)
        {
            KeyGameEvents.BroadcastPlayerPosition(transform.position);

            yield return new WaitForSeconds(playerModel.PositionUpdateInterval);
        }
    }


    public void TakeDamage(int damage)
    {
        playerModel.PlayerHealth -= damage;

        UpdatePlayerHealth();
    }


    private void UpdatePlayerHealth()
    {
        if(playerModel.UpdatePlayerSpriteColorCoroutine != null)
        {
            StopCoroutine(playerModel.UpdatePlayerSpriteColorCoroutine);
        }

        playerModel.UpdatePlayerSpriteColorCoroutine = StartCoroutine(playerView.UpdatePlayerSpriteColor());

        if (playerModel.PlayerHealth <= 0)
        {
            KeyGameEvents.BroadcastPlayerDeath();

            playerView.SpawnHitParticle();

            playerView.DestroyPlayer();
        }
    }


    private void HandleEnemyDetection()
    {
        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, 
                                                                  playerModel.PlayerLight.pointLightOuterRadius, 
                                                                  playerModel.EnemyLayerMask);

        playerView.UpdateLightRadius(enemiesInRadius.Length);
    }
}







