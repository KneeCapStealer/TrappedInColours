using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.UI.GridLayoutGroup;

public class PlayerMovementScript : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private InputActionReference move;
    [SerializeField] private float mouseSensitivity = 1f; 
    private float turner;
    private float looker;

    [SerializeField] private Camera camera;


    void Start()
    {
    }

    void Update()
    {
        turner = Input.GetAxis("Mouse X") * mouseSensitivity;
        looker = -Input.GetAxis("Mouse Y") * mouseSensitivity;
        if (turner != 0)
        {
            //Code for action on mouse moving right
            transform.eulerAngles += new Vector3(0, turner, 0);
        }
        if (looker != 0)
        {
            //Code for action on mouse moving right
            camera.transform.eulerAngles += new Vector3(looker, 0, 0);
        }

        Vector2 moveVec = move.action.ReadValue<Vector2>();
        transform.Translate(new Vector3(moveVec.x, 0, moveVec.y) * speed * Time.deltaTime);
    }
}
