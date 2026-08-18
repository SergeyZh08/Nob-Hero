using System;
using System.Collections;
using UnityEngine;

public class Rock : MonoBehaviour, IPoolable
{
    [SerializeField] private GameObject _capsuleForDamage;
    [SerializeField] private float _rockSpeed = 0.3f;
    static private readonly WaitForSeconds _delay = new WaitForSeconds(1.2f);
    private float _damage;
    private int _passCount;
    private Action<Rock, int> OnGrewUp;
    private Action<Rock> _release;
    private Coroutine _currentCoroutine;
    
    public void Init(float damage, int passCount, Action<Rock, int> grewUp, Action<Rock> action)
    {
        _damage = damage;
        _passCount = passCount;
        OnGrewUp = grewUp;
        _release = action;
        _currentCoroutine = StartCoroutine(LifeProcess());
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);
        }
    }

    private IEnumerator LifeProcess()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / _rockSpeed)
        {
            _capsuleForDamage.transform.localScale = new Vector3(1f, 1f, t);
            yield return null;
        }

        _passCount--;
        OnGrewUp?.Invoke(this, _passCount);

        _capsuleForDamage.gameObject.SetActive(false);

        yield return _delay;

        Die();
    }

    private void Die()
    {
        _currentCoroutine = null;
        _release?.Invoke(this);
    }

    public void OnGetFromPool()
    {
        _capsuleForDamage.gameObject.SetActive(true);
    }

    public void OnReleaseToPool()
    {
        _damage = 0;
        _passCount = 0;
        OnGrewUp = null;
        _release = null;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }
}
