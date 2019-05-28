using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {
    private Slider slider;
    public bool isIncreased;
    public float decraseValue = 0.1f;
    public AudioSource alarm;
    private bool alarmPlaying = false;

    // Use this for initialization
    void Start () {
        slider = gameObject.GetComponent<Slider>();
    }

    public void IncreaseDetection(float val)
    {
        if (slider.value < slider.maxValue)
            slider.value += val;
        if (!alarmPlaying && slider.value >= 75)
        {
            alarmPlaying = true;
            alarm.Play();
        }
        if (alarmPlaying && slider.value < 75)
        {
            alarmPlaying = false;
            alarm.Stop();
        }
        if (slider.value >= 100)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (!isIncreased && slider.value > slider.minValue)
            IncreaseDetection(-decraseValue);

        if (Input.GetKey(KeyCode.LeftShift)
            && (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0))
            IncreaseDetection(0.5f);
    }
}
