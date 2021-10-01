using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private Camera _camera;
    [SerializeField] private float _shootDistance;

    private RaycastHit _hit;
    private Ray _ray;
    private Vector3 _shootPosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(_ray, out _hit, _shootDistance);

            if (_hit.transform != null)
            {
                _shootPosition = _hit.point;
            }
            else
            {
                _shootPosition = _ray.origin + _ray.direction * _shootDistance;
            }

            _player.TryShoot(_shootPosition);
        }
    }
}