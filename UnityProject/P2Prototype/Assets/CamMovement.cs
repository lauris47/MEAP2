using UnityEngine;
using System.Collections;

public class CamMovement : MonoBehaviour {
    public float moveSpeed = 5f;
    private Rigidbody rigBody;

    void Awake() {
        rigBody = GetComponent<Rigidbody>();
    }

    public void move(bool goingForward) {
        int direction = goingForward ? 1 : -1;
        rigBody.AddForce(transform.forward * moveSpeed * direction);
        // this.transform.position +=  transform.forward * direction * moveSpeed;
    }

    public void stopMovement() {
        rigBody.velocity.Set(0, 0, 0);
        rigBody.angularVelocity.Set(0,0,0);
    }

}
