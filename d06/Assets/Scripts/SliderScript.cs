using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {
    private Slider slider;
    public bool isIncreased;
    public float decraseValue = 0.1f;
    public AudioSource alarm;
    private AudioSource uiAudioSource;
    public AudioClip ambientNormal;
    public AudioClip ambientAlert;
    private bool alarmPlaying = false;
    private PlayerScript playerScript;


    // Use this for initialization
    void Start () {
        slider = gameObject.GetComponent<Slider>();
        uiAudioSource = gameObject.GetComponent<AudioSource>();
        PlaySound(ambientNormal);
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
    }

    void PlaySound(AudioClip clip)
    {
        uiAudioSource.Stop();
        uiAudioSource.clip = clip;
        uiAudioSource.Play();
    }

    public void IncreaseDetection(float val)
    {
        if (slider.value < slider.maxValue)
            slider.value += val;
        if (!alarmPlaying && slider.value >= 75)
        {
            alarmPlaying = true;
            alarm.Play();
            PlaySound(ambientAlert);
            playerScript.SetHint("ALERT");
        }
        if (alarmPlaying && slider.value < 75)
        {
            alarmPlaying = false;
            alarm.Stop();
            PlaySound(ambientNormal);
            playerScript.SetHint("Probably just a rat ...");
        }
        if (slider.value >= 100)
        {
            playerScript.SetHint("Yo loose ...");
            playerScript.Reload();
        }
    }

    private void Update()
    {
        if (!isIncreased && slider.value > slider.minValue)
            IncreaseDetection(-decraseValue);
    }
}
