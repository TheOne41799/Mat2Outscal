using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class BulletController : MonoBehaviour
{
    [SerializeField] private BulletModel bulletModel;
    [SerializeField] private BulletView bulletView;


    public void Initialize(float speed, Vector2 direction)
    {
        bulletModel.BulletSpeed = speed;
        bulletModel.Direction = direction;
    }


    private void Update()
    {
        MoveBullet();
    }


    private void MoveBullet()
    {
        bulletView.MoveBullet();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageableObject = collision.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            damageableObject.TakeDamage(bulletModel.BulletDamage);

            bulletView.DestroyBullet();
        }        
    }


    public int GetBulletDestroyTime()
    {
        return bulletModel.BulletDestroyTime;
    }
}









