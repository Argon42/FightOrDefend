using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

public class CardAnimation : MonoBehaviour
{
    [SerializeField] private float duration = 0.4f;

    private TweenerCore<Vector3, Vector3, VectorOptions> _animation;

    public void Show()
    {
        TryStopAnimation();

        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        _animation = transform.DOScale(Vector3.one, duration);
    }

    public void Hide()
    {
        TryStopAnimation();

        _animation = transform.DOScale(Vector3.zero, duration);
        _animation.onComplete += () => gameObject.SetActive(false);
    }

    private void TryStopAnimation()
    {
        if (_animation != null && _animation.IsPlaying()) _animation.Kill();
    }
}