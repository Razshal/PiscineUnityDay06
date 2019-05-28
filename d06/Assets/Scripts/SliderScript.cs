using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour {
    private Slider slider;
    public bool isIncreased;
    public float decraseValue = 0.1f;

    // Use this for initialization
    void Start () {
        slider = gameObject.GetComponent<Slider>();
    }

    public void IncreaseDetection(float val)
    {
        if (slider.value < slider.maxValue)
            slider.value += val;
    }

    private void Update()
    {
        if (!isIncreased && slider.value > slider.minValue)
            slider.value -= decraseValue;

        if (Input.GetKey(KeyCode.LeftShift)
            && (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0))
            IncreaseDetection(0.5f);
    }
}
