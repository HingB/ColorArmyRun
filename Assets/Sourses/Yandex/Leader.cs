using TMPro;
using UnityEngine;

public class Leader : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _result;

    public void Fill(string name, string result)
    {
        _name.text = name;
        _result.text = result;
    }
}
