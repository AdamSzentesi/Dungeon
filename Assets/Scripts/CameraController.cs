using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform TargetTransform;

    public Camera Camera
    {
        get
        {
            return _Camera;
        }
    }

    private Camera _Camera;

    private void Awake()
    {
        _Camera = GetComponent<Camera>();
    }

    public void SetLocalPosition(Vector3 localPosition)
    {
        TargetTransform.localPosition = localPosition;
    }

}
