using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : Button {
    private CamMovement camera;
    public bool rightButton;

    // Use this for initialization
    void Start() {
        camera = GameObject.Find("Camera").GetComponent<CamMovement>();
        if (this.gameObject.name == "BackwardButton")
            rightButton = true;
        else if (this.gameObject.name == "ForwardButton")
            rightButton = false;
    }

    void FixedUpdate() {
        if (this.IsPressed() ) {
            Debug.Log(gameObject.name + "is being pressed");
            camera.move(rightButton);
            camera.IsMoving = true;
        }
    }

    void OnRelease() {
        camera.stopMovement();
        Debug.Log("button released");
    }
  
    // Update is called once per frame
    void Update() {

    }
}
