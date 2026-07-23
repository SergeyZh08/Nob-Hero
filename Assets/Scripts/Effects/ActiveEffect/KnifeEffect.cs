using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(KnifeEffect), menuName = ("ContinuousEffect/" + nameof(KnifeEffect)))]

public class KnifeEffect : ActiveEffect
{
    private Pool<Knife> _pool;
    [SerializeField] private Knife _knifePrefab;
    private Knife[] _knives = new Knife[12];

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _pool = new Pool<Knife>(_knifePrefab, 12, 1, null);
    }

    protected override void Produce()
    {
        base.Produce();

        for (int i = 0; i < Current.Number; i++)
        {
            if (_knives[i] == null)
            {
                CreateKnife(i);
                break;
            }
        }
    }

    private void CreateKnife(int i)
    {
        Vector3 position = _player.transform.position + new Vector3(0, 0, 1 + i / 5f);
        _knives[i] = _pool.Get();
        _knives[i].transform.position = position;
        _knives[i].transform.rotation = Quaternion.identity;
        _knives[i].Init(ApplyPassCountBoost(Current.PassCount), Current.Speed, ApplyDamageBoost(Current.Damage), _player.transform);
        _knives[i].OnDie += RemoveKnife;
    }

    private void RemoveKnife(Knife knife)
    {
        knife.OnDie -= RemoveKnife;
        _knives[Array.IndexOf(_knives, knife)] = null;
        _pool.Release(knife);
    }
}
