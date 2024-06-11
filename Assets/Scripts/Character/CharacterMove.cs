using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;
    [SerializeField] private float _movementSpeed;

    private IInputService _inputService;
    private Camera _camera;

    private void Awake() => _inputService = Game.InputService;

    private void Start() => _camera = Camera.main;

    private void Update()
    {
        Vector3 movementVector = Vector3.zero;

        if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
        {
            movementVector = _camera.transform.TransformDirection(_inputService.Axis);
            movementVector.y = 0;
            movementVector.Normalize();

            transform.forward = movementVector;
        }

        movementVector += Physics.gravity;
        _controller.Move(_movementSpeed * movementVector * Time.deltaTime);
    }
}