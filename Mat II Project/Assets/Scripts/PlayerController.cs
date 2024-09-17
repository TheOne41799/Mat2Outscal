using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private float moveSpeed = 5f;


    private void Update()
    {
        RotatePlayerToMousePosition();
    }


    private void FixedUpdate()
    {
        MovePlayer();
    }


    private void MovePlayer()
    {
        Vector2 movementInput = InputManager.Instance.GetMovementInput();
        Vector2 velocity = movementInput.normalized * moveSpeed;

        playerRB.velocity = velocity;
    }


    private void RotatePlayerToMousePosition()
    {
        Vector3 mousePosition = InputManager.Instance.GetMousePosition();
        Vector3 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        playerRB.rotation = angle;
    }
}







