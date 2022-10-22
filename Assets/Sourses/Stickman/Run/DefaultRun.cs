using UnityEngine;

public class DefaultRun : IMovalble
{
    protected Animator _animator;

    public virtual void Move()
    {
        _animator.Play(AnimatorStickmanController.Trigger.Run);
    }

    public virtual void Stop()
    {
        _animator.Play(AnimatorStickmanController.Trigger.Idle);
    }

    public void Init(Animator animator)
    {
        _animator = animator;
    }
}
