using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

[RequireComponent(typeof(Rigidbody))]
[DisallowMultipleComponent]
public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private InputActionReference move;
    [SerializeField] private float mouseSensitivity = 1f; 
    private float turner;
    private float looker;
    private float cameraAngle = 0f;

    [SerializeField] private Camera playerCamera;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;
    }

    void FixedUpdate()
    {
        turner = Input.GetAxis("Mouse X") * mouseSensitivity;
        looker = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        if (turner != 0)
        {
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        if (looker != 0)
        {
            cameraAngle += looker * mouseSensitivity;
            cameraAngle = Mathf.Clamp(cameraAngle, -70, 70);
            playerCamera.transform.localRotation = Quaternion.Euler(cameraAngle, 0, 0);
        }

        Vector2 moveVec = move.action.ReadValue<Vector2>();
        rb.AddRelativeForce(new Vector3(moveVec.x, 0, moveVec.y) * movementSpeed * Time.deltaTime);
    }

    private float ModularClamp(float val, float min, float max, float rangemin = 0f, float rangemax = 360f)
    {
        var modulus = Mathf.Abs(rangemax - rangemin);
        if ((val %= modulus) < 0f) val += modulus;
        return Mathf.Clamp(val + Mathf.Min(rangemin, rangemax), min, max);
    }

    private float NormalizeAngle(float a)
    {
        return a - 180f * Mathf.Floor((a + 180f) / 180f);
    }

}

