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
    // Settings
    [Header("Calibration Settings")]
    public VarjoEyeTracking.GazeCalibrationMode calibrationMode = VarjoEyeTracking.GazeCalibrationMode.Fast;
    public KeyCode calibrationRequestKey = KeyCode.Space;

    [Header("XR camera")]
    public Camera XRCamera;

    // Text Components
    [Header("Display (Text Components)")]
    // Display
    public Text displayPosition;
    public Text displayRotation;
    [Header("Gaze (Text Components)")]
    // Gaze
    public Text gazeStatus;
    public Text gazePosition;
    public Text gazeForward;
    [Header("Left Eye (Text Components)")]
    // Left Eye
    public Text leftEyeStatus;
    public Text leftEyePosition;
    public Text leftEyeForward;
    public Text leftEyePupilSize;
    [Header("Right Eye (Text Components)")]
    // Right Eye
    public Text rightEyeStatus;
    public Text rightEyePosition;
    public Text rightEyeForward;
    public Text rightEyePupilSize;
    [Header("Focus (Text Components)")]
    // Focus
    public Text focusDistance;
    public Text focusStability;
    [Header("Capture Tiem (Text Component)")]
    // Focus
    public Text captureTime;


    private VarjoEyeTracking.GazeData latestGazeCapture;

    private void Start()
    {
        // Request calibration every time the program starts
        VarjoEyeTracking.RequestGazeCalibration(calibrationMode);
    }

    void Update()
    {
        // If calibration key is pressed, a calibration request is sent to headset
        if (Input.GetKeyDown(calibrationRequestKey))
        {
            VarjoEyeTracking.RequestGazeCalibration(calibrationMode);
        }

        // Get data from the latest frame captured by the eye
        // tracking system
        latestGazeCapture = VarjoEyeTracking.GetGaze();
        updateCanvas(latestGazeCapture);
    }

    void updateCanvas(VarjoEyeTracking.GazeData gazeCapture)
    {
        /* Display */
        displayPosition.text = "Position: " + XRCamera.transform.localPosition.ToString("F3");
        displayRotation.text = "Rotation: " + XRCamera.transform.localRotation.ToString("F3");
        /* Gaze */
        gazeStatus.text = "Status: " + gazeCapture.status;
        gazePosition.text = "Position (Origin): " + gazeCapture.gaze.origin.ToString("F3");
        gazeForward.text = "Position (Forward): " + gazeCapture.gaze.forward.ToString("F3");
        /* Left Eye */
        leftEyeStatus.text = "Status: " + gazeCapture.leftStatus;
        leftEyePosition.text = "Position (Origin): " + gazeCapture.left.origin.ToString("F3");
        leftEyeForward.text = "Position (Forward): " + gazeCapture.left.forward.ToString("F3");
        leftEyePupilSize.text = "Pupil Size: " + gazeCapture.leftPupilSize.ToString();
        /* Right Eye */
        rightEyeStatus.text = "Status: " + gazeCapture.rightStatus;
        rightEyePosition.text = "Position (Origin): " + gazeCapture.right.origin.ToString("F3");
        rightEyeForward.text = "Position (Forward): " + gazeCapture.right.forward.ToString("F3");
        rightEyePupilSize.text = "Pupil Size: " + gazeCapture.rightPupilSize.ToString();
        /* Focus */
        focusDistance.text = "Distance: " + gazeCapture.focusDistance.ToString();
        focusStability.text = "Stability: " + gazeCapture.focusStability.ToString();
        /* Capture Time */
        captureTime.text = "Capture Time: " + (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString();
    }
}