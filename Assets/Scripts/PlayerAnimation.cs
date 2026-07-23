using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _isRun = "IsRun";

    private void OnEnable()
    {
        _playerMove.OnRunStarting += SetWalkState;
    }

    private void OnDisable()
    {
        _playerMove.OnRunStarting -= SetWalkState;
    }

    private void SetWalkState(bool state)
    {
        _animator.SetBool(_isRun, state);
    }
}
