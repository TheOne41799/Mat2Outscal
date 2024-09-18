using System.Collections;
using System.Collections.Generic;
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
}









