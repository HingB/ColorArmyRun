using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private UnityEvent _onTimeOut;
    [SerializeField] private int _time;
    [SerializeField] private bool _startOnEnable;

    public void StartCount()
    {
        StartCoroutine(CountDown(_time));

        if (_text != null)
            StartCoroutine(Count());
    }

    private IEnumerator CountDown(float time)
    {
        yield return new WaitForSeconds(time);
        _onTimeOut?.Invoke();
    }

    private IEnumerator Count()
    {
        for (int i = _time; i >= 0; i--)
        {
            _text.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
    }

    private void OnEnable()
    {
        if (_startOnEnable)
            StartCount();
    }
}
