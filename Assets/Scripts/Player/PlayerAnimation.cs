using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Player Player { get; private set; }
    [SerializeField] private Animator _animator;
    [SerializeField] private string _isRun = "IsRun";

    public void Init(Player player)
    {
        Player = player;
        Player.Move.OnRunStarting += SetWalkState;
    }

    private void OnDisable()
    {
        Player.Move.OnRunStarting -= SetWalkState;
    }

    private void SetWalkState(bool state)
    {
        _animator.SetBool(_isRun, state);
    }
}
