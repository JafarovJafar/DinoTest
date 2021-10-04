using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class UI : MonoBehaviour
{
    [SerializeField] private Transform _startPanelTransform;
    [SerializeField] private Transform _finishPanelTransform;

    [SerializeField] private float _distanceMultiplier;
    [SerializeField] private float _moveSpeed;

    [SerializeField] private CanvasGroup _mainCanvasGroup;
    [SerializeField] private Background _background;

    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;

    public event UnityAction StartButtonPressed;
    public event UnityAction RestartButtonPressed;

    private void Start()
    {
        _startButton.onClick.AddListener(() =>
        {
            StartButtonPressed?.Invoke();
        });

        _restartButton.onClick.AddListener(() =>
        {
            RestartButtonPressed?.Invoke();
        });
    }

    public void ShowLogo(UnityAction Finished = null)
    {
        _mainCanvasGroup.blocksRaycasts = true;

        _background.Show(100f / _moveSpeed);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_startPanelTransform.DOLocalMoveY(32.27f * _distanceMultiplier, 16f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(-13.12f * _distanceMultiplier, 12f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(4.63f * _distanceMultiplier, 16f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(-1.64f * _distanceMultiplier, 15f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(0.58f * _distanceMultiplier, 14f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(-0.2f * _distanceMultiplier, 15f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(0f, 12f / _moveSpeed));

        sequence.AppendCallback(() =>
        {
            Finished?.Invoke();
        });
    }

    public void HideLogo(UnityAction Finished = null)
    {
        _background.Hide(100f / _moveSpeed);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_startPanelTransform.DOLocalMoveY(-0.2f * _distanceMultiplier, 12f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(-1.64f * _distanceMultiplier, 14f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(4.63f * _distanceMultiplier, 15f / _moveSpeed));
        sequence.Append(_startPanelTransform.DOLocalMoveY(1000, 59f / _moveSpeed));

        sequence.AppendCallback(() =>
        {
            _mainCanvasGroup.blocksRaycasts = false;
            Finished?.Invoke();
        });
    }

    public void ShowLevelFinish(UnityAction Finished = null)
    {
        _background.Show(100f / _moveSpeed);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_finishPanelTransform.DOLocalMoveY(1000f, 0f));
        sequence.Append(_finishPanelTransform.DOLocalMoveY(0f, 100f / _moveSpeed));

        sequence.AppendCallback(() =>
        {
            _mainCanvasGroup.blocksRaycasts = true;
            Finished?.Invoke();
        });
    }
}