using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    [SerializeField] private Sound _sound;

    private AudioSource _sourse;
    private float _defaultValue;

    public Sound Sound => _sound;

    private void Awake()
    {
        _sourse = GetComponent<AudioSource>();
        _defaultValue = _sourse.volume;
    }

    public void Play()
    {
        _sourse.Play();
    }

    public void Mute()
    {
        _sourse.volume = 0;
    }

    public void UnMute()
    {
        _sourse.volume = _defaultValue;
    }
}
