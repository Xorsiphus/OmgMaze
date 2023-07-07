using ControlStrategies;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform orientation;

    [SerializeField] private ControlTypeWrapper controlTypeWrapper;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        controlTypeWrapper.GetControlStrategy().UpdateRotation(ref _xRotation, ref _yRotation);

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}