using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToNorm : MonoBehaviour
{

    public float rotationSpeedX = 5f;
    public float rotationSpeedY = 5f;
    public float returnTime = 5f; // Time in seconds before returning to previous rotation
    private Quaternion initialRotation;
    private float timer;
    public Transform Player;

    private void Start()
    {
        initialRotation = transform.rotation;
        timer = returnTime;
    }

    private void Update()
    {
        initialRotation = Player.rotation;
        // Check for player input to rotate the camera
        HandleInput();

        // Update the timer
        timer -= Time.deltaTime;

        // If the timer reaches zero, return to the initial rotation
        if (timer <= 0f)
        {
            StartCoroutine(ReturnToInitialRotation());
        }
    }

    private void HandleInput()
    {
        // Example: Rotate the camera based on player input (you can customize this part)
        float horizontalInput = ControlFreak2.CF2Input.GetAxis("Mouse X");
        float verticalInput = ControlFreak2.CF2Input.GetAxis("Mouse Y");

        Vector3 rotation = new Vector3(verticalInput* rotationSpeedY, horizontalInput* rotationSpeedX, 0f) * Time.deltaTime;
        transform.Rotate(rotation);

        // Reset the timer when there is player input
        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            timer = returnTime;
        }
    }

    private IEnumerator ReturnToInitialRotation()
    {
        float elapsedTime = 0f;
        Quaternion currentRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * (2f / returnTime);
            transform.rotation = Quaternion.Slerp(currentRotation, initialRotation, elapsedTime);
            yield return null;
        }

        // Reset the timer and continue monitoring for player input
        timer = returnTime;
    }
}

