using UnityEngine;

internal class EnemyDie : IDyeing
{
    private readonly ParticlesController _particleController;
    private readonly Renderer _render;
    private readonly Transform _transform;
    private Enemy _enemy;

    public EnemyDie(ParticlesController particleController, Renderer render, Transform transform)
    {
        _particleController = particleController;
        _render = render;
        _transform = transform;
        _enemy = transform.GetComponent<Enemy>();
    }

    public void Die()
    {
        var parents = _transform.GetComponentsInParent<Transform>();
        var mainParent = this._transform.parent;
        foreach (var item in _particleController._heads)
            item.transform.parent = mainParent;
        _render.enabled = false;
        _particleController.ChangeColor(_enemy.CaseMaterial.Color);
        _particleController.PlayParticle();
    }
}
