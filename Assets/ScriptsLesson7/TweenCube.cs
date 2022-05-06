using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(Renderer))]
public class TweenCube : MonoBehaviour
{
    [SerializeField]
    private float _duration;
    [SerializeField]
    private Vector3 _endValue;
    [SerializeField]
    private Color _color;
    private Material _material;
    [SerializeField]
    private PathType _pathType = PathType.Linear;
    [SerializeField]
    private Transform[] _points;
    private List<Vector3> _pointPosition = new List<Vector3>();
    [SerializeField]
    private int _countLoops;
    [SerializeField]
    private float _positionX;
    [SerializeField]
    private float _endScale;

    private void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }
    private void Start()
    {
        foreach (var point in _points)
            _pointPosition.Add(point.position);
        transform.DOPath(_pointPosition.ToArray(), _duration, _pathType);
    }
    private void ComplexTween()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(_positionX,
        _duration).SetLoops(_countLoops).SetEase(Ease.InExpo));
        sequence.Insert(0, transform.DOScale(_endScale, _duration));
        sequence.Insert(1, transform.DOJump(Vector3.forward, 5, 5, _duration));
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            ComplexTween();
    }

}
