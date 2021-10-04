using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class Background : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _alpha;

    [SerializeField] private Image _image;

    public void Show(float duration, bool immediate = false, UnityAction Finished = null)
    {
        DoFade(_alpha, duration, immediate);
    }

    public void Hide(float duration, bool immediate = false, UnityAction Finished = null)
    {
        DoFade(0f, duration, immediate);
    }

    private void DoFade(float value, float duration, bool immediate = false, UnityAction Finished = null)
    {
        Sequence sequence = DOTween.Sequence();

        //sequence.Append(_image.DOFade(0, 0));
        sequence.Append(_image.DOFade(value, immediate ? 0f : duration));

        sequence.AppendCallback(() =>
        {
            Finished?.Invoke();
        });
    }
}