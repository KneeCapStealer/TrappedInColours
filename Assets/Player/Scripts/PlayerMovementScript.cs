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
            playerCamera.transform.eulerAngles += new Vector3(looker, 0, 0); 
            Vector3 localEulerAngles = playerCamera.transform.eulerAngles;
            localEulerAngles.x = ModularClamp(localEulerAngles.x, -70, 100);
            playerCamera.transform.eulerAngles = localEulerAngles;
        }

        Vector2 moveVec = move.action.ReadValue<Vector2>();
        rb.AddRelativeForce(new Vector3(moveVec.x, 0, moveVec.y) * movementSpeed * Time.deltaTime);
    }

    private float ModularClamp(float val, float min, float max, float rangemin = -180f, float rangemax = 180f)
    {
        var modulus = Mathf.Abs(rangemax - rangemin);
        if ((val %= modulus) < 0f) val += modulus;
        return Mathf.Clamp(val + Mathf.Min(rangemin, rangemax), min, max);
    }

}

