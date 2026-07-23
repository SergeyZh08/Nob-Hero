using UnityEngine;

public class EnemyVisualProvider : MonoBehaviour
{
    [SerializeField] private EnemyVisual _settings;
    private Pool<EnemyVisual> _pool;

    private void Start()
    {
        _pool = new Pool<EnemyVisual>(_settings, 10, 5, transform);
    }

    public void StartEffect(Enemy enemy)
    {
        EnemyVisual visual = _pool.Get();

        visual.OnEffectFinished += HideEffect;

        visual.PlayEffect(enemy.transform.position);
    }

    private void HideEffect(EnemyVisual visual)
    {
        visual.OnEffectFinished -= HideEffect;

        _pool.Release(visual);
    }
}