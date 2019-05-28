using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionScript : MonoBehaviour {
    private SliderScript sliderScript;
    public float detectionZoneValue = 0.1f;

    private void Start()
	{
        sliderScript = GameObject.Find("Slider").GetComponent<SliderScript>();
	}

	private void OnTriggerStay(Collider other)
	{
        sliderScript.IncreaseDetection(detectionZoneValue);
        sliderScript.isIncreased = true;
	}

	private void OnTriggerExit(Collider other)
	{
        sliderScript.isIncreased = false;
	}
}
