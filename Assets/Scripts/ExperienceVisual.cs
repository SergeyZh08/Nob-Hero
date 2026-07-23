using System;
using UnityEngine;

public class ExperienceVisual : MonoBehaviour
{
    [SerializeField] private ParticleSystem _levelUp;

    public event Action OnAnimationEnd;

    private void Start()
    {
        var main = _levelUp.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    public void Play()
    {
        _levelUp.Play();
    }

    private void OnParticleSystemStopped()
    {
        OnAnimationEnd?.Invoke();
    }
}
