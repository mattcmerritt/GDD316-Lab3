using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    // Unity components that control the visuals
    [SerializeField] private Light GlobalLight;
    [SerializeField] private Animator LightAnimator;

    // Data
    [SerializeField] private bool IsDay;
    [SerializeField, Range(0f, 120f)] private float DayLength, TransitionLength = 1f;

    public void Start()
    {
        IsDay = true;
        StartCoroutine(StartDay());
    }

    private IEnumerator StartDay()
    {
        // GlobalLight.intensity = 1;

        // Wait until a second before the day ends
        yield return new WaitForSeconds(DayLength - TransitionLength);
        // Play sunset animation
        LightAnimator.SetTrigger("End Day");
        // Wait for animation to finish
        yield return new WaitForSeconds(TransitionLength);

        IsDay = false;
        StartCoroutine(StartNight());
    }

    private IEnumerator StartNight()
    {
        // GlobalLight.intensity = 0;

        // Wait until a second before the night ends
        yield return new WaitForSeconds(DayLength - TransitionLength);
        // Play sunrise animation
        LightAnimator.SetTrigger("End Night");
        // Wait for animation to finish
        yield return new WaitForSeconds(TransitionLength);

        IsDay = true;
        StartCoroutine(StartDay());
    }

    public bool CheckIsDay()
    {
        return IsDay;
    }
}
