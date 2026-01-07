using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{

    public AudioMixer mainMix;
    public Transform BGMtrs;
    public Transform SFXtrs;
    public AudioMixerGroup BGM;
    public AudioMixerGroup SFX;

    private const string AUDIO_PATH_BGM = "Audio/BGM";
    private const string AUDIO_PATH_SFX = "Audio/SFX";

    private Dictionary<EnumData.BGM, AudioSource> _BGMPlayer = new Dictionary<EnumData.BGM, AudioSource>();
    private Dictionary<EnumData.SFX, AudioSource> _SFXPlayer = new Dictionary<EnumData.SFX, AudioSource>();

    private AudioSource _currBGMSource;


    protected override void Init()
    {
        base.Init();
        LoadBGMPlayer();
        LoadSFXPlayer();
    }

    private void LoadBGMPlayer()
    {
        for (int i = 0; i < (int)EnumData.BGM.COUNT; i++)
        {
            var audioName = ((EnumData.BGM)i).ToString();
            var pathStr = $"{AUDIO_PATH_BGM}/{audioName}";
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip;

            if (!audioClip)
            {
                continue;
            }

            var newGameObject = new GameObject(audioName);
            var newAudioSource = newGameObject.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = true;
            newAudioSource.volume = 1f;
            newAudioSource.playOnAwake = false;
            newAudioSource.outputAudioMixerGroup = BGM;
            newGameObject.transform.parent = BGMtrs;

            _BGMPlayer[(EnumData.BGM)i] = newAudioSource;
        }
    }

    private void LoadSFXPlayer()
    {
        for (int i = 0; i < (int)EnumData.SFX.COUNT; i++)
        {
            var audioName = ((EnumData.SFX)i).ToString();
            var pathStr = $"{AUDIO_PATH_SFX}/{audioName}";
            var audioClip = Resources.Load(pathStr, typeof(AudioClip)) as AudioClip;

            if (!audioClip)
            {
                continue;
            }

            var newGameObject = new GameObject(audioName);
            var newAudioSource = newGameObject.AddComponent<AudioSource>();
            newAudioSource.clip = audioClip;
            newAudioSource.loop = false;
            newAudioSource.volume = 1f;
            newAudioSource.playOnAwake = false;
            newAudioSource.outputAudioMixerGroup = SFX;
            newGameObject.transform.parent = SFXtrs;

            _SFXPlayer[(EnumData.SFX)i] = newAudioSource;
        }
    }

    public void SetVolume(EnumData.SliderType type, float val)
    {
        string name = type.ToString();

        float vol = val <= 0.0001f ? -80f : Mathf.Log10(val) * 16f;

        mainMix.SetFloat(name, vol);
    }

    public void PlayBGM(EnumData.BGM bgm)
    {

        if (_currBGMSource)
        {
            _currBGMSource.Stop();
            _currBGMSource = null;
        }

        if (!_BGMPlayer.ContainsKey(bgm))
        {
            return;
        }
        _currBGMSource = _BGMPlayer[bgm];
        _currBGMSource.Play();
    }

    public void PauseBGM()
    {

        if (_currBGMSource)
        {
            _currBGMSource.Pause();
        }
    }

    public void ResumeBGM()
    {

        if (_currBGMSource)
        {
            _currBGMSource.UnPause();
        }
    }

    public void StopBGM()
    {

        if (_currBGMSource)
        {
            _currBGMSource.Stop();
        }
    }

    public void PlaySFX(EnumData.SFX sfx)
    {

        if (!_SFXPlayer.ContainsKey(sfx))
        {
            return;
        }
        _SFXPlayer[sfx].Play();
    }
}