using System;
using System.Collections;
using UnityEngine;

public class DifficultyController : MonoBehaviour
{
    public static DifficultyController Instance;
    public Action OnChange;

    [SerializeField] private float _objectsSpeed = 0.1f;
    [SerializeField] private float _maxSpeed = 1.0f;
    [SerializeField] private float _increaseStep = 0.1f; 
    [SerializeField] private float _increaseInterval = 1.0f;

    public float ObjectsSpeed => _objectsSpeed;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        OnChange?.Invoke();
        StartCoroutine(IncreaseSpeedOverTime());
    }

    public void SetObjectsSpeed(float objectsSpeed)
    {
        _objectsSpeed = objectsSpeed;
        OnChange?.Invoke();
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (_objectsSpeed < _maxSpeed)
        {
            yield return new WaitForSeconds(_increaseInterval);
            SetObjectsSpeed(Mathf.Min(_objectsSpeed + _increaseStep, _maxSpeed));
        }
    }
}
