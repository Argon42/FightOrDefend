using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class GameLogic : MonoBehaviour
{
    [SerializeField] private Transform parentForCards;
    [SerializeField] private List<Card> cards = new List<Card>();

    #region Game events

    [SerializeField, FoldoutGroup("Game Events")]
    private UnityEvent onStart;

    [SerializeField, FoldoutGroup("Game Events")]
    private UnityEvent<Card> onNewCard;

    [SerializeField, FoldoutGroup("Game Events")]
    private UnityEvent onCorrectCard;

    [SerializeField, FoldoutGroup("Game Events")]
    private UnityEvent onIncorrectCard;

    [SerializeField, FoldoutGroup("Game Events")]
    private UnityEvent onEndGame;

    #endregion

    private Card _currentCard;
    private readonly Dictionary<Card, List<Card>> _cardsPool = new Dictionary<Card, List<Card>>();

    public bool IsPlayed { get; private set; }

    public Card CurrentCard
    {
        get => _currentCard;
        set
        {
            _currentCard = value;
            onNewCard?.Invoke(_currentCard);
        }
    }

    public void StartGame()
    {
        if (IsPlayed) return;

        IsPlayed = true;
        CurrentCard = CreateCard();
        onStart?.Invoke();
    }

    public void EndGame()
    {
        if (IsPlayed == false) return;

        CurrentCard.Hide();
        IsPlayed = false;
        onEndGame?.Invoke();
    }

    public void UseCard(CardType cardType)
    {
        if (CurrentCard.CardType == cardType)
            onCorrectCard?.Invoke();
        else
            onIncorrectCard?.Invoke();

        CurrentCard.Hide();

        if (IsPlayed)
            CurrentCard = CreateCard();
    }

    private Card CreateCard()
    {
        var types = cards.Select(c => c.CardType).Distinct().ToArray();
        var type = types[Random.Range(0, types.Length)];
        var cardPrefab = cards.Where(c => c.CardType == type).RandomElement();

        if (_cardsPool.ContainsKey(cardPrefab) == false)
            _cardsPool.Add(cardPrefab, new List<Card>());

        var cardFromPool = _cardsPool[cardPrefab].FirstOrDefault(c => c.IsEnabled == false);
        if (cardFromPool == default)
        {
            cardFromPool = InstantiateCard(cardPrefab);
            _cardsPool[cardPrefab].Add(cardFromPool);
        }

        cardFromPool.Show();

        return cardFromPool;
    }

    private Card InstantiateCard(Card cardPrefab)
    {
        return Instantiate(cardPrefab, parentForCards ? parentForCards : transform);
    }
}