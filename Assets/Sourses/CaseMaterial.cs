using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color", menuName = "Colors", order = 51)]
public class CaseMaterial : ScriptableObject
{
    [field: SerializeField] public Color Color { get; private set; }
    [field: SerializeField] public Color DissolveMainColor { get; private set; }
    [field: SerializeField] public Color DissolveColor { get; private set; }
    [field: SerializeField] public Material Friend { get; private set; }
    [field: SerializeField] public Material BrushHandle { get; private set; }
}
