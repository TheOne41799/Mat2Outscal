using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private EnemySpawnModel enemySpawnModel;


    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }


    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPosition = GetRandomPointOnEllipse();
            Instantiate(enemySpawnModel.Enemy, spawnPosition, Quaternion.identity);
            
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

        float x = enemySpawnModel.EllipseRadiusX * Mathf.Cos(angle);
        float y = enemySpawnModel.EllipseRadiusY * Mathf.Sin(angle);

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


    public void StopSpawningEnemies()
    {
        StopCoroutine(SpawnEnemies());
    }
}
