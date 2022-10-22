using UnityEngine;
using System;
using System.Linq;

public class GameSoundsPlayer : MonoBehaviour
{
    [SerializeField] private GameSound[] _sounds;

    public static GameSoundsPlayer Instance { get; private set; }

    public void PlaySound(Sound sound)
    {
        _sounds.Where(p => p.Sound == sound).FirstOrDefault()?.Play();
    }

    public void MuteAll()
    {
        foreach (var sound in _sounds)
            sound.Mute();
    }

    public void UnMuteAll()
    {
        foreach (var sound in _sounds)
            sound.UnMute();
    }

    private void Awake()
    {
        Instance = this;
    }
}

public enum Sound
{
    Win,
    Lose,
    Money,
    Background,
    Die,
    BossDie,
    PickUp,
    Bulk
}