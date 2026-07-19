using UnityEngine;
using Ami.BroAudio;
using System.Collections.Generic;

public enum SoundType
{
    BGM,
    ImpactNormal,
    ImpactHeavy,
    Win,
    Fail
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    [SerializeField] private SoundID soundBGM;
    [SerializeField] private SoundID soundImpactNormal;
    [SerializeField] private SoundID soundImpactHeavy;
    [SerializeField] private SoundID soundWin;
    [SerializeField] private SoundID soundFail;

    private Dictionary<SoundType, SoundID> soundMappingDictionary;

    private void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        soundMappingDictionary = new Dictionary<SoundType, SoundID>
        {
            { SoundType.BGM, soundBGM },
            { SoundType.ImpactNormal, soundImpactNormal },
            { SoundType.ImpactHeavy, soundImpactHeavy },
            { SoundType.Win, soundWin },
            { SoundType.Fail, soundFail },
        };
    }

    private void Start()
    {
        PlayAudio(SoundType.BGM);
    }

    public void PlayAudio(SoundType sound)
    {
        if (soundMappingDictionary.TryGetValue(sound, out SoundID id)) BroAudio.Play(id);
        else Debug.Log($"Enum SoundType '{sound}' gk ada");
    }

    public void StopAudio(SoundType sound)
    {
        if (soundMappingDictionary.TryGetValue(sound, out SoundID id)) BroAudio.Stop(id);
    }
}