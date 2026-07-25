using System;
using System.Collections;
using UnityEngine;

public class ExperienceVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem _levelUp;
    private Transform _target;
    private Coroutine _currentCoroutine;

    public void Init(Transform target)
    {
        _target = target;
    }

    public void Play(Action onFinished)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(VisualProcess(onFinished));
    }

    private IEnumerator VisualProcess(Action onFinished)
    {
        _levelUp.Play();

        while (_levelUp.isPlaying)
        {
            transform.position = _target.position;
            yield return null;
        }

        onFinished?.Invoke();
        _currentCoroutine = null;
    }
}
