using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FollowFriendTrigger : Trigger
{
    public static event UnityAction<Friend> FriendAdded;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Friend friend) && friend.Follow == false)
        {
            friend.Follow = true;
            friend.Animator.Move();
            friend.TrySpawnSkin();
            FriendAdded?.Invoke(friend);
        }
    }
}