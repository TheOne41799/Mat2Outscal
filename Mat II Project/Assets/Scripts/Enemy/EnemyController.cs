using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField] private EnemyModel enemyModel;
    [SerializeField] private EnemyView enemyView;


    private void OnEnable()
    {
        KeyGameEvents.OnPlayerPositionUpdated += UpdatePlayerPosition;
    }


    private void OnDisable()
    {
        KeyGameEvents.OnPlayerPositionUpdated -= UpdatePlayerPosition;
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
        IDamageable damageableObject = collision.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            damageableObject.TakeDamage(enemyModel.DealDamage);
            enemyView.DestroyEnemy();
        }
    }


    public void TakeDamage(int damage)
    {
        enemyModel.EnemyHealth -= damage;

        UpdateEnemyHealth();
    }


    private void UpdateEnemyHealth()
    {
        StartCoroutine(UpdateHealthAndDestroyIfNeeded());
    }


    private IEnumerator UpdateHealthAndDestroyIfNeeded()
    {
        yield return StartCoroutine(enemyView.UpdatePlayerSpriteColor());

        if (enemyModel.EnemyHealth <= 0)
        {
            enemyView.DestroyEnemy();
        }
    }
}
