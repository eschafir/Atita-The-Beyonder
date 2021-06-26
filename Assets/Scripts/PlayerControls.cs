using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General setup settings")]
    [Tooltip("Movement velocity of the Ship")]
    [SerializeField] float controlSpeed = 5f;

    [Tooltip("Sets the value to clamp the Horizontal movement of the Ship. Prevents the Ship to go out of the screen.")]
    [SerializeField] float xRange = 5f;

    [Tooltip("Sets the value to clamp the Vertical movement of the Ship. Prevents the Ship to go out of the screen.")]
    [SerializeField] float yRange = 5f;


    [Header("Laser gun array")]
    [SerializeField] GameObject[] lasers;

    [Space]

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 2f;

    [Header("Player position based tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -20f;
    [SerializeField] float rotationFactor = 1f;


    float inputX, inputY;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();

    }

    void ProcessTranslation()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        float rawXmov = transform.localPosition.x + inputX * controlSpeed * Time.deltaTime;
        float rawYmov = transform.localPosition.y + inputY * controlSpeed * Time.deltaTime;

        float clampedXmov = Mathf.Clamp(rawXmov, -xRange, xRange);
        float clampedYmov = Mathf.Clamp(rawYmov, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXmov, clampedYmov, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = inputY * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = inputX * controlRollFactor;

        Quaternion targetRotation = Quaternion.Euler(pitch, yaw, roll);
        //transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationFactor);
    }

    void ProcessFiring()
    {
        if (Input.GetButton("Fire1")) { SetLasersActive(true); }
        else { SetLasersActive(false); }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
