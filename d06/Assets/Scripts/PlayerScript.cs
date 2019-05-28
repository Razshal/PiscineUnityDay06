using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    private Vector3 movement;
    private Vector3 direction;
    public float rotationSpeed = 1;
    public float speed = 0.1f;
    public int multiplier = 2;
    public bool hasTheKey = false;

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Key")
        {
			hasTheKey = true;
            Destroy(collision.gameObject);
        }
	}

	void FixedUpdate () {
        movement = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")) * speed * (Input.GetKey(KeyCode.LeftShift) ? multiplier : 1);
        gameObject.transform.Translate(movement);

        direction = new Vector3(0, Input.GetAxis("Mouse X"), 0f) * rotationSpeed;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + direction);
	}
}
