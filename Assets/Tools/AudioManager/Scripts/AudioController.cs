using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] List<AudioClip> audios;
    [SerializeField][Range(0,100f)] float volume;
    AudioSource audioSource;
    public static AudioController Instance;
    Dictionary<string, AudioClip> useNameGetAudioClip = new Dictionary<string, AudioClip>();
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        for(int i = 0; i<audios.Count; i++)
        {
            useNameGetAudioClip.Add(audios[i].name, audios[i]);
        }
    }
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            PlayAudio(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            PlayAudio(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            PlayAudio(2);
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayAudio("audio1");
        }
        audioSource.volume = volume / 100f;
    }
    public void PlayAudio(int audioCode)
    {
        audioSource.clip = audios[audioCode];
        audioSource.Play();
    }
    public void PlayAudio(string audioName)
    {
        audioSource.clip = useNameGetAudioClip[audioName];
        audioSource.Play();
    }
    public void SetAudioVolume(float value)
    {
        volume = value;
    }
}
