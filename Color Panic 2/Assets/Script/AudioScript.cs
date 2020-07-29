using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    [SerializeField] private AudioClip[] listOfMusic;
    private static AudioScript _instance;
    public static AudioScript Instance { get { return _instance; } }
    public AudioSource AudioSource;

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.transform);
            AudioSource = GetComponent<AudioSource>();
            ChangeAudioClip(0);
            return;

        }
    }

    public void ChangeAudioClip(int Nb)
    {
        AudioSource.clip = listOfMusic[Nb];
        AudioSource.Play();
    }
}
