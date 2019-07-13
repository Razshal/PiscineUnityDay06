using UnityEngine;

public class FanScript : MonoBehaviour
{
    public DetectionScript detectionScript;
    public GameObject particles;

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            detectionScript.detectionZoneValue = 0.2f;
            particles.SetActive(true);
        }
    }
}
