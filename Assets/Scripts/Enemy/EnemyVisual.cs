using System;
using System.Collections;
using UnityEngine;

public class EnemyVisual : MonoBehaviour, IPoolable
{
    public event Action<EnemyVisual> OnEffectFinished;
    [SerializeField] private ParticleSystem _dieEffect;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _dieSound;
    private float _duration;
    private WaitForSeconds _delay;

    private void Start()
    {
        _duration = Mathf.Max(_dieEffect.main.duration + _dieEffect.main.startLifetime.constantMax, _dieSound.length);
        _delay = new WaitForSeconds(_duration);
    }

    public void PlayEffect(Vector3 position)
    {
        _dieEffect.transform.position = position;

        _dieEffect.Play();

        _audioSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(_dieSound);

        StartCoroutine(ReleaseAfterDelay());
    }

    private IEnumerator ReleaseAfterDelay()
    {
        yield return _delay;

        OnEffectFinished?.Invoke(this);
    }

    public void OnGetFromPool()
    {
        gameObject.SetActive(true);
    }

    public void OnReleaseToPool()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
    }
}
