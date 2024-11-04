using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using Unity.Burst.CompilerServices;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void OnEnable()
    {

        CurrentCameraYaw = 180f;
        CurrentCameraPitch = 0f;
    }

    public float PlayerSpeed = 1.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 PlayerMovement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        PlayerMovement.Normalize();
        PlayerMovement = PlayerMovement * Time.fixedDeltaTime * PlayerSpeed;

        gameObject.transform.position += gameObject.transform.rotation * PlayerMovement;
    }

    RaycastHit[] raycastResults = new RaycastHit[10];

    private void Update()
    {
        CurrentCameraPitch = Mathf.Clamp(CurrentCameraPitch - Input.GetAxis("Mouse Y") * VerticalSensitivity, MinimumCameraPitch, MaximumCameraPitch);
        playerCamera.gameObject.transform.localRotation = Quaternion.Euler(CurrentCameraPitch, 0.0f, 0.0f);

        CurrentCameraYaw = (CurrentCameraYaw + Input.GetAxis("Mouse X") * HorizontalSensitivity) % 360f;
        gameObject.transform.localRotation = Quaternion.Euler(0f, CurrentCameraYaw, 0.0f);

        // Check if we're looking at an interactible
        Ray ray = new Ray(RaycastSource.position, RaycastSource.forward);
        int hitCount = Physics.RaycastNonAlloc(ray, raycastResults, 5f, LayerMask.GetMask("Interactible"));
        float closestDistance = Mathf.Infinity;
        int closestIndex = -1; 
        for(int i = 0; i < hitCount; i++)
        {
            Interactible interactWith = raycastResults[i].collider.GetComponent<Interactible>();
            if (interactWith != null && raycastResults[i].distance < closestDistance && raycastResults[i].distance < interactWith.InteractDistance)
            {
                closestIndex = i;
                closestDistance = raycastResults[i].distance;
            }
        }

        LookingAtInteractible = (closestIndex != -1);

        if (Input.GetMouseButtonDown(0) && LookingAtInteractible)
        {
            raycastResults[closestIndex].collider.GetComponent<Interactible>()?.PerformInteraction();
        }

        RaycastHit hintCollision;
        LookingAtHint = Physics.Raycast(ray, out hintCollision, 1.5f, LayerMask.GetMask("Hint"));

        if (LookingAtHint)
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Hint myHint = hintCollision.collider.GetComponent<Hint>();
                if (myHint)
                    hintTextObject.text = myHint.HelpText;
            }
        }
        else
        {
            hintTextObject.text = "";
        }

    }

    public Transform RaycastSource = null;

    [ReadOnly]
    public bool LookingAtInteractible;

    private float CurrentCameraYaw = 0.0f;
    public float HorizontalSensitivity = 8.0f;

    private float CurrentCameraPitch = 0.0f;
    public float VerticalSensitivity = 5.0f;
    public Camera playerCamera = null;
    public float MinimumCameraPitch = -150.0f;
    public float MaximumCameraPitch = 150.0f;

    [Header("Hint Stuff")]
    public bool LookingAtHint;
    public TextMeshProUGUI hintTextObject = null;



}
