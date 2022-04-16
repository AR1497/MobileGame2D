using JetBrains.Annotations;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class GunAbility : IAbility
{
    private readonly Rigidbody2D _viewPrefab;
    private readonly float _projectileSpeed;
    public GunAbility(
    [NotNull] string viewPath,
    float projectileSpeed)
    {
        _viewPrefab = ResourceLoader.LoadObject<Rigidbody2D>(new ResourcePath{ PathResource = viewPath });
        if (_viewPrefab == null) throw new InvalidOperationException($"{nameof(GunAbility)} view requires { nameof(Rigidbody2D) } component!");
        _projectileSpeed = projectileSpeed;
    }
    public void Apply(IAbilityActivator activator)
    {
        var projectile = Object.Instantiate(_viewPrefab);
        projectile.AddForce(activator.GetViewObject().transform.right * _projectileSpeed,
        ForceMode2D.Force);
    }
}