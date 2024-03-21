using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private CharacterController _controller;

    public Transform PlayerTransform { get; private set; }

    private Vector3 _mousePoint;
    private Controls _controls;
    private Camera _mainCamera;
    private Vector3 _directionMove;

    [Inject]
    private void Construct(CameraFollow camera)
    {
        _mainCamera = camera.GetComponent<Camera>();
        _controls = new();
    }

    private void OnEnable() => 
        _controls.Gameplay.Enable();

    private void OnDisable() => 
        _controls.Gameplay.Disable();

    private void Update()
    {
        ReadMousePoint();

        Vector2 moveInput = _controls.Gameplay.Movement.ReadValue<Vector2>();

        _directionMove = moveInput.x * _mainCamera.transform.right + moveInput.y * _mainCamera.transform.forward;
        _directionMove.y = 0;

        _controller.Move(_player.PlayerCharacteristics.MoveSpeed.Value * Time.deltaTime * _directionMove);
        RotationPlayer();
    }

    private void ReadMousePoint()
    {
        Ray mousePointRay = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Physics.Raycast(mousePointRay, out RaycastHit mouseRaycastHit);
        _mousePoint = mouseRaycastHit.point;
    }
    private void RotationPlayer()
    {
        var dist = _mousePoint - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dist, transform.TransformDirection(Vector3.up));
        transform.rotation = new Quaternion(0, rotation.y, 0, rotation.w);
    }
}
