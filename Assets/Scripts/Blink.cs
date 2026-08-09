using System.Collections;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _blinkTime = 0.5f;
    private Coroutine _currentCoroutine;
    private MaterialPropertyBlock _block;
    private static int _emissionID = Shader.PropertyToID("_EmissionColor");

    private void Awake()
    {
        _block = new MaterialPropertyBlock();
    }

    private void OnEnable()
    {
        if (_enemy)
        {
            _enemy.Health.OnEnemyHit += StartBlink;
        }
    }

    private void OnDisable()
    {
        if (_enemy)
        {
            _enemy.Health.OnEnemyHit -= StartBlink;
        }

        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        SetEmisstionColor(Color.clear);
    }

    private void StartBlink(float _)
    {
        if (_currentCoroutine != null)
        {
            return;
        }

        _currentCoroutine = StartCoroutine(BlinkProcess());
    }

    private IEnumerator BlinkProcess()
    {
        for (float t = 0; t < _blinkTime; t += Time.deltaTime)
        {
            //красный цвет
            SetEmisstionColor(new Color(Mathf.Sin(t * 30) * 0.5f + 0.5f, 0, 0));

            yield return null;
        }

        SetEmisstionColor(Color.clear);
        _currentCoroutine = null;
    }

    private void SetEmisstionColor(Color color)
    {
        _block.SetColor(_emissionID, color);

        _renderer.SetPropertyBlock(_block);
    }
}
