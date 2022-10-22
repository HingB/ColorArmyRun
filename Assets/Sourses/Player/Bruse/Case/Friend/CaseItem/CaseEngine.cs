using System.Collections.Generic;

internal class CaseEngine
{
    public readonly List<Item> CaseObjects;
    private List<ItemMover> _objectMoves = new List<ItemMover>();
    public CaseEngine()
    {
        CaseObjects = new List<Item>();
    }

    public void Add(Item caseObject)
    {
        CaseObjects.Add(caseObject);

        if (CaseObjects.Count > 1)
        {
            _objectMoves.Add(new ItemMover(CaseObjects[CaseObjects.Count - 2], CaseObjects[CaseObjects.Count - 1]));
        }
    }

    public void Remove(int number)
    {
        CaseObjects.RemoveAt(number);
        if (number == 0)
        {
            if (_objectMoves.Count != 0)
                _objectMoves.RemoveAt(number);

            if (CaseObjects.Count > 1)
                _objectMoves.Insert(number, new ItemMover(CaseObjects[number], CaseObjects[number + 1]));
        }
        else if (number == CaseObjects.Count)
        {
            _objectMoves.RemoveAt(number - 1);
        }
        else
        {
            _objectMoves.RemoveRange(number - 1, 2);
            _objectMoves.Insert(number - 1, new ItemMover(CaseObjects[number - 1], CaseObjects[number]));
            // 1 2 3 $4 5
            //  1 2  $3 $4
        }
    }

    public void Update()
    {
        foreach (var objectMove in _objectMoves)
        {
            objectMove.Move();
        }
    }
}