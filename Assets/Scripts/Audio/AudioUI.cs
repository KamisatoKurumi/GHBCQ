using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    [SerializeField]private AudioMixer audioMixer;
    [SerializeField]private Slider sliderBGM;
    [SerializeField]private Slider sliderSFX;

    private void Awake()
    {
        audioMixer.GetFloat("BGM", out float bgmValue);
        audioMixer.GetFloat("SFX", out float sfxValue);
        sliderBGM.value = bgmValue;
        sliderSFX.value = sfxValue;

        sliderBGM.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat("BGM", value);
            if(value == sliderBGM.minValue)
            {
                audioMixer.SetFloat("BGM", -80f);
            }
        });
        sliderSFX.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat("SFX", value);
            if(value == sliderSFX.minValue)
            {
                audioMixer.SetFloat("SFX", -80f);
            }
        });
    }
}
