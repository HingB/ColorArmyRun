using System.Collections.Generic;
using UnityEngine;

public class PositionCeator
{
    private List<Vector3[]> _positions;
    private int _currentPositionRow;
    private int _lenghtRow;
    private float _distant;
    private Vector3 _directionRow;
    private Vector3 _directionColumn;

    public PositionCeator(Vector3 startPosition, int lenghtRow, float distant, Vector3 directionRow, Vector3 directionColumn)
    {
        StartPosition = startPosition;
        _positions = new List<Vector3[]>();
        _currentPositionRow = 0;
        _lenghtRow = lenghtRow;
        _distant = distant;
        _directionRow = directionRow.normalized;
        _directionColumn = directionColumn.normalized;
    }

    public Vector3 StartPosition { get; private set; }
    public IReadOnlyList<IReadOnlyCollection<Vector3>> Positions => _positions;

    public Vector3 GetPosition()
    {
        if (_positions.Count == 0)
        {
            var rowPositions = CreateRowPosition(_lenghtRow);
            FillRowPosition(rowPositions, StartPosition);
            _positions.Add(rowPositions);
            return _positions[0][0];
        }

        if (_positions.Count >= 1)
        {
            if (_currentPositionRow >= _lenghtRow - 1)
            {
                _currentPositionRow = 0;
                StartPosition += _directionColumn * _distant;
                var rowPositions = CreateRowPosition(_lenghtRow);
                FillRowPosition(rowPositions, StartPosition);
                _positions.Add(rowPositions);
                return _positions[_positions.Count - 1][++_currentPositionRow];
            }
            else
            {
                return _positions[_positions.Count - 1][++_currentPositionRow];
            }
        }

        Debug.LogAssertion("Error");
        return Vector3.zero;
    }

    public void RemoveAllPosition()
    {
        _positions = new List<Vector3[]>();
        _currentPositionRow = 0;
    }

    private Vector3[] CreateRowPosition(int lenght) => new Vector3[lenght];

    private void FillRowPosition(Vector3[] rowPosition, Vector3 startPosition)
    {
        rowPosition[0] = startPosition;
        for (int i = 1; i < rowPosition.Length; i++)
            rowPosition[i] = CreatePosition(startPosition, i);
    }

    private Vector3 CreatePosition(Vector3 startPosition, int index) => startPosition + (_directionRow * _distant * index);
}
