using UnityEngine;

public class MusicManager : MonoBehaviour {
    #region Constants
    private const string US_MUSIC_VOLUME = "UsMusicVolume";
    #endregion Constants

    #region Variables
    [SerializeField] private FloatVariableSO volume;
    [SerializeField] private OptionCanvas optionCanvas;
    private AudioSource musicSource;
    #endregion Variables

    #region Methods
    private void Awake()
    {
        float savedVolume = Mathf.Clamp(PlayerPrefs.GetFloat(US_MUSIC_VOLUME, 0.02f), 0, 0.04f);
        volume.SetValue(savedVolume, false);
        musicSource = GetComponent<AudioSource>();
        musicSource.volume = savedVolume;
    }
    private void OnEnable()
    {
        volume.OnChanged += Volume_OnChanged;
        optionCanvas.OnCloseOption += OptionCanvas_OnCloseOption;
    }

    private void OnDisable()
    {
        volume.OnChanged -= Volume_OnChanged;
        optionCanvas.OnCloseOption -= OptionCanvas_OnCloseOption;
    }

    private void Volume_OnChanged()
    {
        musicSource.volume = volume.GetValue();
    }

    private void OptionCanvas_OnCloseOption()
    {
        PlayerPrefs.SetFloat(US_MUSIC_VOLUME, volume.GetValue());
        PlayerPrefs.Save();
    }

    #endregion Methods
}
