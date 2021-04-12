using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStates : MonoBehaviour
{
    [SerializeField] private UnityEvent onStartGame;
    [SerializeField] private UnityEvent onEndGame;
    
    public void StartGame() => onStartGame?.Invoke();
    public void EndGame() => onEndGame?.Invoke();
    public void QuitFromApp() => Application.Quit();
}