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
    [SerializeField]private Slider sliderMaster;

    private void Awake()
    {
        audioMixer.GetFloat("Master", out float masterValue);
        audioMixer.GetFloat("BGMVolume", out float bgmValue);
        audioMixer.GetFloat("SFXVolume", out float sfxValue);
        Debug.Log(bgmValue + " " + sfxValue + " " + masterValue);
        sliderBGM.value = bgmValue;
        sliderSFX.value = sfxValue;
        sliderMaster.value = masterValue;

        sliderMaster.onValueChanged.AddListener(value =>
        {
            Debug.Log(value);
            audioMixer.SetFloat("MasterVolume", value);
            if(value == sliderMaster.minValue)
            {
                audioMixer.SetFloat("MasterVolume", -80f);
            }
        });
        sliderBGM.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat("BGMVolume", value);
            if(value == sliderBGM.minValue)
            {
                audioMixer.SetFloat("BGMVolume", -80f);
            }
        });
        sliderSFX.onValueChanged.AddListener(value =>
        {
            audioMixer.SetFloat("SFXVolume", value);
            if(value == sliderSFX.minValue)
            {
                audioMixer.SetFloat("SFXVolume", -80f);
            }
        });
    }
}
