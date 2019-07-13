using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Vector3 movement;
    private Vector3 direction;
    public float rotationSpeed = 1;
    public float speed = 0.1f;
    public int multiplier = 2;
    public bool hasTheKey = false;
    public AudioClip key;
    public AudioClip run;
    public AudioClip restart;
    public GameObject congrats;
    private AudioSource audioSource;
    private SliderScript sliderScript;
    private bool isRunningSound = false;
    private Text hints;
    private Color color = Color.white;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        sliderScript = GameObject.Find("Slider").GetComponent<SliderScript>();
        hints = GameObject.Find("Hints").GetComponent<Text>();
        Cursor.lockState = CursorLockMode.Locked;
    }

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

    public void Reload()
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
            SetHint("You got the key, now find the door");
        }
        if (collision.gameObject.CompareTag("Objective"))
        {
            Destroy(collision.gameObject);
            PlaySound(restart);
            Invoke("Reload", 4.3f);
            congrats.SetActive(true);
            SetHint("You Win !");
        }
    }

    public void SetHint(string text)
    {
        hints.text = text;
        color = Color.white;
        hints.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Fan"))
            SetHint("Press E to activate");
        if (other.gameObject.CompareTag("Detection"))
            SetHint("We can see you ...");
        if (other.gameObject.CompareTag("Door") && !hasTheKey)
            SetHint("You need a key");
        if (other.gameObject.CompareTag("Door") && hasTheKey)
            SetHint("Door opened, get the map !");
    }

    void FixedUpdate()
    {
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
        if (hints.color.a > 0)
        {
            color.a -= 0.005f;
            hints.color = color;
        }
    }
}
