using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioEnabler : MonoBehaviour
{

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _parameterName = "...Volume";

    [Space(5f), Header("Sprites")]
    [SerializeField] private Sprite _enabledSprite; 
    [SerializeField] private Sprite _disabledSprite;

    [Space(5f), Header("Colors")]
    [SerializeField] private Color32 _enabledColor = new Color32(0, 0, 0, 255);
    [SerializeField] private Color32 _disabledColor = new Color32(0, 0, 0, 255);

    private Image _buttonImage;
    
    private void Awake()
    {
        _buttonImage = GetComponent<Image>();
        _mixer.GetFloat(_parameterName, out float volume);

        if (volume == -80)
        {
            DisableAudio();
        }
        else
        {
            EnableAudio();
        }
    }

    public void ButtonClick()
    {
        _mixer.GetFloat(_parameterName, out float volume);

        if (volume >= 0)
        {
            DisableAudio();
        }
        else if (volume < 0)
        {
            EnableAudio();
        }
    }

    private void EnableAudio()
    {
        _buttonImage.sprite = _enabledSprite;
        _buttonImage.color = _enabledColor;

        _mixer.SetFloat(_parameterName, 0);
    }

    private void DisableAudio()
    {
        _buttonImage.sprite = _disabledSprite;
        _buttonImage.color = _disabledColor;

        _mixer.SetFloat(_parameterName, -80);
    }
}
