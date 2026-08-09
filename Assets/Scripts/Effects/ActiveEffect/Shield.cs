using System.Collections;
using UnityEngine;

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
        //без урона, если включен
        return gameObject.activeSelf ? 0 : value;
    }

    private IEnumerator LifeProcess(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        _currentCoroutine = null;
        Deactivate();
    }

    
}
