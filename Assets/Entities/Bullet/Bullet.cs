using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    #region Unity API
    private void Update()
    {
        switch (_currentState)
        {
            case States.Default:
                Move();
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _damageable = collision.transform.GetComponent<IDamageable>();

        if (_damageable != null)
        {
            _damageable.TakeDamage(collision.contacts[0].point, transform.forward, _strength);
        }

        Destroy();
    }

    public void OnEnable()
    {
        _rigidbody.isKinematic = false;
        _collider.enabled = true;
        _mesh.SetActive(true);

        _liveCoroutine = StartCoroutine(LiveCoroutine());

        ChangeState(States.Default);
    }
    #endregion

    #region Vars
    private enum States
    {
        Default,
        Destroying,
        Destroyed,
    }

    private States _currentState = States.Default;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private GameObject _mesh;
    [SerializeField] private ParticleSystem _particle;

    [SerializeField] private float _maxLiveDuration;
    private Coroutine _liveCoroutine;
    
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _strength;

    [SerializeField] private float _destroyDuration;

    private IDamageable _damageable;

    public event UnityAction BeforeDestroyed;
    public event UnityAction AfterDestroyed;
    #endregion

    #region Methods
    private void Move()
    {
        _rigidbody.velocity = transform.forward * _moveSpeed;
    }

    private void ChangeState(States state)
    {
        _currentState = state;
    }

    private void Destroy()
    {
        StopCoroutine(_liveCoroutine);

        BeforeDestroyed?.Invoke();

        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _mesh.SetActive(false);

        _particle.Play();

        ChangeState(States.Destroying);
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_destroyDuration);

        AfterDestroyed?.Invoke();

        gameObject.SetActive(false);
    }

    private IEnumerator LiveCoroutine()
    {
        yield return new WaitForSeconds(_maxLiveDuration);

        Destroy();
    }
    #endregion
}