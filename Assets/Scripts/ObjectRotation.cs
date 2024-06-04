using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    [SerializeField] private Transform _transformForRotation;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private Vector3 _rotationSide;

    [SerializeField] private bool _isPlayer;

    private void FixedUpdate()
    {
        _transformForRotation.Rotate(_rotationSide * _rotationSpeed);
    }
    private void OnEnable()
    {
        DifficultyController.Instance.OnChange += ChangeRotationSpeed;
        ChangeRotationSpeed();
    }

    private void OnDisable()
    {
        DifficultyController.Instance.OnChange -= ChangeRotationSpeed;
    }
    private void ChangeRotationSpeed()
    {
        if (_isPlayer == true)
        {
            _rotationSpeed = DifficultyController.Instance.ObjectsSpeed * 33.33f;
        }
    }
}
