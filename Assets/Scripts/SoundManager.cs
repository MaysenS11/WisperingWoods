using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public enum MusicMode {
        InGameMusic,
        InMenuMusic,
        InHouseMusic,
        ShrineBuildMusic,
        InCreditsMusic
    }

    [Header("Music Objects")]
    public GameObject inGameMusicObject;
    public GameObject inMenuMusicObject;
    public GameObject inHouseMusicObject;
    public GameObject shrineBuildMusicObject;
    public GameObject inCreditsMusicObject;

    private Dictionary<MusicMode, GameObject> _musicObjects = new Dictionary<MusicMode, GameObject>();

    private MusicMode _currentMusicMode;
    public MusicMode CurrentMusicMode {
        get => _currentMusicMode;
        set {
            //Debug.Log($"Switching music mode to: {value}");
            foreach (var kvp in _musicObjects) {
                kvp.Value.SetActive(kvp.Key == value);
            }
            _currentMusicMode = value;
        }
    }

    private FMOD.Studio.Bus _musicBus;
    private FMOD.Studio.Bus _sfxBus;

    void Awake() {
        if (Instance != null) {
            Debug.LogWarning("Multiple SoundManager instances detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);

        _musicBus = FMODUnity.RuntimeManager.GetBus("bus:/Music");
        _sfxBus = FMODUnity.RuntimeManager.GetBus("bus:/SFX");

        _musicObjects[MusicMode.InGameMusic] = inGameMusicObject;
        _musicObjects[MusicMode.InMenuMusic] = inMenuMusicObject;
        _musicObjects[MusicMode.InHouseMusic] = inHouseMusicObject;
        _musicObjects[MusicMode.ShrineBuildMusic] = shrineBuildMusicObject;
        _musicObjects[MusicMode.InCreditsMusic] = inCreditsMusicObject;
    }

    void Start() {
        CurrentMusicMode = MusicMode.InMenuMusic;
    }

    public void SetMusicVolume(float volume) {
        _musicBus.setVolume(volume);
    }

    public void SetSFXVolume(float volume) {
        _sfxBus.setVolume(volume);
    }
}
