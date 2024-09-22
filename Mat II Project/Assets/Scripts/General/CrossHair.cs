using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    [SerializeField] private Transform crossHair;


    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }


    private void Update()
    {
        HandlePlayerCrossHairPosition();
    }


    private void HandlePlayerCrossHairPosition()
    {
        Vector3 mousePosition = InputManager.Instance.GetMousePosition();

        transform.position = mousePosition + Vector3.forward;

        transform.Rotate(Vector3.forward * 200 * Time.deltaTime);
    }
}
