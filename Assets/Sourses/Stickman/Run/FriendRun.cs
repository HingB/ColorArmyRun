public class FriendRun : DefaultRun
{
    public override void Move()
    {
        _animator.SetTrigger(AnimatorStickmanController.Trigger.Run);
    }

}
