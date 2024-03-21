using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float RotationAngleX = 50;
    [SerializeField] private float Ditance = 7;
    [SerializeField] private float OffsetY = 5;
    [SerializeField] private float _speed = 2;

    private Transform _folowing;

    public void Follow(Transform following) =>
        _folowing = following;

    private void LateUpdate()
    {
        if (_folowing == null)
            return;

        Quaternion rotation = Quaternion.Euler(RotationAngleX, 0, 0);
        Vector3 position = rotation * new Vector3(0, 0, -Ditance) + FollowingPointPosition();

        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, position, _speed * Time.deltaTime), rotation);
    }

    private Vector3 FollowingPointPosition()
    {
        Vector3 followingPosition = _folowing.position;
        followingPosition.y += OffsetY;
        return followingPosition;
    }
}
