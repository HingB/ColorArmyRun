using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameResults : MonoBehaviour
{
    [SerializeField] private BonusLevelReferee _referee;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

    private bool _ended;

    public event UnityAction OnEnded;
    public bool Ended => _ended;

    private void OnLose()
    {
        if (_ended)
            return;
        StartCoroutine(WaitAndOpen(_losePanel));
    }

    private void OnWin()
    {
        if (_ended)
            return;
        StartCoroutine(WaitAndOpen(_winPanel));
    }

    private IEnumerator WaitAndOpen(GameObject panel, float delay = 1)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(true);
        _ended = true;
        OnEnded?.Invoke();
    }

    private void OnEnable()
    {
        _referee.Won += OnWin;
        _referee.Lose += OnLose;
    }

    private void OnDisable()
    {
        _referee.Won -= OnWin;
        _referee.Lose -= OnLose;
    }
}
