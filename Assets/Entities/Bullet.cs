﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    #region Unity API
    private void Start()
    {

    }

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
        Destroy();
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

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _strength;

    private Vector3 _moveVector = new Vector3(0, 0, 1); // тут захардкодил потому что пуля всегда будет двигаться вперед и нет смысла выносить куда-то его получение

    [SerializeField] private float _destroyDuration;

    public event UnityAction BeforeDestroyed;
    public event UnityAction AfterDestroyed;
    #endregion

    #region Methods
    private void Move()
    {
        _rigidbody.velocity = _moveVector * _moveSpeed;
    }

    private void ChangeState(States state)
    {
        _currentState = state;
    }

    private void Destroy()
    {
        BeforeDestroyed?.Invoke();

        _rigidbody.isKinematic = true;
        _collider.enabled = false;

        ChangeState(States.Destroying);
        StartCoroutine(DestroyCoroutine());
    }

    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(_destroyDuration);

        AfterDestroyed?.Invoke();

        gameObject.SetActive(false);
    }
    #endregion
}