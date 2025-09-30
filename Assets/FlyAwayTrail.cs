using System.Collections;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class FlyAwayTrailSquare : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 2f;           // tốc độ bay lên
    public float floatAmplitude = 0.5f; // lắc ngang
    public float floatFrequency = 2f;   // tần số lắc

    private Vector3 startPos;

    void Start()
    {
        startPos = new Vector3(transform.position.x, -6f, transform.position.z);
        transform.position = startPos;

        // Setup TrailRenderer
        TrailRenderer trail = GetComponent<TrailRenderer>();
        trail.startWidth = 0.2f;
        trail.endWidth = 0.02f;
        trail.time = 0.5f; // Shorten the trail duration (e.g., 0.5 seconds)

        // tạo material vuông
        Material mat = new Material(Shader.Find("Sprites/Default"));
        Texture2D tex = new Texture2D(2, 2);
        tex.SetPixels(new Color[] { Color.white, Color.white, Color.white, Color.white });
        tex.Apply();
        mat.mainTexture = tex;

        trail.material = mat;
        trail.startColor = Color.white;
        trail.endColor = new Color(0, 1, 1, 0); // mờ dần

        // Start the movement coroutine
        StartCoroutine(FlyUp());
    }

    private IEnumerator FlyUp()
    {
        while (true)
        {
            // Move the object upward until it reaches 6
            while (transform.position.y < 6f)
            {
                float newY = transform.position.y + speed * Time.deltaTime;
                float newX = startPos.x + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
                transform.position = new Vector3(newX, newY, transform.position.z);
                yield return null;
            }

            gameObject.SetActive(false);

            yield return new WaitForSeconds(3f);

            gameObject.SetActive(true);
            transform.position = startPos;
        }
    }
}