using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider musicSlider;
    public Slider masterSlider;

    [Header("Audio")]
    public AudioSource musicSource; // Only for music
    private float lastMusicVolume = 1f;
    private float lastMasterVolume = 1f;

    private bool isMusicMuted = false;
    private bool isMasterMuted = false;

    [Header("Buttons")]
    public Button musicButton;
    public Button volumeButton;

    [Header("Cross Images")]
    public GameObject musicCrossImage; // Cross icon for music mute
    public GameObject volumeCrossImage; // Cross icon for master mute

    void Start()
    {
        // Load saved settings from PlayerPrefs
        lastMusicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        lastMasterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        isMusicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
        isMasterMuted = PlayerPrefs.GetInt("MasterMuted", 0) == 1;

        // Apply loaded volumes
        musicSlider.value = lastMusicVolume;
        masterSlider.value = lastMasterVolume;

        // Apply mute states
        musicSource.volume = isMusicMuted ? 0f : lastMusicVolume;
        AudioListener.volume = isMasterMuted ? 0f : lastMasterVolume;

        // Update visuals
        UpdateMusicVisual();
        UpdateMasterVisual();

        // Add listeners
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicButton.onClick.AddListener(ToggleMusicMute);
        volumeButton.onClick.AddListener(ToggleMasterMute);
    }

    private void SetMusicVolume(float volume)
    {
        lastMusicVolume = volume;
        if (!isMusicMuted)
            musicSource.volume = volume;

        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    private void SetMasterVolume(float volume)
    {
        lastMasterVolume = volume;
        if (!isMasterMuted)
            AudioListener.volume = volume;

        PlayerPrefs.SetFloat("MasterVolume", volume);
    }

    private void ToggleMusicMute()
    {
        isMusicMuted = !isMusicMuted;
        musicSource.volume = isMusicMuted ? 0f : lastMusicVolume;
        PlayerPrefs.SetInt("MusicMuted", isMusicMuted ? 1 : 0);
        UpdateMusicVisual();
    }

    private void ToggleMasterMute()
    {
        isMasterMuted = !isMasterMuted;
        AudioListener.volume = isMasterMuted ? 0f : lastMasterVolume;
        PlayerPrefs.SetInt("MasterMuted", isMasterMuted ? 1 : 0);
        UpdateMasterVisual();
    }

    private void UpdateMusicVisual()
    {
        if (musicCrossImage != null)
            musicCrossImage.SetActive(isMusicMuted);
    }

    private void UpdateMasterVisual()
    {
        if (volumeCrossImage != null)
            volumeCrossImage.SetActive(isMasterMuted);
    }
}
