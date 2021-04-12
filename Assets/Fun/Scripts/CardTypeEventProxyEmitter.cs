using UnityEngine.Events;
using Zinnia.Event.Proxy;

public class CardTypeEventProxyEmitter : RestrictableSingleEventProxyEmitter<CardType, UnityEvent<CardType>>
{
    protected override object GetTargetToCheck() => Payload;
}