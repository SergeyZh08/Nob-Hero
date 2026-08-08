using UnityEngine;

public class LookAtDirection : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.back, Vector3.up);
    }
}
