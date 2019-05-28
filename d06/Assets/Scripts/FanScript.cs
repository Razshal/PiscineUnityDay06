using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour {
    public DetectionScript detectionScript;
    public GameObject particles;

	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerStay(Collider other)
	{
        if (Input.GetKeyDown(KeyCode.E))
        {
            detectionScript.detectionZoneValue = 0.2f;
            particles.SetActive(true);
        }
	}
}
