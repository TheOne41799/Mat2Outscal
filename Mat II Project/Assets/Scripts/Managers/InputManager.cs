using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public  static InputManager Instance {  get { return instance; } }


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.CurrentGameState == GameState.HUD)
            {
                SoundManager.Instance.Play(Sounds.PAUSE);
                GameManager.Instance.SetGameState(GameState.PAUSE_MENU);
            }
        }
    }


    public Vector2 GetMovementInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        return new Vector2(moveX, moveY);
    }


    public Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    public bool IsLeftMouseButtonPressed()
    {
        return Input.GetMouseButton(0);
    }


    public bool IsLeftMouseButtonDown()
    {
        return Input.GetMouseButtonDown(0);
    }


    public bool IsRightMouseButtonDown()
    {
        return Input.GetMouseButtonDown(1);
    }


    public bool IsRkeyPressed()
    {
        return Input.GetKeyDown(KeyCode.R);
    }
}







