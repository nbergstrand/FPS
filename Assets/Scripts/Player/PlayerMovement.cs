using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float mouseSensitivity = 5f;

    [SerializeField]
    float _movementSpeed = 5f;

    [SerializeField]
    float _jumpHeight = 7f;

    [SerializeField]
    float _gravity;

    float verticalRotation;

    float minMaxRotation = 30.0f;
    
    float forwardVelocity, sidewaysVelocity, verticalVelocity;

    CharacterController _controller;
         
    void Start()
    {

        _controller = gameObject.GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;

        if (_controller == null)
            Debug.LogError("Character Controller Missing");

    }

    void Update()
    {
        Rotation();
        Movement();

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleCursor();
        }
    }
        

    void Rotation()
    {

        float horizontalRotation = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, horizontalRotation, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -minMaxRotation, minMaxRotation);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }
    
    void Movement()
    {

        forwardVelocity = Input.GetAxis("Vertical") * _movementSpeed;
        sidewaysVelocity = Input.GetAxis("Horizontal") * _movementSpeed;

        Vector3 direction = new Vector3(sidewaysVelocity, 0, forwardVelocity);
        direction = Vector3.ClampMagnitude(direction, _movementSpeed);

        if (_controller.isGrounded && Input.GetKey(KeyCode.Space))
        {
            verticalVelocity = _jumpHeight;
        }

        verticalVelocity -= _gravity * Time.deltaTime;
        direction.y = verticalVelocity;

        direction = transform.TransformDirection(direction);

        _controller.Move(direction * Time.deltaTime);
    }

    void ToggleCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
