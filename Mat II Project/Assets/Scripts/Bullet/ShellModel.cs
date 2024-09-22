using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShellModel : MonoBehaviour
{
    [SerializeField] private float fadeDuration = 2f;
    public float FadeDuration { get => fadeDuration; set => fadeDuration = value; }


    [SerializeField] private float fadeStartTime = 8f;
    public float FadeStartTime { get => fadeStartTime; }


    private Coroutine fadeCoroutine;
    public Coroutine FadeCoroutine { get => fadeCoroutine; set => fadeCoroutine = value; }


    [SerializeField] private SpriteRenderer[] shellSpriteRenderers;
    public SpriteRenderer[] ShellSpriteRenderers { get => shellSpriteRenderers; set => shellSpriteRenderers = value; }


    private Color[] originalColor = new Color[3];
    public Color[] OriginalColor { get => originalColor; set => originalColor = value; }


    [SerializeField] private Rigidbody2D shellRB;
    public Rigidbody2D ShellRB { get => shellRB; set => shellRB = value; }


    [SerializeField] private float stopMovementThreshold = 0.2f;
    public float StopMovementThreshold { get => stopMovementThreshold; set => stopMovementThreshold = value; }


    [SerializeField] private float stopRotationThreshold = 1f;
    public float StopRotationThreshold { get => stopRotationThreshold; set => stopRotationThreshold = value; }


    [SerializeField] private float decelerationRate = 0.98f;
    public float DecelerationRate { get => decelerationRate; set => decelerationRate = value; }
}






















