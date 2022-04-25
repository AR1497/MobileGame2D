using JetBrains.Annotations;
using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class GunAbility : IAbility
{
    private readonly Rigidbody2D _viewPrefab;
    private readonly float _projectileSpeed;
    private GameObject view;
    private float value;
    private float projectileSpeed;
    private readonly AbilityItemConfig _config;

    public GunAbility([NotNull] string viewPath,
    [NotNull] AbilityItemConfig config,
    float projectileSpeed)
    {
        _config = config;
        _viewPrefab = ResourceLoader.LoadObject<Rigidbody2D>(new ResourcePath{ PathResource = viewPath });
        if (_viewPrefab == null) throw new InvalidOperationException($"{nameof(GunAbility)} view requires { nameof(Rigidbody2D) } component!");
        _projectileSpeed = projectileSpeed;
    }

    public void Apply(IAbilityActivator activator)
    {
        var projectile = Object.Instantiate(_config.view).GetComponent<Rigidbody2D>();
        projectile.AddForce(activator.GetViewObject().transform.right * _config.value,
        ForceMode2D.Force);
    }
}