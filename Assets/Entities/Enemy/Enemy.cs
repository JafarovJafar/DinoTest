using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IDamageable
{
    private void Start()
    {
        _currentHealth = _defaultHealth;
    }

    public event UnityAction Destroyed;
    [SerializeField] private float _defaultHealth;
    private float _currentHealth;

    [SerializeField] private CharacterController _controller;
    [SerializeField] private Animator _animator;

    private readonly string _deathAnimationName = "Death";

    [SerializeField] private HealthBar _healthBar;

    public void TakeDamage(Vector3 point, Vector3 direction, float strength)
    {
        _currentHealth -= strength;

        _healthBar.SetFloat(_currentHealth / _defaultHealth * 100f);

        if (_currentHealth <= 0)
        {
            Destroy();
            _healthBar.Disable();
        }
    }

    private void Destroy()
    {
        _controller.enabled = false;
        _animator.Play(_deathAnimationName);

        Destroyed?.Invoke();
    }
}