using UnityEngine;

public sealed class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject focus = null;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position - focus.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = focus.transform.position + offset;
    }
}
