using UnityEngine;

public class Scope : MonoBehaviour
{
    [SerializeField] private BonusLevelInput _input;
    [SerializeField] private GameResults _results;

    private void MoveTo(Vector3 mousePosition)
    {
        if (_results.Ended)
        {
            gameObject.SetActive(false);
        }

        transform.position = mousePosition;
    }

    private void OnEnable()
    {
        _input.MousePostionChanged += MoveTo;
    }

    private void OnDisable()
    {
        _input.MousePostionChanged -= MoveTo;
    }
}