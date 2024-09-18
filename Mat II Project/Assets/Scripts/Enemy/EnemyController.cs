using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyModel enemyModel;
    [SerializeField] private EnemyView enemyView;


    private void OnEnable()
    {
        PlayerPositionUpdateEvent.OnPlayerPositionUpdated += UpdatePlayerPosition;
    }


    private void OnDisable()
    {
        PlayerPositionUpdateEvent.OnPlayerPositionUpdated -= UpdatePlayerPosition;
    }


    private void UpdatePlayerPosition(Vector3 newPlayerPosition)
    {
        enemyModel.PlayerPosition = newPlayerPosition;
    }


    private void Update()
    {
        MoveTowardsPlayer();
    }


    private void MoveTowardsPlayer()
    {
        Vector2 direction = (enemyModel.PlayerPosition - transform.position).normalized;
        Vector2 velocity = direction * enemyModel.MoveSpeed;

        enemyView.SetVelocity(velocity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        enemyView.SetRotation(angle);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        BulletController bulletController = collision.GetComponent<BulletController>();

        if (bulletController != null)
        {
            enemyModel.EnemyHealth -= bulletController.GetBulletDamage();

            if (enemyModel.EnemyHealth <= 0)
            {
                enemyView.DestroyEnemy();
            }
        }
    }
}
