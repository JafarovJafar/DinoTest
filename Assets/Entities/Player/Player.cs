using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region Vars
    [SerializeField] private Waypoint[] _waypoints;

    [SerializeField] private float _moveSpeed;

    [SerializeField] private int _currentWaypointIndex;
    [SerializeField] private int _targetWaypointIndex;

    private Waypoint CurrentWaypoint => _waypoints[_currentWaypointIndex];
    private Waypoint TargetWaypoint => _waypoints[_targetWaypointIndex];

    private Vector3 _moveVector;
    private Quaternion _goalRotation;

    [SerializeField] private Transform _rootTransform;

    [SerializeField] private Animator _animator;

    [SerializeField] private Weapon _currentWeapon;

    [SerializeField] private ObjectPool _bulletsPool;

    private GameObject _bulletGO;

    private readonly string _idleAnimationName = "Idle";
    private readonly string _runAnimationName = "Run";

    public event UnityAction Finished;
    #endregion

    #region Methods
    public void StartMovement()
    {
        StartCoroutine(MainCoroutine());
    }

    private IEnumerator MainCoroutine() //убрал из update, чтобы не было ненужных проверок на if
    {
        while (true)
        {
            yield return WaitForCanPassCoroutine();
            yield return MoveCoroutine();

            if (_targetWaypointIndex == _waypoints.Length - 1)
            {
                PlayAnimation(_idleAnimationName);

                Finished?.Invoke();
                StopAllCoroutines();
            }
            else
            {
                SetNextWaypoint();
            }

            yield return null;
        }
    }

    private IEnumerator WaitForCanPassCoroutine()
    {
        if (!CurrentWaypoint.CanPass)
        {
            PlayAnimation(_idleAnimationName);
        }

        while (!CurrentWaypoint.CanPass)
        {
            yield return null;
        }
    }

    private IEnumerator MoveCoroutine()
    {
        PlayAnimation(_runAnimationName);

        while (true)
        {
            _moveVector = TargetWaypoint.transform.position - transform.position;

            if (_moveVector.magnitude > _moveSpeed * Time.deltaTime)
            {
                _moveVector.Normalize();
                _moveVector *= _moveSpeed;
                _moveVector *= Time.deltaTime;
            }

            transform.Translate(_moveVector);
            _goalRotation = Quaternion.LookRotation(_moveVector, transform.up);
            _goalRotation.x = 0;
            _goalRotation.z = 0;
            _rootTransform.rotation = _goalRotation;

            if (transform.position == TargetWaypoint.transform.position)
            {
                break;
            }

            yield return null;
        }
    }

    private void SetNextWaypoint()
    {
        _targetWaypointIndex++;
        _currentWaypointIndex = _targetWaypointIndex - 1;
    }

    private void PlayAnimation(string animationName)
    {
        _animator.Play(animationName);
    }

    public void TryShoot(Vector3 targetPosition)
    {
        if (_currentWeapon.CanShoot)
        {
            _bulletGO = _bulletsPool.GetItem();
            _bulletGO.transform.position = _currentWeapon.MuzzleTransform.position;
            _bulletGO.transform.LookAt(targetPosition);

            _currentWeapon.StartReloading();
        }
    }
    #endregion
}