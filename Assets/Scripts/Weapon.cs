using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool CanShoot => _canShoot;
    public float ReloadTime => _reloadTime;
    public Transform MuzzleTransform => _muzzleTransform;

    [SerializeField] private float _reloadTime;
    [SerializeField] private Transform _muzzleTransform;

    private bool _canShoot = true;

    public void StartReloading()
    {
        _canShoot = false;

        StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        yield return new WaitForSeconds(_reloadTime);
        _canShoot = true;
    }
}