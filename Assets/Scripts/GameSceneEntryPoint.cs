using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerInput _playerInput;

    [SerializeField] private UI _ui;

    private void Start()
    {
        DOTween.Init();

        _ui.ShowLogo();
        _ui.StartButtonPressed += () =>
        {
            _ui.HideLogo(() =>
            {
                _playerInput.Enable();
                StartPlayerMovement();
            });
        };
        _ui.RestartButtonPressed += Restart;

        _player.Finished += () =>
        {
            _playerInput.Disable();
            _ui.ShowLevelFinish();
        };
    }

    private void StartPlayerMovement()
    {
        _player.StartMovement();
    }

    private void Restart()
    {
        SceneManager.LoadScene(CONSTANTS.GAME_SCENE_NAME);
    }
}