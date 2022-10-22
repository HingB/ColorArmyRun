using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrushCaseFriend
{
    private readonly CaseItem[] _cases;

    public BrushCaseFriend(Brush brush, float radius)
    {
        Brush = brush;
        Friends = new List<Friend>();
        _cases = new CaseItem[2];
        _cases[0] = new CaseItem(brush.Point1, radius);
        _cases[1] = new CaseItem(brush.Point2, radius);
    }

    public Brush Brush { get; private set; }
    public List<Friend> Friends { get; }

    public void Update()
    {
        foreach (var item in _cases)
            item.Update();
    }

    public void AddFriend(Friend friend)
    {
        var minFriendCaseItem = _cases.OrderBy(x => x.Items.Count).First();
        minFriendCaseItem.Add(friend.transform);
        friend.Init(minFriendCaseItem.Head);
        friend.LookAt();
        friend.ChangeColor();
        Friends.Add(friend);
    }

    public bool TryRemoveFriend(Friend friend)
    {
        foreach (var item in _cases)
        {
            if (item.TryRemove(friend.transform))
            {
                Friends.Remove(friend);
                return true;
            }
        }

        return false;
    }

    public Friend ReturnFriend()
    {
        if (Friends.Count <= 0)
            return null;
        var maxFriendCase = _cases.OrderBy(x => x.Items.Count).Last();
        var friend = maxFriendCase.Items[maxFriendCase.Items.Count - 1].Transform.GetComponent<Friend>();
        maxFriendCase.TryRemove(friend.transform);
        Friends.Remove(friend);
        return friend;
    }

    public void UpdateRadius(float radius)
    {
        foreach (var item in _cases)
            item.UpdateRadius(radius);
    }
}