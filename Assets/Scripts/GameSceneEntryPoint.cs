using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Waypoint _firstWaypoint;
    [SerializeField] private Player _player;

    private void Start()
    {
        _player.StartMovement();

        _player.Finished += () =>
        {
            Debug.Log(90);
        };
    }
}