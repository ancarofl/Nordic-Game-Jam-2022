using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [SerializeField] AudioSource _bassSource;
    [SerializeField] AudioSource _drumSource;
    [SerializeField] AudioSource _pianoSource;
    [SerializeField] AudioSource _shakerSource;

    public float BassVolume { get => _bassSource.volume; set { _bassSource.volume = value; } }
    public float DrumVolume { get => _drumSource.volume; set { _drumSource.volume = value; } }
    public float PianoVolume { get => _pianoSource.volume; set { _pianoSource.volume = value; } }
    public float ShakerVolume { get => _shakerSource.volume; set { _shakerSource.volume = value; } }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
