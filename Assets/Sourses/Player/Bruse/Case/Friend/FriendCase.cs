using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FriendCase : MonoBehaviour
{
    [SerializeField] private BrushCase _brush;
    [Range(0, 2)]
    [SerializeField] private float _radius = 1;
    private List<BrushCaseFriend> _brushCaseFriends;

    private void OnEnable()
    {
        _brushCaseFriends = new List<BrushCaseFriend>();
        FollowFriendTrigger.FriendAdded += AddFriend;
        _brush.BrushAdded += AddBrushCases;
        _brush.BrushRemoved += RemoveBrush;
        Friend.FriendDie += RemoveFriend;
        Friend.FriendFight += RemoveFriend;
    }

    private void OnDisable()
    {
        FollowFriendTrigger.FriendAdded -= AddFriend;
        _brush.BrushAdded -= AddBrushCases;
        _brush.BrushRemoved -= RemoveBrush;
        Friend.FriendDie -= RemoveFriend;
        Friend.FriendFight += RemoveFriend;
    }

    private void LateUpdate()
    {
        foreach (var item in _brushCaseFriends)
        {
            item.Update();
        }
    }

    private void AddFriend(Friend friend) => AddFriends(friend);

    private void AddFriends(params Friend[] friends) => AddFriends((IEnumerable<Friend>)friends);

    public int GetFriendCount()
    {
        return FindObjectsOfType<Friend>().Where(friend => friend.Follow).Count();
    }

    private void AddFriends(IEnumerable<Friend> friends)
    {
        foreach (var friend in friends)
        {
            _brushCaseFriends.OrderBy(x => x.Friends.Count).First().AddFriend(friend);
            GameSoundsPlayer.Instance?.PlaySound(Sound.PickUp);
        }
    }

    private void AddBrushCases(Brush brush)
    {
        var newBrushCace = new BrushCaseFriend(brush, _radius);
        _brushCaseFriends.Add(newBrushCace);
        if (_brushCaseFriends.Count > 1)
        {
            var avarage = _brushCaseFriends.Select(x => x.Friends.Count).Average();
            while (newBrushCace.Friends.Count <= avarage)
            {
                var friend = _brushCaseFriends.OrderByDescending(x => x.Friends.Count)
                .ToList()
                .First()
                .ReturnFriend();
                if (friend == null)
                    return;
                newBrushCace.AddFriend(friend);
            }
        }
    }

    private void RemoveBrush(Brush brush)
    {
        for (int i = 0; i < _brushCaseFriends.Count; i++)
        {
            if (_brushCaseFriends[i].Brush == brush)
            {
                List<Friend> friends = _brushCaseFriends[i].Friends;
                _brushCaseFriends.RemoveAt(i);
                foreach (var friend in friends)
                {
                    GameSoundsPlayer.Instance?.PlaySound(Sound.Die);
                    friend.Die();
                }
            }
        }
    }

    private void RemoveFriend(Friend friend)
    {
        foreach (BrushCaseFriend brushFriendCase in _brushCaseFriends)
        {
            if (brushFriendCase.TryRemoveFriend(friend))
                return;
        }
    }

    private void OnValidate()
    {
        if (_brushCaseFriends == null)
            return;
        foreach (var brushFriendCase in _brushCaseFriends)
        {
            brushFriendCase.UpdateRadius(_radius);
        }
    }
}
