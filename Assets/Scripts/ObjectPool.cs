using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _initialSize = 10;
    private Queue<GameObject> _pool = new Queue<GameObject>();
    private bool _prefabSet = false;

    public List<GameObject> PoolQueue => new List<GameObject>(_pool);

    private void Start()
    {
        if (_prefab == null)
        {
            StartCoroutine(WaitForPrefab());
        }
        else
        {
            _prefabSet = true;
            InitializePool();
        }
    }

    private IEnumerator WaitForPrefab()
    {
        while (!_prefabSet)
        {
            yield return null;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        for (int i = 0; i < _initialSize; i++)
        {
            GameObject obj = Instantiate(_prefab, transform);
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }

    public void InitializePool(int size)
    {
        _initialSize = size;
        InitializePool();
    }

    public void SetPrefab(GameObject prefab)
    {
        _prefab = prefab;
        _prefabSet = true;
    }

    public GameObject GetObject()
    {
        if (!_prefabSet)
        {
            Debug.LogWarning("Prefab not set. Waiting for prefab...");
            StartCoroutine(WaitForPrefab());
            return null;
        }

        if (_pool.Count > 0)
        {
            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = Instantiate(_prefab, transform);
            obj.SetActive(true);
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        _pool.Enqueue(obj);
    }
}
