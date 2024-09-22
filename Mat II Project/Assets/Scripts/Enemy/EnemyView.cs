using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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


    private void OnDestroy()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlayingOrWillChangePlaymode)
        {
            return;
        }
#endif

        if (Application.isPlaying)
        {
            SpawnHitParticle();
        }
    }


    public IEnumerator UpdatePlayerSpriteColor()
    {
        enemyModel.EnemySpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(enemyModel.EnemyStunnedTime);
        enemyModel.EnemySpriteRenderer.color = enemyModel.OriginalColor;
    }


    public void SpawnHitParticle()
    {
        ParticleSystem particle = Instantiate(enemyModel.DeathParticleEffect, transform.position, transform.rotation);
        particle.Play();


        Destroy(particle.gameObject, particle.main.duration + particle.main.startLifetime.constantMax);
    }
}
