using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseLook : MonoBehaviour
{

    public float yLowClampAngle = -30.0f;
    public float yClampRange = 60.0f;
    public float mouseSensitivity = 30.0f;
    public float smoothing = 3.0f;

    private GameObject player;
    private Vector2 _smoothMouse;
    private bool isCursorVisible = false;
    private Vector2 _mouseAbsolute;

    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        player = this.transform.parent.gameObject;
        //get the rigid body component for later use
        rb = player.GetComponent<Rigidbody>();
    }


    void Update()
    {

        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        mouseDelta = Vector2.Scale(mouseDelta, new Vector2(mouseSensitivity, mouseSensitivity));

        _smoothMouse.x = Mathf.Lerp(_smoothMouse.x, mouseDelta.x, 1f / smoothing);
        _smoothMouse.y = Mathf.Lerp(_smoothMouse.y, mouseDelta.y, 1f / smoothing);

        _mouseAbsolute += _smoothMouse;

        _mouseAbsolute.y = Mathf.Clamp(_mouseAbsolute.y, yLowClampAngle, yLowClampAngle + yClampRange);

        transform.localRotation = Quaternion.AngleAxis(-_mouseAbsolute.y, Vector3.right);
        rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, mouseSensitivity * Input.GetAxis("Mouse X"), 0f));
        rb.MoveRotation(Quaternion.Euler(rb.rotation.eulerAngles + new Vector3(0f, mouseSensitivity * Input.GetAxis("Mouse X"), 0f)));

    }


    private void FixedUpdate()
    {
        if (!isCursorVisible)
        {
            Cursor.visible = false;
            //Cursor.lockState = true;
            Screen.lockCursor = true;
        }
        else
        {
            Cursor.visible = true;
            //Cursor.lockState = false;
            Screen.lockCursor = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorVisible = !isCursorVisible;
        }

    }
}
