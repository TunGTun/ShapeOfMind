using UnityEngine;

public class WaveMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of the movement
    public float range = 5f; // Range of movement (from -range to +range)

    void Update()
    {
        // Move the object along the x-axis
        transform.position += speed * Time.deltaTime *Vector3.right ;

        // Reset position to -range when it reaches range
        if (transform.position.x >= range)
        {
            transform.position = new Vector3(-range, transform.position.y, transform.position.z);
        }
    }
}