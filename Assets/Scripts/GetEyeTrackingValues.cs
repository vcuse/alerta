using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;
using Varjo.XR;

public class GetEyeTrackingValues : MonoBehaviour
{
    /* Eye-tracking values */
    /* Display */
    public Vector3 displayPosition;
    public Quaternion displayRotation;
    /* Gaze */
    public VarjoEyeTracking.GazeStatus gazeStatus;
    public Vector3 gazePosition;
    public Vector3 gazeForward;
    /* Left Eye */
    public VarjoEyeTracking.GazeEyeStatus leftEyeStatus;
    public Vector3 leftEyePosition;
    public Vector3 leftEyeForward;
    public float leftEyePupilSize;
    /* Right Eye */
    public VarjoEyeTracking.GazeEyeStatus rightEyeStatus;
    public Vector3 rightEyePosition;
    public Vector3 rightEyeForward;
    public float rightEyePupilSize;
    /* Focus */
    public float focusDistance;
    public float focusStability;
    /* Focus */
    public long captureTime;

    /* Settings */
    [Header("Calibration Settings")]
    public VarjoEyeTracking.GazeCalibrationMode calibrationMode = VarjoEyeTracking.GazeCalibrationMode.Fast;
    public KeyCode calibrationRequestKey = KeyCode.Space;

    [Header("XR camera")]
    public Camera XRCamera;

    /* Canvas (Text Components) */
    /* Display */
    [Header("Display (Text Components)")]
    public Text canvasDisplayPosition;
    public Text canvasDisplayRotation;
    /* Gaze */
    [Header("Gaze (Text Components)")]
    public Text canvasGazeStatus;
    public Text canvasGazePosition;
    public Text canvasGazeForward;
    /* Left Eye */
    [Header("Left Eye (Text Components)")]
    public Text canvasLeftEyeStatus;
    public Text canvasLeftEyePosition;
    public Text canvasLeftEyeForward;
    public Text canvasLeftEyePupilSize;
    /* Right Eye */
    [Header("Right Eye (Text Components)")]
    public Text canvasRightEyeStatus;
    public Text canvasRightEyePosition;
    public Text canvasRightEyeForward;
    public Text canvasRightEyePupilSize;
    /* Focus */
    [Header("Focus (Text Components)")]
    public Text canvasFocusDistance;
    public Text canvasFocusStability;
    /* Focus */
    [Header("Capture Time (Text Component)")]
    public Text canvasCaptureTime;

    private VarjoEyeTracking.GazeData latestGazeCapture;

    private void Start()
    {
        /* Request calibration every time the program starts */
        VarjoEyeTracking.RequestGazeCalibration(calibrationMode);
    }

    void Update()
    {
        /* If calibration key is pressed, a calibration request is sent to headset */
        if (Input.GetKeyDown(calibrationRequestKey))
        {
            VarjoEyeTracking.RequestGazeCalibration(calibrationMode);
        }

        /* Get data from the latest frame captured by the eye
         * tracking system */
        latestGazeCapture = VarjoEyeTracking.GetGaze();
        updateValues(latestGazeCapture);
    }

    void updateValues(VarjoEyeTracking.GazeData gazeCapture)
    {
        /* Display */
        displayPosition = XRCamera.transform.localPosition;
        displayRotation = XRCamera.transform.localRotation;
        /* Gaze */
        gazeStatus = gazeCapture.status;
        gazePosition = gazeCapture.gaze.origin;
        gazeForward = gazeCapture.gaze.forward;
        /* Left Eye */
        leftEyeStatus = gazeCapture.leftStatus;
        leftEyePosition = gazeCapture.left.origin;
        leftEyeForward = gazeCapture.left.forward;
        leftEyePupilSize = gazeCapture.leftPupilSize;
        /* Right Eye */
        rightEyeStatus = gazeCapture.rightStatus;
        rightEyePosition = gazeCapture.right.origin;
        rightEyeForward = gazeCapture.right.forward;
        rightEyePupilSize = gazeCapture.rightPupilSize;
        /* Focus */
        focusDistance = gazeCapture.focusDistance;
        focusStability = gazeCapture.focusStability;
        /* Capture Time */
        captureTime = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);

        updateCanvas();
    }

    void updateCanvas()
    {
        /* Display */
        canvasDisplayPosition.text = "Position: " + displayPosition.ToString("F3");
        canvasDisplayRotation.text = "Rotation: " + displayRotation.ToString("F3");
        /* Gaze */
        canvasGazeStatus.text = "Status: " + gazeStatus;
        canvasGazePosition.text = "Position (Origin): " + gazePosition.ToString("F3");
        canvasGazeForward.text = "Position (Forward): " + gazeForward.ToString("F3");
        /* Left Eye */
        canvasLeftEyeStatus.text = "Status: " + leftEyeStatus;
        canvasLeftEyePosition.text = "Position (Origin): " + leftEyePosition.ToString("F3");
        canvasLeftEyeForward.text = "Position (Forward): " + leftEyeForward.ToString("F3");
        canvasLeftEyePupilSize.text = "Pupil Size: " + leftEyePupilSize.ToString();
        /* Right Eye */
        canvasRightEyeStatus.text = "Status: " + rightEyeStatus;
        canvasRightEyePosition.text = "Position (Origin): " + rightEyePosition.ToString("F3");
        canvasRightEyeForward.text = "Position (Forward): " + rightEyeForward.ToString("F3");
        canvasRightEyePupilSize.text = "Pupil Size: " + rightEyePupilSize.ToString();
        /* Focus */
        canvasFocusDistance.text = "Distance: " + focusDistance.ToString();
        canvasFocusStability.text = "Stability: " + focusStability.ToString();
        /* Capture Time */
        canvasCaptureTime.text = "Capture Time: " + captureTime.ToString();
    }
}