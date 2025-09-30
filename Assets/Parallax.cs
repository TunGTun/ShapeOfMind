using UnityEngine;
using UnityEngine.UI;

public class Parallax : MonoBehaviour
{
    [SerializeField] private RawImage img; // gán RawImage ở Inspector
    [SerializeField] private float x = 0.1f; // tốc độ trục X
    [SerializeField] private float y = 0f;   // tốc độ trục Y

    void Update()
    {
        // tăng dần offset theo thời gian
        img.uvRect = new Rect(
            img.uvRect.position + new Vector2(x, y) * Time.deltaTime,
            img.uvRect.size
        );
    }
}