using System.Collections.Generic;
using System.Linq;
using Assets.Sourses.Player.Bruse.Case;
using RunnerMovementSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BrushCase : MonoBehaviour
{
    [SerializeField] private Brush _startBrush;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private float _range = 0.57f;
    [SerializeField] private Brush _prefab;
    [SerializeField] private GameObject _losePanel;
    private PanelOpener _panelOpener;
    private float _timeLife;

    private BrushCaseEngine _caseEngine;
    private CaseMaterial _caseMaterial;
    public List<Brush> _brushes { get; private set; }
    public event UnityAction<int> CountChanged;
    public event UnityAction<Brush> BrushRemoved;
    public event UnityAction<Brush> BrushAdded;
    private MovementSystem _movementSystem;
    public int Count => _brushes.Count;
    public IReadOnlyList<Brush> Brushes => _brushes;

    private void OnEnable()
    {
        Player.MaterialChanged += ChangeCaseMaterial;
        Brush.BrishRemoved += RemoveBrush;
    }

    private void OnDisable()
    {
        Player.MaterialChanged -= ChangeCaseMaterial;
        Brush.BrishRemoved -= RemoveBrush;

    }

    private void ChangeCaseMaterial(CaseMaterial value)
    {
        _caseMaterial = value;
    }

    private void Awake()
    {
        _brushes = new List<Brush>() { _startBrush };
        _caseEngine = new BrushCaseEngine(_centerPosition, _brushes);
        _caseEngine.Range = _range;
        _caseEngine.SetBrushPosition();
        _panelOpener = new PanelOpener();
    }

    private void Start()
    {
        BrushAdded?.Invoke(_startBrush);
        CountChanged?.Invoke(Count);
        _movementSystem = FindObjectOfType<MovementSystem>();
    }

    private void Update()
    {
        _timeLife += Time.deltaTime;
    }

    public void AddBrush()
    {
        if (_brushes.Count >= 5)
            return;
        Vector3 newBrushPosition = FindNewPosition();
        var brush = Instantiate(_prefab, newBrushPosition, _prefab.transform.rotation, transform);
        _brushes.Add(brush);
        BrushAdded?.Invoke(brush);
        ChangeColor(brush);
        _caseEngine.SetBrushPosition();
        CountChanged?.Invoke(Count);
    }

    public void RemoveBrush(Brush brush)
    {
        _brushes.Remove(brush);
        _caseEngine.RemoveAnimation(brush);
        _caseEngine.SetBrushPosition();
        CountChanged?.Invoke(Count);
        BrushRemoved?.Invoke(brush);
        Faild();

    }

    public void RemoveBrush()
    {
        _brushes[0].Die();
        Faild();
    }

    private void Faild()
    {
        if (Count == 0)
        {
            _panelOpener.OpenPanel(_losePanel);
            _movementSystem.enabled = false;
            return;
        }
    }

    private Vector3 FindNewPosition()
    {
        var nextPosition = _caseEngine.CreatePoint(_brushes.Count + 1);
        Vector3 newBrushPosition = nextPosition[nextPosition.Count - 1];
        var parents = _brushes[0].gameObject.GetComponentsInParent<Transform>().ToList();
        parents.RemoveAt(0);

        foreach (var item in parents)
            newBrushPosition = item.localPosition + newBrushPosition;
        return newBrushPosition;
    }

    private void ChangeColor(Brush brush)
    {
        brush.BrushColorChanger.ChangeCaseMaterial(_caseMaterial);
        brush.BrushColorChanger.ChangeColorSimple();
    }

    private void OnValidate()
    {
        if (_caseEngine == null)
            return;
        _caseEngine.Range = _range;
        _caseEngine.SetBrushPosition();
    }
}