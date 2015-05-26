using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : Button {
    private new CamMovement camera;
    private bool rightButton;

    override
    protected void Start() {
        camera = GameObject.Find("Camera").GetComponent<CamMovement>();
        if (this.gameObject.name == "BackwardButton")
            rightButton = true;
        else if (this.gameObject.name == "ForwardButton")
            rightButton = false;
    }

    
    void FixedUpdate() {
        if (this.IsPressed() && this.gameObject.name == "ForwardButton" || this.IsPressed() && this.gameObject.name == "BackwardButton") {
            Debug.Log(gameObject.name + "is being pressed");
            camera.move(rightButton);
        }
        if (this.IsPressed() && this.gameObject.name == "LeftRotationButton" || this.IsPressed() && this.gameObject.name == "RightRotationButton") {
            camera.rotateLeftRight(this.gameObject.name == "LeftRotationButton");
        }
        if (this.IsPressed() && this.gameObject.name == "DownRotationButton" || this.IsPressed() && this.gameObject.name == "UpRotationButton") {
            camera.rotateUpDown(this.gameObject.name == "DownRotationButton");
        }
        if (!this.IsPressed() && this.gameObject.name == "ForwardButton" | !this.IsPressed() && this.gameObject.name == "BackwardButton") {
            camera.stopMovement();
        }
    }
  
}
