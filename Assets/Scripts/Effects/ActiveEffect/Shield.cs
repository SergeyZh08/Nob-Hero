using System.Collections;
using UnityEngine;

public interface IPlayerHealthModifier
{
    public float ModifyDamage(float value);
}

public class Shield : MonoBehaviour, IPlayerHealthModifier
{
    private Transform _target;
    private Coroutine _currentCoroutine;

    public void Init(float lifeTime, Transform target)
    {
        _target = target;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(LifeProcess(lifeTime));
    }

    private void LateUpdate()
    {
        transform.position = _target.position;
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public float ModifyDamage(float value)
    {
        return gameObject.activeSelf ? 0 : value;
    }

    private IEnumerator LifeProcess(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        _currentCoroutine = null;
        Deactivate();
    }

    
}
