using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour {
    private Vector3 movement;
    private Vector3 direction;
    public float rotationSpeed = 1;
    public float speed = 0.1f;
    public int multiplier = 2;
    public bool hasTheKey = false;
    public AudioClip key;
    public AudioClip run;
    public AudioClip restart;
    private AudioSource audioSource;
    private SliderScript sliderScript;
    private bool isRunningSound = false;

    void PlaySound(AudioClip clip, bool loop = false)
    {
        audioSource.loop = loop;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    private bool IsRuning()
    {
        return Input.GetKey(KeyCode.LeftShift) 
                    && (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0);
    }

	private void Start()
	{
        audioSource = gameObject.GetComponent<AudioSource>();
        sliderScript = GameObject.Find("Slider").GetComponent<SliderScript>();
	}

    private void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Key")
        {
			hasTheKey = true;
            Destroy(collision.gameObject);
            isRunningSound = false;
            PlaySound(key);
        }
        if (collision.gameObject.tag == "Objective")
        {
            Destroy(collision.gameObject);
            PlaySound(restart);
            Invoke("Reload", 4.3f);
        }
	}

	void FixedUpdate () {
        movement = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")) * speed * (Input.GetKey(KeyCode.LeftShift) ? multiplier : 1);
        gameObject.transform.Translate(movement);

        direction = new Vector3(0, Input.GetAxis("Mouse X"), 0f) * rotationSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + direction);

        if (IsRuning())
        {
			sliderScript.IncreaseDetection(0.5f);
            if (!isRunningSound)
            {
                isRunningSound = true;
                audioSource.loop = true;
                PlaySound(run, true);
            }
        }
        if (isRunningSound && !IsRuning())
        {
            isRunningSound = false;
            audioSource.Stop();
        }

	}
}
