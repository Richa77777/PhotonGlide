using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private Vector3 _moveDirection;

    private void OnEnable()
    {
        DifficultyController.Instance.OnChange += ChangeSpeed;
        ChangeSpeed();
    }

    private void OnDisable()
    {
        DifficultyController.Instance.OnChange -= ChangeSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(_moveDirection * _moveSpeed);
    }

    private void ChangeSpeed()
    {
        _moveSpeed = DifficultyController.Instance.ObjectsSpeed;
    }
}