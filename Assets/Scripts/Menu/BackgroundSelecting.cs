using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSelecting : MonoBehaviour
{
    [SerializeField] private List<Sprite> _backgrounds = new List<Sprite>();

    private int _currentSelectedBackground;
    private Image _backgroundImage;

    private void Awake()
    {
        _backgroundImage = GetComponent<Image>();

        _currentSelectedBackground = PlayerPrefs.GetInt("Background", 0);
        UpdateSelectedBackground();
    }

    public void NextBackground()
    {
        _currentSelectedBackground = Mathf.Clamp(_currentSelectedBackground + 1, 0, _backgrounds.Count - 1);
        UpdateSelectedBackground();
    }

    public void PreviousBackground()
    {
        _currentSelectedBackground = Mathf.Clamp(_currentSelectedBackground - 1, 0, _backgrounds.Count - 1);
        UpdateSelectedBackground();
    }

    private void UpdateSelectedBackground()
    {
        PlayerPrefs.SetInt("Background", _currentSelectedBackground);
        _backgroundImage.sprite = _backgrounds[PlayerPrefs.GetInt("Background", 0)];
    }
}
