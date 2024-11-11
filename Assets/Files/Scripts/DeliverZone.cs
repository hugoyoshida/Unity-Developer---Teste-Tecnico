using UnityEngine;

public class DeliverZone : MonoBehaviour
{
    [Header("Settings")]
    [Header(" - Material Blink")]
    [SerializeField] private float minAlpha = 0;
    [SerializeField] private float maxAlpha = 1;
    [SerializeField] private float speed = 1;
    [SerializeField] private Material material;

    [Header("Scripts")]
    [SerializeField] private DataController dataController;

    private float alphaDirection = 1;
    private float currentAlpha;

    private readonly float cashPerTarget = 5;

    private void Update()
    {
        BlinkZone();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy(other.gameObject);
            dataController.currentTargetValue -= 1;
            dataController.UpdateTarget();
            dataController.currentCash += cashPerTarget * dataController.bonus;
            dataController.UpdateCash();
        }
    }

    private void BlinkZone()
    {
        if (material == null)
            return;

        currentAlpha += alphaDirection * speed * Time.deltaTime;

        if (currentAlpha >= maxAlpha)
        {
            currentAlpha = maxAlpha;
            alphaDirection = -1.0f;
        }
        else if (currentAlpha <= minAlpha)
        {
            currentAlpha = minAlpha;
            alphaDirection = 1.0f;
        }

        Color color = material.color;
        color.a = currentAlpha;
        material.color = color;
    }
}
