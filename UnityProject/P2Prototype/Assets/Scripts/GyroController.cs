using UnityEngine;


public class GyroController : MonoBehaviour {

    private bool gyroEnabled = true;
    private const float lowPassFilterFactor = 0.1f;

    private Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);

    private Quaternion cameraBase = Quaternion.identity;
    private Quaternion calibration = Quaternion.identity;
    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion baseOrientationRotationFix = Quaternion.identity;

    private Quaternion referanceRotation = Quaternion.identity;



    protected void Start() {
        AttachGyro();
        Input.gyro.enabled = true;
    }

    protected void Update() {
        if (!gyroEnabled)
            return;
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              cameraBase * (ConvertRotation(referanceRotation * Input.gyro.attitude) * GetRotFix()),
                                              lowPassFilterFactor);
    }


    // Attaches gyro controller to the transform.
    private void AttachGyro() {
        gyroEnabled = true;
        ResetBaseOrientation();
        UpdateCalibration(true);
        UpdateCameraBaseRotation(true);
        RecalculateReferenceRotation();
    }


    // Detaches gyro controller from the transform
    private void DetachGyro() {
        gyroEnabled = false;
    }


    // Update the gyro calibration.
    private void UpdateCalibration(bool onlyHorizontal) {
        if (onlyHorizontal) {
            var fw = (Input.gyro.attitude) * (-Vector3.forward);
            fw.z = 0;
            if (fw == Vector3.zero)
                calibration = Quaternion.identity;
            else
                calibration = (Quaternion.FromToRotation(baseOrientationRotationFix * Vector3.up, fw));
        }
        else 
            calibration = Input.gyro.attitude;
    }

    // Update the camera base rotation.
    private void UpdateCameraBaseRotation(bool onlyHorizontal) {
        if (onlyHorizontal) {
            var fw = transform.forward;
            fw.y = 0;
            if (fw == Vector3.zero) 
                cameraBase = Quaternion.identity;
            else 
                cameraBase = Quaternion.FromToRotation(Vector3.forward, fw);
        }
        else
            cameraBase = transform.rotation;
    }


    // Converts the rotation from right handed to left handed.
    private static Quaternion ConvertRotation(Quaternion q) {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    // Gets the rot fix for different orientations.
    private Quaternion GetRotFix() {
        return Quaternion.identity;
    }

    // Recalculates reference system.
    private void ResetBaseOrientation() {
        baseOrientationRotationFix = GetRotFix();
        baseOrientation = baseOrientationRotationFix * baseIdentity;
    }

    // Recalculates reference rotation.
    private void RecalculateReferenceRotation() {
        referanceRotation = Quaternion.Inverse(baseOrientation) * Quaternion.Inverse(calibration);
    }

}
