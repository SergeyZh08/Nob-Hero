using System.Collections;
using UnityEngine;

public class Nova : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    private float _damage;
    private float _radius;
    private Collider[] _enemies = new Collider[30];
    private static readonly WaitForSeconds delay = new WaitForSeconds(3f);
    private Coroutine _currentCoroutine;

    public void Init(float damage, float radius, Vector3 position)
    {
        _damage = damage;
        _radius = radius;
        transform.position = position;
        gameObject.SetActive(true);
        
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(LifeProcess());
    }

    private IEnumerator LifeProcess()
    {
        int len = Physics.OverlapSphereNonAlloc(transform.position, _radius, _enemies, _layerMask, QueryTriggerInteraction.Ignore);

        for (int i = 0; i < len; i++)
        {
            if (_enemies[i].TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_damage);
            }
        }

        yield return delay;

        gameObject.SetActive(false);
    }
}
