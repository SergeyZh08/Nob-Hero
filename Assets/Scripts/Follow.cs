using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    
    private void LateUpdate()
    {
        transform.position = _target.position;
    }
}
