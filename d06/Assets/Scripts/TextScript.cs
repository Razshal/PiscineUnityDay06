using UnityEngine;

public class TextScript : MonoBehaviour
{
    private Color color = Color.white;
    private bool touched = false;
    private TextMesh textMesh;

    private void Start()
    {
        textMesh = gameObject.GetComponent<TextMesh>();
    }

    private void OnTriggerEnter(Collider other)
    {
        touched = true;
        Destroy(gameObject, 7f);
    }

    private void FixedUpdate()
    {
        if (touched)
        {
            color.a -= 0.005f;
            textMesh.color = color;
        }
    }
}
