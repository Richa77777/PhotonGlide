using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _backgrounds = new List<GameObject>();

    [SerializeField] private ObjectPool _backgroundPool;
    [SerializeField] private float _scrollSpeed = 1.0f;
    [SerializeField] private Transform _spawnPoint;

    private List<GameObject> _activeBackgrounds = new List<GameObject>();

    private void Awake()
    {
        _backgroundPool.SetPrefab(_backgrounds[PlayerPrefs.GetInt("Background", 0)]);

        for (int i = 0; i < 3; i++)
        {
            GameObject background = _backgroundPool.GetObject();
            background.transform.position = _spawnPoint.position + new Vector3(i * 21.0f, 0, 0);
            _activeBackgrounds.Add(background);
        }
    }

    private void OnEnable()
    {
        DifficultyController.Instance.OnChange += ChangeScrollSpeed;
        ChangeScrollSpeed();
    }

    private void OnDisable()
    {
        DifficultyController.Instance.OnChange -= ChangeScrollSpeed;
    }

    private void Update()
    {
        MoveBackgrounds();
    }

    private void MoveBackgrounds()
    {
        for (int i = _activeBackgrounds.Count - 1; i >= 0; i--)
        {
            var background = _activeBackgrounds[i];
            background.transform.Translate(Vector3.left * _scrollSpeed * Time.deltaTime);

            if (background.transform.position.x < -23.0f)
            {
                var lastBackground = _activeBackgrounds[_activeBackgrounds.Count - 1];
                background.transform.position = new Vector3(lastBackground.transform.position.x + 21.0f, background.transform.position.y, background.transform.position.z);

                _activeBackgrounds.RemoveAt(i);
                _activeBackgrounds.Add(background);
            }
        }
    }

    public void SetScrollSpeed(float speed)
    {
        _scrollSpeed = speed;
    }

    private void ChangeScrollSpeed()
    {
        _scrollSpeed = DifficultyController.Instance.ObjectsSpeed * 33.33f;
    }
}