using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    [SerializeField] private EnemyModel enemyModel;


    public void SetVelocity(Vector2 velocity)
    {
        enemyModel.EnemyRB.velocity = velocity;
    }


    public void SetRotation(float angle)
    {
        enemyModel.EnemyRB.rotation = angle;
    }


    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }


    public IEnumerator UpdatePlayerSpriteColor()
    {
        enemyModel.EnemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(enemyModel.EnemyStunnedTime);
        enemyModel.EnemySpriteRenderer.color = enemyModel.OriginalColor;
    }
}
