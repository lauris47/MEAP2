using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CamMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    public bool IsMoving { get; set; }
    private Rigidbody rigBody;
    public float rotationSpeed = 10;

    void Awake() {
        rigBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        if (!IsMoving)
            stopMovement();
        else
            Mathf.Clamp(rigBody.velocity.magnitude, 0, moveSpeed);
    }

    public void move(bool rightButton) {
        IsMoving = true;
        int goingForward = rightButton ? -1 : 1;
        Vector3 directionVector = new Vector3(transform.forward.x, 0, transform.forward.z) * moveSpeed * goingForward;
        rigBody.velocity = directionVector;
    }

    public void rotateLeftRight(bool right) {
        int rotateRight = right ? -1 : 1;
        transform.Rotate(Vector3.up, rotationSpeed * rotateRight);
    }

    public void rotateUpDown(bool down) {
        int rotateDown = down ? -1 : 1;
        transform.Rotate(Vector3.left, rotationSpeed * rotateDown);
    }

    public void stopMovement() {
        Debug.Log("stopmovement is being called");
        IsMoving = false;
        rigBody.velocity = Vector3.zero;
        rigBody.angularVelocity = Vector3.zero;
    }


}
