using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemySpawnModel enemySpawnModel;


    private void OnEnable()
    {
        KeyGameEvents.OnPlayerPositionUpdated += UpdateSpawnerPosition;
        KeyGameEvents.OnPlayerDeath += HandlePlayerDeath;
    }


    private void OnDisable()
    {
        KeyGameEvents.OnPlayerPositionUpdated -= UpdateSpawnerPosition;
        KeyGameEvents.OnPlayerDeath -= HandlePlayerDeath;
    }


    private void UpdateSpawnerPosition(Vector3 newPlayerPosition)
    {
        enemySpawnModel.SpawnerPosition = newPlayerPosition;
    }


    private void Start()
    {
        enemySpawnModel.CanSpawnEnemies = true;
        enemySpawnModel.EnemySpawnCoroutine = StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while (enemySpawnModel.CanSpawnEnemies)
        {
            Vector2 spawnPosition = GetRandomPointOnEllipse();

            int randomEnemy = Random.Range(0, enemySpawnModel.EnemyPrefabs.Length);

            GameObject thisEnemy = Instantiate(enemySpawnModel.EnemyPrefabs[randomEnemy],
                                               spawnPosition,
                                               Quaternion.identity);


            //GameObject thisEnemy = Instantiate(enemySpawnModel.Enemy, spawnPosition, Quaternion.identity);


            enemySpawnModel.Enemies.Add(thisEnemy);

            enemySpawnModel.EnemySpawnInterval = Mathf.Clamp(
                                    enemySpawnModel.EnemySpawnInterval - enemySpawnModel.DecreaseSpawnIntervalBy,
                                    enemySpawnModel.MinEnemySpawnInterval,
                                    enemySpawnModel.MaxEnemySpawnInterval);

            yield return new WaitForSeconds(enemySpawnModel.EnemySpawnInterval);
        }
    }


    private Vector2 GetRandomPointOnEllipse()
    {
        float angle = Random.Range(0, 2f * Mathf.PI);

        float x = enemySpawnModel.SpawnerPosition.x + enemySpawnModel.EllipseRadiusX * Mathf.Cos(angle);
        float y = enemySpawnModel.SpawnerPosition.y + enemySpawnModel.EllipseRadiusY * Mathf.Sin(angle);

        return new Vector2(x, y);
    }


    private void OnDrawGizmos()
    {
        if (enemySpawnModel == null) return;

        Gizmos.color = Color.green;
        int segments = 100;
        Vector3 previousPoint = GetPointOnEllipse(0);

        for (int i = 1; i <= segments; i++)
        {
            float angle = (i / (float)segments) * 2 * Mathf.PI;
            Vector3 nextPoint = GetPointOnEllipse(angle);
            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }
    }


    private Vector3 GetPointOnEllipse(float angle)
    {
        float x = enemySpawnModel.EllipseRadiusX * Mathf.Cos(angle);
        float y = enemySpawnModel.EllipseRadiusY * Mathf.Sin(angle);
        return new Vector3(x, y, 0);
    }


    private void HandlePlayerDeath()
    {
        if (enemySpawnModel.EnemySpawnCoroutine != null)
        {
            enemySpawnModel.CanSpawnEnemies = false;
            StopCoroutine(enemySpawnModel.EnemySpawnCoroutine);
            enemySpawnModel.EnemySpawnCoroutine = null;

            StartCoroutine(WaitForEndofScene());
        }
    }


    private IEnumerator WaitForEndofScene()
    {
        yield return StartCoroutine(DestroyEnemies());

        GameManager.Instance.SetGameState(GameState.RESTART_MENU);
    }


    private IEnumerator DestroyEnemies()
    {
        foreach (var enemy in enemySpawnModel.Enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy, enemySpawnModel.WaitTimeToKillLeftOverEnemies);
            }
        }

        enemySpawnModel.Enemies.Clear();

        yield return new WaitForSeconds(5);
    }
}












