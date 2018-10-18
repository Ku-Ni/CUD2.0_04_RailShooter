using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController : MonoBehaviour {
    private static readonly string AXIS_HORIZONTAL = "Horizontal";
    private static readonly string AXIS_VERTICAL = "Vertical";

    [Header("Movement Speed settings:")]
    [SerializeField]
    private float xThrowSpeed = 20f;
    [SerializeField] private float yThrowSpeed = 20f;

    [Header("Position settings:")]
    [SerializeField]
    private float[] xBorder = new float[] { -7.5f, 7.5f };
    [SerializeField]
    private float[] yBorder = new float[] { -4.5f, 4.5f };

    [Header("Rotation settings:")]
    [SerializeField]
    private float positionYawFactor = 5f;
    [SerializeField]
    private float positionPitchFactor = -2f;
    [SerializeField]
    private float positionRollFactor = 2f;

    [SerializeField]
    private float movementPitchFactor = -100f;
    [SerializeField]
    private float movementRollFactor = -100f;


    private bool isActive = true;
    private float xThrow, yThrow;


    // Use this for initialization
    protected void Start() {
        Array.Sort(xBorder);
        Array.Sort(yBorder);
    }

    // Update is called once per frame
    protected void Update() {
        if (isActive) {
            processMovement();
            processMovementRotation();
        }
    }

    private void processMovementRotation() {
        float yaw = transform.localPosition.x * positionYawFactor;

        float positionPitch = transform.localPosition.y * positionPitchFactor;
        float movementPitch = yThrow * movementPitchFactor;
        float pitch = positionPitch + movementPitch;


        float positionRoll = transform.localPosition.x * positionRollFactor +
            transform.localPosition.y * positionRollFactor;
        float movementRoll = xThrow * movementRollFactor;
        float roll = positionRoll + movementRoll;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void processMovement() {
        float xInput = CrossPlatformInputManager.GetAxis(AXIS_HORIZONTAL);
        float yInput = CrossPlatformInputManager.GetAxis(AXIS_VERTICAL);

        if (isAxisInput(xInput) || isAxisInput(yInput)) {

            xThrow = xInput * xThrowSpeed * Time.deltaTime;
            yThrow = yInput * yThrowSpeed * Time.deltaTime;

            float newX = Mathf.Clamp(transform.localPosition.x + xThrow, xBorder[0], xBorder[1]);
            float newY = Mathf.Clamp(transform.localPosition.y + yThrow, yBorder[0], yBorder[1]);

            transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
        }
        else {
            xThrow = 0f;
            yThrow = 0f;
        }
    }

    private static bool isAxisInput(float inputValue) {
        if (inputValue > Mathf.Epsilon || inputValue < -Mathf.Epsilon) {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Starts player death sequence<br/>
    /// <strong>NOTE:</strong> Method name is refrenced from Constants.METHOD_ON_PLAYER_DEATH
    /// </summary>
    public void OnPlayerDeath() {
        isActive = false;
    }
}
