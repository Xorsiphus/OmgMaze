using ControlStrategies;
using Enums;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public Transform orientation;
    [SerializeField] private StandardFpControlStrategy standardFpControlStrategy;
    [SerializeField] private DirectionalControlStrategy directionalControlStrategy;
    [SerializeField] private ControlType controlType;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        switch (controlType)
        {
            case ControlType.DirectionalControlStrategy:
                directionalControlStrategy.UpdateRotation(ref _xRotation, ref _yRotation);
                break;
            case ControlType.StandardFpControlStrategy:
                standardFpControlStrategy.UpdateRotation(ref _xRotation, ref _yRotation);
                break;
            default:
                standardFpControlStrategy.UpdateRotation(ref _xRotation, ref _yRotation);
                break;
        }

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}