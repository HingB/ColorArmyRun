using UnityEngine;

public class StickmanAnimator : IMovalble, IAttacking, IDyeing
{
    private readonly IDyeing _diyAnimation;
    private readonly IMovalble _moveAnimator;
    private Animator _animator;

    public StickmanAnimator(IDyeing animation, IMovalble animator)
    {
        _diyAnimation = animation;
        _moveAnimator = animator;
    }

    public StickmanAnimator(IDyeing animation)
    {
        _diyAnimation = animation;
        _moveAnimator = new DefaultRun();
        var animator = (DefaultRun)_moveAnimator;
    }

    public void InitAnimator(Animator animator)
    {
        _animator = animator;
        if (_moveAnimator is DefaultRun)
        {
            var moveAnimator = (DefaultRun)_moveAnimator;
            moveAnimator.Init(animator);
        }
    }

    public void Attack()
    {
        _animator.Play(AnimatorStickmanController.Trigger.Attack);
    }

    public void Move() => _moveAnimator.Move();

    public void Stop() => _moveAnimator.Stop();

    public void Joi()
    {
        _animator.Play(AnimatorStickmanController.Trigger.Joi);
    }

    public void Die() => _diyAnimation.Die();
    public void Stay() => _animator.Play(AnimatorStickmanController.Trigger.Stay);

    public void Hit() => _animator.SetTrigger("Hit");
    public void Mark() => _animator.SetTrigger("Mark");
}
