using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] private CardType cardType;
    [SerializeField] private UnityEvent onShow;
    [SerializeField] private UnityEvent onHide;

    public CardType CardType => cardType;
    public virtual bool IsEnabled => gameObject.activeSelf;

    public virtual void Show() => onShow?.Invoke();

    public virtual void Hide() => onHide?.Invoke();
}