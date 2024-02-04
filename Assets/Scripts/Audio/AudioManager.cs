using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;


[Serializable]
public class Sound//存储单个音频的各个参数
{
    [Header("音频剪辑")]
    public AudioClip clip;
    [Header("音频分组")]
    public AudioMixerGroup outputGroup;
    [Header("音频音量")]
    [Range(0,1)]//限制范围
    public float volume;
    [Header("音频是否开局播放")]
    public bool playOnAwake;
    [Header("音频是否循环播放")]
    public bool loop;
}
public class AudioManager : MonoSingleton<AudioManager>//音频管理器，存储所有音频并且可以播放和停止
{
    public Sound sound;
    //存储所有的音频信息，需要自己在该脚本挂载对象的检查器中添加
    public List<Sound> soundList;
    //每一个音频剪辑的名称对应一个音频组件
    private Dictionary<string, AudioSource> audiosDic = new Dictionary<string, AudioSource>();

    private void Start()
    {
        foreach(var sound in soundList)
        {
            GameObject obj = new GameObject(sound.clip.name);
            obj.transform.SetParent(transform);
            AudioSource source = obj.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.outputAudioMixerGroup = sound.outputGroup;
            source.volume = sound.volume;
            source.playOnAwake = sound.playOnAwake;
            source.loop = sound.loop;

            if(sound.playOnAwake)
            {
                source.Play();
            }
            audiosDic.Add(sound.clip.name, source);
        }
    }

    //播放某一个音频，传入的name名要与音频文件的名称一致
    public static void PlayAudio(string name, bool isWait = false)
    {
        Debug.Log("播放音频");
        if(!instance.audiosDic.ContainsKey(name))
        {
            Debug.LogWarning($"名为{name}的音频不存在");
            return;
        }
        if(isWait)
        {
            if(!instance.audiosDic[name].isPlaying)
            {
                instance.audiosDic[name].Play();
            }
        }
        else
        {
            instance.audiosDic[name].Play();
        }
    }
    //停止某一音频的播放
    public static void StopAudio(string name)
    {
        if(!instance.audiosDic.ContainsKey(name))
        {
            Debug.LogWarning($"名为{name}的音频不存在");
            return;
        }
        instance.audiosDic[name].Stop();
    }
    //检查某一音频是否在播放，正在播放返回true
    public static bool IsPlaying(string name)
    {
        if(instance.audiosDic.TryGetValue(name, out AudioSource a))
        {
            if(a.isPlaying)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
