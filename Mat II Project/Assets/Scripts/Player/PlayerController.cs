using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.Rendering.DebugUI;

public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerModel playerModel;
    [SerializeField] private PlayerView playerView;


    private void OnEnable()
    {
        KeyGameEvents.OnEnemyDeath += UpdateScore;
    }

    private void OnDisable()
    {
        KeyGameEvents.OnEnemyDeath -= UpdateScore;
    }


    private void Start()
    {
        EquipGun();
        StartCoroutine(BroadcastPlayerPositionCoroutine());
        playerModel.InitialLightRadius = playerModel.PlayerLight.pointLightOuterRadius;
        playerModel.OriginalColor = playerModel.PlayerSpriteRenderer.color;

        playerModel.Score = 0;
        GameManager.Instance.Score = playerModel.Score;
        playerModel.HighScore = GameManager.Instance.HighScore;
        GameManager.Instance.UpdateScore();
        UIController.Instance.DisplayCurrentFireModeInHUD((int)FireMode.Single);
        UIController.Instance.UpdatePlayerHealth(playerModel.PlayerHealth);
    }


    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameState.HUD) return;

        HandlePlayerRotation();
        HandlePlayerShooting();
        HandleEnemyDetection();
        HandleGunSwitching();
        HandleReloading();
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
                        UIController.Instance.DisplayCurrentFireModeInHUD((int) FireMode.Burst);
                        break;
                    case FireMode.Burst:
                        playerModel.EquippedGunController.SetCurrentFireMode(FireMode.Auto);
                        UIController.Instance.DisplayCurrentFireModeInHUD((int)FireMode.Auto);
                        break;
                    case FireMode.Auto:
                        playerModel.EquippedGunController.SetCurrentFireMode(FireMode.Single);
                        UIController.Instance.DisplayCurrentFireModeInHUD((int)FireMode.Single);
                        break;
                }
            }
        }
    }


    private void HandleReloading()
    {
        if(InputManager.Instance.IsRkeyPressed())
        {
            playerModel.EquippedGunController.ManualReload();
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

        if(playerModel.PlayerHealth < 0)  playerModel.PlayerHealth = 0;
        UIController.Instance.UpdatePlayerHealth(playerModel.PlayerHealth);

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

            SoundManager.Instance.Play(Sounds.PLAYER_KILLED);

            playerView.SpawnHitParticle();

            playerView.DestroyPlayer();
        }
        else
        {
            SoundManager.Instance.Play(Sounds.PLAYER_HIT);
        }
    }


    private void HandleEnemyDetection()
    {
        Collider2D[] enemiesInRadius = Physics2D.OverlapCircleAll(transform.position, 
                                                                  playerModel.PlayerLight.pointLightOuterRadius, 
                                                                  playerModel.EnemyLayerMask);

        playerView.UpdateLightRadius(enemiesInRadius.Length);
    }


    private void UpdateScore(int scoreAmount)
    {
        playerModel.Score += scoreAmount;

        GameManager.Instance.Score = playerModel.Score;

        UpdateHighScore();

        GameManager.Instance.UpdateScore();
    }


    private void UpdateHighScore()
    {
        if (playerModel.Score > playerModel.HighScore)
        {
            playerModel.HighScore = playerModel.Score;
            GameManager.Instance.HighScore = playerModel.HighScore;
        }
    }
}







