using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float characterSpeed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Animator animator;

    [Header("Input Actions")]
    [SerializeField] private InputAction leftStickInput;
    [SerializeField] private InputAction butonEastInput;

    [Header("Scripts")]

    //
    [SerializeField] private DataController dataController;
    //

    [SerializeField] private PunchZone punchZone;

    private Vector3 velocity;
    private Vector2 leftStickValues;
    private Vector3 characterDirectionValues;

    private readonly string RunningParameterName = "Running";
    private readonly string PunchParameterName = "Punch";

    private void OnEnable()
    {
        leftStickInput.Enable();
        leftStickInput.performed += MovementAction_performed;
        leftStickInput.canceled += LeftStickInput_canceled;
        butonEastInput.Enable();
        butonEastInput.performed += PunchAction_performed;
    }

    private void Update()
    {
        if (leftStickValues != Vector2.zero)
            characterController.Move(characterSpeed * Time.deltaTime * characterDirectionValues);

        velocity.y -= 2 * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void OnDisable()
    {
        leftStickInput.Disable();
        leftStickInput.performed -= MovementAction_performed;
        butonEastInput.Disable();
        butonEastInput.performed -= PunchAction_performed;
    }

    private void MovementAction_performed(InputAction.CallbackContext obj)
    {
        leftStickValues = leftStickInput.ReadValue<Vector2>();
        characterDirectionValues = new(leftStickValues.x, 0, leftStickValues.y);
        characterController.transform.forward = characterDirectionValues;

        animator.SetFloat(RunningParameterName, leftStickValues.magnitude);
    }

    private void LeftStickInput_canceled(InputAction.CallbackContext obj)
    {
        leftStickValues = Vector2.zero;
        characterDirectionValues = Vector3.zero;

        animator.SetFloat(RunningParameterName, 0);
    }

    private void PunchAction_performed(InputAction.CallbackContext obj)
    {
        animator.SetTrigger(PunchParameterName);

        if (!punchZone.isTargetHere)
            return;

        if (dataController.currentTargetValue >= dataController.capacity)
            return;

        dataController.currentTargetValue += 1;
        dataController.UpdateTarget();
        punchZone.KnockTarget();
    }
}
