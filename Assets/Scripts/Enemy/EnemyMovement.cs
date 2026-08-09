using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Enemy Enemy {get; private set;}
    [SerializeField] private float _speed;
    [SerializeField] private float _angularSpeed;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _distanceToCarry;
    private Transform _target;
    private List<ISpeedModifier> _speedModifiers = new List<ISpeedModifier>();
    private float _modifierSpeed = 1f;

    public void Init(Enemy enemy)
    {
        Enemy = enemy;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = _speed * _modifierSpeed * transform.forward;

        Vector3 toTarget = _target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(toTarget);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * _angularSpeed);

        if (toTarget.magnitude > _distanceToCarry)
        {
            transform.position += toTarget * 1.95f;
        }
    }


    public void AddModifier(ISpeedModifier modifier)
    {
        _speedModifiers.Add(modifier);
        CalculateModifierSpeed();
    }

    public void RemoveModifier(ISpeedModifier modifier)
    {
        _speedModifiers.Remove(modifier);
        CalculateModifierSpeed();
    }

    private void CalculateModifierSpeed()
    {
        _modifierSpeed = 1f;

        foreach (var m in _speedModifiers)
        {
            _modifierSpeed = m.ModifySpeed(_modifierSpeed);
        }
    }

    private void OnDisable()
    {
        _target = null;
        _rigidbody.linearVelocity = Vector3.zero;
    }
}
