using System.Collections.Generic;
using UnityEngine;

public class ShootingWaypoint : Waypoint // кажется нейминг не самый удачный
{
    private void Start()
    {
        _canPass = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy tempEnemy = other.GetComponent<Enemy>();

        if (tempEnemy != null)
        {
            tempEnemy.Destroyed += () =>
            {
                _enemies.Remove(tempEnemy);

                if (_enemies.Count == 0)
                {
                    _canPass = true;
                }
            };

            _enemies.Add(tempEnemy);
        }
    }

    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
}