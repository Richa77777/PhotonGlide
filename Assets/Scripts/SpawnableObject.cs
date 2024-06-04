using System;
using UnityEngine;

[RequireComponent(typeof(ObjectRotation), typeof(ObjectMover))]
public class SpawnableObject : MonoBehaviour
{
    public Action<GameObject> OnDisableAction;

    private void OnDisable()
    {
        OnDisableAction?.Invoke(gameObject);
    }
}
