using System.Collections.Generic;
using UnityEngine;

public class ParticlesController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> _particles;
    [field: SerializeField] public ParticleSystem[] _heads { get; private set; }

    public void PlayParticle()
    {
        foreach (var head in _heads)
            head.Play();
    }

    public void ChangeColor(Color color)
    {
        for (int i = 0; i < _particles.Count; i++)
            _particles[i].startColor = color;
    }
}