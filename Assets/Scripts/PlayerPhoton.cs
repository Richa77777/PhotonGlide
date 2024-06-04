using TMPro;
using UnityEngine;
using System.Collections;

public class PlayerPhoton : MonoBehaviour
{
    [SerializeField] private GameObject _loseTab;
    [SerializeField] private GameObject _electricSphere; // Added serialized field for electric sphere

    [Header("Move Variables")]
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _yLimit = 0f;

    [Header("Score Variables")]
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _score = 0;

    [Header("Hearts Variables")]
    [SerializeField] private TextMeshProUGUI _heartsText;
    [SerializeField] private int _hearts = 5;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip _shockEffect;
    [SerializeField] private AudioClip _scoreEffect;

    private Camera _camera;
    private Vector2 _touchStartPosition;
    private Vector2 _touchEndPosition;
    private Vector2 _touchDeltaPosition;

    private AudioSource _audioSource;
    private Vector3 _originalElectricSphereScale;

    private void Awake()
    {
        Application.targetFrameRate = 60;

        _camera = Camera.main;
        _audioSource = GetComponent<AudioSource>();
        _originalElectricSphereScale = _electricSphere.transform.localScale;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _touchStartPosition = _camera.ScreenToWorldPoint(touch.position);
                _touchEndPosition = _touchStartPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                _touchEndPosition = _camera.ScreenToWorldPoint(touch.position);
                _touchDeltaPosition = _touchEndPosition - _touchStartPosition;

                Vector2 newPosition = new Vector2(transform.position.x, transform.position.y + _touchDeltaPosition.y * _moveSpeed);

                transform.position += (Vector3)newPosition - transform.position;
                transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -_yLimit, _yLimit)); // Limit

                _touchStartPosition = _touchEndPosition;
            }
        }
    }

    private void UpdateHeartsText()
    {
        _heartsText.text = _hearts.ToString();
    }

    private void UpdateScoreText()
    {
        _scoreText.text = _score.ToString();
    }

    private void Lose()
    {
        _loseTab.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obstacle>())
        {
            collision.gameObject.SetActive(false);

            _audioSource.PlayOneShot(_shockEffect);

            _hearts -= 1;
            _hearts = Mathf.Clamp(_hearts, 0, int.MaxValue);
            UpdateHeartsText();

            if (_hearts == 0)
            {
                Lose();
                this.enabled = false;
            }

            StartCoroutine(ScaleElectricSphere());
        }

        if (collision.CompareTag("ScoreTrigger"))
        {
            _audioSource.PlayOneShot(_scoreEffect);

            _score += 1;
            _score = Mathf.Clamp(_score, 0, int.MaxValue);
            UpdateScoreText();

            if (_score > PlayerPrefs.GetInt("Record", 0))
            {
                PlayerPrefs.SetInt("Record", _score);
            }
        }
    }

    private IEnumerator ScaleElectricSphere()
    {
        float duration = 0.1f;
        Vector3 targetScale = _originalElectricSphereScale * 1.5f;
        Vector3 initialScale = _electricSphere.transform.localScale;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            _electricSphere.transform.localScale = Vector3.Lerp(initialScale, targetScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _electricSphere.transform.localScale = targetScale;

        elapsed = 0f;

        while (elapsed < duration)
        {
            _electricSphere.transform.localScale = Vector3.Lerp(targetScale, _originalElectricSphereScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        _electricSphere.transform.localScale = _originalElectricSphereScale;
    }
}