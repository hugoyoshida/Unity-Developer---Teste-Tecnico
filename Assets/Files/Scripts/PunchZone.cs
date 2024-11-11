using System.Collections;
using UnityEngine;

public class PunchZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform targetTransform;

    [Header("Display")]
    public bool isTargetHere;

    private Coroutine coroutine;
    private Transform lastTargetTransform;
    private Collider targetCollider;
    private AttachTarget attachTarget;

    private void Start()
    {
        lastTargetTransform = null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isTargetHere)
            return;

        if (other.CompareTag("Target"))
        {
            isTargetHere = true;
            targetCollider = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isTargetHere)
            return;

        if (other.CompareTag("Target"))
        {
            isTargetHere = false;
            targetCollider = null;
        }
    }

    public void KnockTarget()
    {
        if (coroutine != null)
            return;

        coroutine = StartCoroutine(AttachTransform());
    }

    IEnumerator AttachTransform()
    {
        attachTarget = targetCollider.GetComponent<AttachTarget>();
        attachTarget.animator.SetTrigger("KnockedOut");
        attachTarget.GetComponent<Collider>().isTrigger = true;

        Vector3 direction = transform.position - attachTarget.transform.position;
        direction.y = 0;
        attachTarget.transform.rotation = Quaternion.LookRotation(direction);

        yield return new WaitForSeconds(3);

        //targetAttachTransform.RagDollActive(true);

        if (lastTargetTransform == null)
            attachTarget.linkTransform = targetTransform;
        else
            attachTarget.linkTransform = lastTargetTransform;

        lastTargetTransform = attachTarget.transform;
        attachTarget.GetComponentInParent<TargetSpawner>().currentChild = null;
        attachTarget = null;
        coroutine = null;
    }
}
