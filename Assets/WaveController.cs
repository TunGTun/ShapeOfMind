using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> waveObjects = new List<GameObject>();

    public float range = 5f;
    private float speed = 0.4f;
    void Update()
    {
        foreach (var waveObject in waveObjects)
        {
            if (waveObject.gameObject != null)
            {
                
                waveObject.gameObject.transform.position += speed * Time.deltaTime * Vector3.right;

              
                if (waveObject.gameObject.transform.position.x >= range)
                {
                    waveObject.gameObject.transform.position = new Vector3(-range, waveObject.gameObject.transform.position.y, waveObject.gameObject.transform.position.z);
                }
            }
        }
    }
}