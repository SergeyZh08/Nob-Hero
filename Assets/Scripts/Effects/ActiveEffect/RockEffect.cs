using UnityEngine;

[CreateAssetMenu(fileName = nameof(RockEffect), menuName = ("ContinuousEffect/" + nameof(RockEffect)))]

public class RockEffect : ActiveEffect
{
    private Pool<Rock> _pool;
    [SerializeField] private Rock _rockPrefab;
    private float _rockHeight;
    private readonly float[] _angles = { -60, 0, 60 };

    protected override void FirstTimeActivate()
    {
        base.FirstTimeActivate();
        _rockHeight = _rockPrefab.GetComponentInChildren<CapsuleCollider>().height;
        _pool = new Pool<Rock>(_rockPrefab, 4, 12, null);
    }

    protected override void Produce()
    {
        base.Produce();

        Create(_player.transform.position, _player.transform.rotation, Current.PassCount);
    }

    private void CreateNext(Rock prevRock, int passCount)
    {
        if (passCount == 0)
        {
            return;
        }

        Vector3 position = prevRock.transform.position + prevRock.transform.forward * _rockHeight;


        for (int i = 0; i < 3; i++)
        {
            Vector3 direction = Quaternion.AngleAxis(_angles[i], Vector3.up) * prevRock.transform.forward;
            Quaternion rotation = Quaternion.LookRotation(direction);

            Create(position, rotation, passCount);
        }
    }

    private void Create(Vector3 position, Quaternion rotation, int passCount)
    {
        Rock newRock = _pool.Get();
        newRock.transform.SetPositionAndRotation(position, rotation);
        newRock.Init(ApplyDamageBoost(Current.Damage), passCount, CreateNext, _pool.Release);
    }
}
