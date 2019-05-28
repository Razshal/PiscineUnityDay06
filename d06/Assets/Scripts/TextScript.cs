using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour {
    private int alpha = 255;
    private Color color = Color.white;
    private bool touched = false;

    private void OnTriggerEnter(Collider other)
    {
        touched = true;
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        if (touched)
        {
            color.a -= 0.01f;
            gameObject.GetComponent<TextMesh>().color = color;
        }
    }
}
