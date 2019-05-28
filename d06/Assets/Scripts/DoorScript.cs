using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
    public PlayerScript playerScript;
	public AudioSource audioSource;
    public AudioClip denied;
    public AudioClip open;

	// Use this for initialization
	void Start () {
        playerScript = GameObject.FindWithTag("Player").GetComponent<PlayerScript>();
        audioSource = gameObject.GetComponent<AudioSource>();
	}

    private void OnTriggerEnter(Collider collision)
	{
        if (collision.gameObject.CompareTag("Player") && playerScript.hasTheKey)
        {
            audioSource.clip = open;
            audioSource.Play();
            Destroy(gameObject, 1);
        }
        else
        {
            audioSource.clip = denied;
            audioSource.Play();
        }
	}
}
