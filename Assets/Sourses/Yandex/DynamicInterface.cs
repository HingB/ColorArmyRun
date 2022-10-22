using System;
using UnityEngine;

public class DynamicInterface : MonoBehaviour
{
    [SerializeField] private GameObject[] _objects;

    private bool _play = false;

    private void Start()
    {
        GameStage.Instance.GameStarted += OnGameStart;
        GameStage.Instance.GameStop += OnFinishGame;
    }

    private void OnDisable()
    {
        GameStage.Instance.GameStarted -= OnGameStart;
        GameStage.Instance.GameStop -= OnFinishGame;
    }

    private void OnFinishGame()
    {
        _play = false;
        OnChanges();
    }

    private void OnGameStart()
    {
        _play = true;
        OnChanges();
        GameStage.Instance.GameStarted -= OnGameStart;
    }

    private void OnChanges()
    {
        foreach (var gameObject in _objects)
            gameObject.SetActive(!_play);
    }
}