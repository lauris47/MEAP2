using UnityEngine;


public class GyroController : MonoBehaviour {

    private const float lowPassFilter = 0.1f;

    private Quaternion baseIdentity = Quaternion.Euler(90, 0, 0);

    private Quaternion cameraBase = Quaternion.identity;
    private Quaternion calibration = Quaternion.identity;
    private Quaternion baseOrientation = Quaternion.Euler(90, 0, 0);
    private Quaternion referanceRotation = Quaternion.identity;



    void Start() {
        ResetBaseOrientation();
        UpdateBaseRotation();
        RecalculateReferenceRotation();
        Input.gyro.enabled = true;
    }

    void Update() {
        transform.rotation = Quaternion.Slerp(transform.rotation,
                                              ConvertRotation(referanceRotation * Input.gyro.attitude),
                                              lowPassFilter);
    }

    // Update the camera base rotation.
    private void UpdateBaseRotation() {
        Vector3 forward = transform.forward;
        forward.y = 0;
        if (forward == Vector3.zero)
            cameraBase = Quaternion.identity;
        else
            cameraBase = Quaternion.FromToRotation(Vector3.forward, forward);
    }

    // Recalculates reference rotation.
    private void RecalculateReferenceRotation() {
        referanceRotation = Quaternion.Inverse(baseOrientation) * calibration;
    }

    // Converts the rotation from right handed to left handed.
    private static Quaternion ConvertRotation(Quaternion q) {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }


    // Recalculates reference system.
    private void ResetBaseOrientation() {
        baseOrientation = Quaternion.identity * baseIdentity;
    }
}