using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private List<SpawnableObject> _objectPrefabs;
    [SerializeField] private float _spawnXPosition = 0f;
    [SerializeField] private Vector2 _spawnYRange = new Vector2(-5f, 5f);
    [SerializeField] private float _spawnInterval = 2f;

    private List<ObjectPool> _objectPools = new List<ObjectPool>();

    private void Start()
    {
        InitializePools();
        StartCoroutine(SpawnObjects());
    }

    private void OnEnable()
    {
        foreach (ObjectPool pool in _objectPools)
        {
            foreach (GameObject obj in pool.PoolQueue)
            {
                SpawnableObject spObj = obj.GetComponent<SpawnableObject>();
                spObj.OnDisableAction += pool.ReturnObject;
            }
        }
    }

    private void OnDisable()
    {
        foreach (ObjectPool pool in _objectPools)
        {
            foreach (GameObject obj in pool.PoolQueue)
            {
                SpawnableObject spObj = obj.GetComponent<SpawnableObject>();
                spObj.OnDisableAction -= pool.ReturnObject;
            }
        }
    }

    private void InitializePools()
    {
        foreach (SpawnableObject prefab in _objectPrefabs)
        {
            GameObject poolObject = new GameObject(prefab.name + " PoolQueue");
            poolObject.transform.SetParent(transform);
            ObjectPool pool = poolObject.AddComponent<ObjectPool>();
            pool.SetPrefab(prefab.gameObject);
            pool.InitializePool(5);
            _objectPools.Add(pool);
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnInterval);

            int randomIndex = Random.Range(0, _objectPools.Count);
            ObjectPool selectedPool = _objectPools[randomIndex];

            float randomYPosition = Random.Range(_spawnYRange.x, _spawnYRange.y);

            GameObject spawnedObject = selectedPool.GetObject();
            spawnedObject.transform.position = new Vector2(_spawnXPosition, randomYPosition);
        }
    }
}