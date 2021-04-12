using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int startHealth;

    [SerializeField, Tooltip("Кол-во времени на карточку от текущего времени")]
    private AnimationCurve timeCurve = AnimationCurve.Constant(0, 100, 2);

    #region Events

    [SerializeField, FoldoutGroup("Events")]
    private UnityEvent<int> onScoreUpdated;

    [SerializeField, FoldoutGroup("Events")]
    private UnityEvent<int> onGameEnded;

    [SerializeField, FoldoutGroup("Events")]
    private UnityEvent<float> onTimeUpdatedNormalized;

    [SerializeField, FoldoutGroup("Events")]
    private UnityEvent<float> onHealthUpdatedNormalized;

    #endregion

    private int _health;
    private int _score;
    private float _allTime;
    private float _currentTime;

    #region Property

    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            onHealthUpdatedNormalized?.Invoke(Mathf.InverseLerp(0, startHealth, _health));
            if (_health <= 0)
                StopGame();
        }
    }

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            onScoreUpdated?.Invoke(_score);
        }
    }

    public float CurrentTime
    {
        get => _currentTime;
        private set
        {
            _currentTime = value;
            onTimeUpdatedNormalized?.Invoke(Mathf.InverseLerp(0, CurrentMaxTime, _currentTime));
            if (_currentTime <= 0)
                DecreaseHealth();
        }
    }

    public float AllTime
    {
        get => _allTime;
        private set => _allTime = value;
    }

    public float CurrentMaxTime { get; private set; }

    public bool IsSystemEnabled { get; private set; }

    #endregion

    public void StartSystem()
    {
        Health = startHealth;
        Score = 0;
        AllTime = 0;
        UpdateCurrentTime();

        IsSystemEnabled = true;
    }

    public void ChooseCorrectAnswer()
    {
        Score++;
        UpdateCurrentTime();
    }

    public void ChooseIncorrectAnswer()
    {
        Health--;
        UpdateCurrentTime();
    }

    private void UpdateCurrentTime()
    {
        CurrentTime = timeCurve.Evaluate(AllTime);
        CurrentMaxTime = CurrentTime;
    }

    private void StopGame()
    {
        onGameEnded?.Invoke(Score);
        IsSystemEnabled = false;
    }

    private void DecreaseHealth()
    {
        Health--;
        UpdateCurrentTime();
    }

    private void Update()
    {
        if (IsSystemEnabled == false) return;

        CurrentTime -= Time.deltaTime;

        AllTime += Time.deltaTime;
    }
}