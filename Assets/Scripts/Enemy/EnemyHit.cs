using System.Collections;
using TMPro;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private float _animationTime = 1f;
    [SerializeField] private Enemy _enemy;
    private float _currentDamage;

    private Coroutine _currentCoroutine;

    private void OnEnable()
    {
        if (_enemy)
        {
            _enemy.Health.OnEnemyHit += Hit;
        }
        
        _currentDamage = 0;
        _damageText.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (_enemy)
        {
            _enemy.Health.OnEnemyHit -= Hit;
        }
        
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    private void Hit(float damage)
    {
        _damageText.gameObject.SetActive(true);

        _currentDamage += damage;

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        _currentCoroutine = StartCoroutine(HitVisualRoutine(_currentDamage));
    }

    private IEnumerator HitVisualRoutine(float damage)
    {
        //_scaleObject.localScale = _startTextSize;

        for (float t = 0; t < _animationTime; t += Time.deltaTime)
        {
            _damageText.SetText("{0}", (int)damage);

            //_scaleObject.localScale += _startTextSize * _sizeDelta;

            yield return null;
        }

        _currentDamage = 0;
        _currentCoroutine = null;
        _damageText.gameObject.SetActive(false);
    }
}
