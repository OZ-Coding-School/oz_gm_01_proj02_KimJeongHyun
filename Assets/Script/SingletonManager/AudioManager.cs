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

    private Dictionary<BGMType, AudioSource> _BGMPlayer = new Dictionary<BGMType, AudioSource>();
    private Dictionary<SFXType, AudioSource> _SFXPlayer = new Dictionary<SFXType, AudioSource>();

    private AudioSource _currBGMSource;


    protected override void Init()
    {
        base.Init();
        LoadBGMPlayer();
        LoadSFXPlayer();
    }

    private void Start()
    {
        SetSFXLoop(SFXType.PeashooterLoop, true);
        SetSFXLoop(SFXType.TombMove, true);
    }

    private void LoadBGMPlayer()
    {
        for (int i = 0; i < (int)BGMType.COUNT; i++)
        {
            var audioName = ((BGMType)i).ToString();
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

            _BGMPlayer[(BGMType)i] = newAudioSource;
        }
    }

    private void LoadSFXPlayer()
    {
        for (int i = 0; i < (int)SFXType.COUNT; i++)
        {
            var audioName = ((SFXType)i).ToString();
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

            _SFXPlayer[(SFXType)i] = newAudioSource;
        }
    }

    public void SetVolume(SliderTypeE type, float val)
    {
        string name = type.ToString();

        float vol = val <= 0.0001f ? -80f : Mathf.Log10(val) * 16f;

        mainMix.SetFloat(name, vol);
    }

    public void PlayBGM(BGMType bgm)
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

    public void PlaySFX(SFXType sfx)
    {

        if (!_SFXPlayer.ContainsKey(sfx))
        {
            return;
        }
        _SFXPlayer[sfx].Play();
    }

    public void StopSFX(SFXType sfx) 
    {
        if (!_SFXPlayer.ContainsKey(sfx))
        {
            return;
        }
        _SFXPlayer[sfx].Stop();
    }

    public void PauseSFX(SFXType sfx)
    {
        if (!_SFXPlayer.ContainsKey(sfx))
        {
            return;
        }
        _SFXPlayer[sfx].Pause();
    }

    public void UnPauseSFX(SFXType sfx)
    {
        if (!_SFXPlayer.ContainsKey(sfx))
        {
            return;
        }
        _SFXPlayer[sfx].UnPause();
    }

    public void StopAllSFX()
    {
        for (int i = 0; i < (int)SFXType.COUNT; i++)
        {
            _SFXPlayer[(SFXType)i].Stop();
        }
    }

    public void SetSFXLoop(SFXType type, bool bol)
    {
        _SFXPlayer[type].loop = bol;
    }

    public float GetSFXLength(SFXType type)
    {
        return _SFXPlayer[type].clip.length;
    }
}