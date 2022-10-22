using UnityEngine;

internal class FriendDie : IDyeing
{
    private readonly ParticlesController _particleController;
    private readonly Renderer _render;
    private readonly Transform _transform;
    private Friend _friend;

    public FriendDie(ParticlesController particleController, Renderer render, Transform transform)
    {
        _particleController = particleController;
        _render = render;
        _transform = transform;
        _friend = transform.GetComponent<Friend>();
    }

    public void Die()
    {
        if (_transform == null)
            return;
        var parents = _transform.GetComponentsInParent<Transform>();
        var mainParent = parents[parents.Length - 1];
        foreach (var item in _particleController._heads)
            item.transform.parent = mainParent;
        _render.enabled = false;
        _particleController.ChangeColor(_friend.CaseMaterial.Color);
        _particleController.PlayParticle();
    }
}
