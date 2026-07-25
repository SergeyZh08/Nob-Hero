using UnityEngine;

public class Rotation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationVector;

    private void Update()
    {
        transform.Rotate(_rotationVector * Time.deltaTime);
    }
}
