using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletView : MonoBehaviour
{
    [SerializeField] private BulletModel bulletModel;


    public void MoveBullet()
    {
        transform.Translate(bulletModel.BulletSpeed * Time.deltaTime * Vector2.right);
    }


    public void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}









