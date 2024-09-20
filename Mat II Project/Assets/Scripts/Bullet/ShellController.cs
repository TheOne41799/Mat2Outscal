using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField] private ShellModel shellModel;
    [SerializeField] private ShellView shellView;


    private void Start()
    {
        InitializeShell();

        shellView.FadeShells(shellModel.FadeStartTime);
    }


    private void InitializeShell()
    {
        for (int i = 0; i < shellModel.ShellSpriteRenderers.Length; i++)
        {
            shellModel.OriginalColor[i] = shellModel.ShellSpriteRenderers[i].color;
        }
    }


    private void Update()
    {
        shellView.StopMovingAndRotatingShells();
    }
}
