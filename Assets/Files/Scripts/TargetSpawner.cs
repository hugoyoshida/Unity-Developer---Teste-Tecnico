using System.Collections;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject targetPrefab;

    [Header("Display")]
    public GameObject currentChild;

    private Coroutine coroutine;
    private Transform spawnPoint;

    private void Start()
    {
        spawnPoint = this.transform;
        SpawnChild();
    }

    private void Update()
    {
        if (currentChild == null)
            if (coroutine == null)
                coroutine = StartCoroutine(RespawnChildAfterDelay(10f));
    }

    private void SpawnChild()
    {
        currentChild = Instantiate(targetPrefab, spawnPoint.position, spawnPoint.rotation, transform);
    }

    IEnumerator RespawnChildAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (currentChild == null)
            SpawnChild();

        coroutine = null;
    }
}
