using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    private static readonly string AXIS_HORIZONTAL = "Horizontal";
    private static readonly string AXIS_VERTICAL = "Vertical";

    [SerializeField] private float xThrowSpeed = 20f;
    [SerializeField] private float yThrowSpeed = 20f;

    [SerializeField] private float[] xBorder = new float[] { -4.5f, 4.5f };
    [SerializeField] private float[] yBorder = new float[] { -3f, 3.5f };

    // Use this for initialization
    protected void Start() {
        Array.Sort(xBorder);
        Array.Sort(yBorder);
    }

    // Update is called once per frame
    protected void Update() {
        float xInput = CrossPlatformInputManager.GetAxis(AXIS_HORIZONTAL);
        float yInput = CrossPlatformInputManager.GetAxis(AXIS_VERTICAL);

        if (isAxisInput(xInput) || isAxisInput(yInput)) {

            float xThrow = xInput * xThrowSpeed * Time.deltaTime;
            float yThrow = yInput * yThrowSpeed * Time.deltaTime;

            float newX = Mathf.Clamp(transform.localPosition.x + xThrow, xBorder[0], xBorder[1]);
            float newY = Mathf.Clamp(transform.localPosition.y + yThrow, yBorder[0], yBorder[1]);

            transform.localPosition = new Vector3(newX, newY, transform.localPosition.z);
        }
    }

    private static bool isAxisInput(float inputValue) {
        if (inputValue > Mathf.Epsilon || inputValue < -Mathf.Epsilon) {
            return true;
        }

        return false;
    }
}
