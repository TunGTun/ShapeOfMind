using System.Collections.Generic;
using UnityEngine;

public class HaloController : MonoBehaviour
{
    [Header("Prefab mặc định (dùng nếu không có pool)")]
    public GameObject haloPiecePrefab;

    [Header("Danh sách 25 prefab để random")]
    public List<GameObject> prefabPool = new List<GameObject>();

    public int pieceCount = 24;
    public float radius = 2f;
    public float tiltX = 16f;
    public float rotationSpeed = 15f;
    private GameObject[] spawnedPieces;
    private bool isDereasing = false;
    

    void Start()
    {
        // Xoay parent trước
        transform.rotation = Quaternion.Euler(tiltX, 0, 0);

        SpawnHalo();
    }

    void SpawnHalo()
    {
        if (haloPiecePrefab == null && (prefabPool == null || prefabPool.Count == 0))
        {
            Debug.LogWarning("⚠️ Chưa gán prefab cho HaloSpawner!");
            return;
        }

        spawnedPieces = new GameObject[pieceCount];

        // Nếu có prefabPool thì chọn ngẫu nhiên 6 cái
        List<GameObject> selectedPrefabs = SelectRandomPrefabs(6);

        for (int i = 0; i < pieceCount; i++)
        {
            float angle = i * Mathf.PI * 2f / pieceCount;

            // vị trí trong local space (XZ phẳng)
            Vector3 localPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);

            // hướng ra ngoài vòng tròn
            Quaternion rot = Quaternion.LookRotation(localPos.normalized, Vector3.up);

            // xoay ngang 90° sang trái (trục z)
            rot *= Quaternion.Euler(0, 0f, 90f);

            // chọn prefab: nếu có pool thì lấy theo chu kỳ đều, nếu không thì fallback về haloPiecePrefab
            GameObject prefabToUse = (selectedPrefabs.Count > 0)
                ? selectedPrefabs[i % selectedPrefabs.Count]
                : haloPiecePrefab;

            // spawn dưới parent -> dùng localPosition/localRotation
            GameObject piece = Instantiate(prefabToUse, transform);
            piece.transform.localPosition = localPos;
            piece.transform.localRotation = rot;

            spawnedPieces[i] = piece;
        }
    }

    /// <summary>
    /// Chọn random n prefab khác nhau từ prefabPool
    /// </summary>
    List<GameObject> SelectRandomPrefabs(int n)
    {
        List<GameObject> result = new List<GameObject>();

        if (prefabPool == null || prefabPool.Count == 0) return result;

        // copy list để tránh trùng
        List<GameObject> tempPool = new List<GameObject>(prefabPool);

        for (int i = 0; i < n && tempPool.Count > 0; i++)
        {
            int index = Random.Range(0, tempPool.Count);
            result.Add(tempPool[index]);
            tempPool.RemoveAt(index); // tránh chọn lại cùng prefab
        }

        return result;
    }
    void Update()
    {
        // Rotate faster
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.up);
        if (isDereasing)
        {
            DecreaseRadius();
        }
    }

    public void StartDecreaseRadius()
    {
         isDereasing=true;
    }

    void DecreaseRadius()
    {
        Debug.Log("Decreasing radius");
        float decrementSpeed = 2f; // Initial decrement speed
        decrementSpeed += Time.deltaTime; // Gradually increase the speed

        if (radius > 0)
        {
            radius -= decrementSpeed * Time.deltaTime; // Use the increasing decrement speed
            if (radius < 0)
            {
                radius = 0; // Clamp to 0
                isDereasing = false;
            }

            // Update the positions of the spawned pieces
            for (int i = 0; i < spawnedPieces.Length; i++)
            {
                if (spawnedPieces[i] != null)
                {
                    float angle = i * Mathf.PI * 2f / pieceCount;
                    Vector3 localPos = new Vector3(Mathf.Cos(angle) * radius, 0, Mathf.Sin(angle) * radius);
                    spawnedPieces[i].transform.localPosition = localPos;
                }
            }
        }
        transform.position += Time.deltaTime*2*Vector3.up; // Adjust the speed as needed
    }
}
