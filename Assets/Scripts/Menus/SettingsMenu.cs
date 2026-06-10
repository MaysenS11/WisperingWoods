using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenu : MonoBehaviour {
    public UIDocument menu;

    void OnEnable() {
        var root = menu.rootVisualElement;
        var musicSlider = root.Q<Slider>("musicSlider");
        var musicValue = root.Q<Label>("musicValue");
        var sfxSlider = root.Q<Slider>("sfxSlider");
        var sfxValue = root.Q<Label>("sfxValue");

        musicValue.text = Mathf.RoundToInt(musicSlider.value).ToString();
        sfxValue.text = Mathf.RoundToInt(sfxSlider.value).ToString();
        SoundManager.Instance.SetMusicVolume(musicSlider.value / 100f);
        SoundManager.Instance.SetSFXVolume(sfxSlider.value / 100f);

        musicSlider.RegisterValueChangedCallback(e => {
            SoundManager.Instance.SetMusicVolume(e.newValue / 100f);
            musicValue.text = Mathf.RoundToInt(e.newValue).ToString();
        });

        sfxSlider.RegisterValueChangedCallback(e => {
            SoundManager.Instance.SetSFXVolume(e.newValue / 100f);
            sfxValue.text = Mathf.RoundToInt(e.newValue).ToString();
        });
    }
}