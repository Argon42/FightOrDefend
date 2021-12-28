using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    private const float HideOffset = 0.01f;

    [SerializeField] private float duration = 0.4f;
    [SerializeField] private float up;
    [SerializeField] private float left;
    [SerializeField] private float right;

    private Sequence _animation;

    public void Show()
    {
        TryStopAnimation();

        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.localPosition = Vector3.zero;
        _animation = DOTween.Sequence()
            .Insert(0, transform.DOScale(Vector3.one, duration));
    }

    public void Hide()
    {
        TryStopAnimation();

        _animation = DOTween.Sequence()
            .Insert(0, transform.DOScale(Vector3.zero, duration))
            .Insert(0, transform.DOMove(new Vector3(Random.Range(left, right), up, HideOffset), duration));
        _animation.onComplete += () => gameObject.SetActive(false);
    }

    private void TryStopAnimation()
    {
        if (_animation != null && _animation.IsPlaying())
            _animation.Kill();
    }
}