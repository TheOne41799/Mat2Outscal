using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShellView : MonoBehaviour
{
    [SerializeField] private ShellModel shellModel;


    public void FadeShells(float delay)
    {
        if (shellModel.FadeCoroutine != null)
        {
            StopCoroutine(shellModel.FadeCoroutine);
        }

        shellModel.FadeCoroutine = StartCoroutine(FadeShellsCoroutine(delay));
    }


    public void StopMovingAndRotatingShells()
    {
        if (shellModel.ShellRB.velocity.sqrMagnitude > Mathf.Pow(shellModel.StopMovementThreshold, 2))
        {
            shellModel.ShellRB.velocity *= shellModel.DecelerationRate;
        }
        else
        {
            shellModel.ShellRB.velocity = Vector2.zero;
        }

        if (Mathf.Abs(shellModel.ShellRB.angularVelocity) > shellModel.StopRotationThreshold)
        {
            shellModel.ShellRB.angularVelocity *= shellModel.DecelerationRate;
        }
        else
        {
            shellModel.ShellRB.angularVelocity = 0f;
        }
    }


    private IEnumerator FadeShellsCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        float fadeDuration = shellModel.FadeDuration;
        float currentTime = 0f;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, currentTime / fadeDuration);

            for (int i = 0; i < shellModel.ShellSpriteRenderers.Length; i++)
            {
                Color currentColor = shellModel.ShellSpriteRenderers[i].color;
                shellModel.ShellSpriteRenderers[i].color = new Color(
                    currentColor.r,
                    currentColor.g,
                    currentColor.b,
                    alpha);
            }

            yield return null;
        }

        DestroyShell();
    }


    public void DestroyShell()
    {
        Destroy(this.gameObject);
    }
}
