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

        musicSlider.RegisterValueChangedCallback(e => {
            musicValue.text = Mathf.RoundToInt(e.newValue).ToString();
        });

        sfxSlider.RegisterValueChangedCallback(e => {
            sfxValue.text = Mathf.RoundToInt(e.newValue).ToString();
        });
    }
}