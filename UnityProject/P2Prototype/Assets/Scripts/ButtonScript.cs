using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonScript : Button {
    private new CamMovement camera;

    override
    protected void Start() {
        camera = GameObject.Find("Camera").GetComponent<CamMovement>();
    }


    void FixedUpdate() {
        if (this.IsPressed()) {
            if (this.gameObject.name == "ForwardButton" || this.gameObject.name == "BackwardButton")
                camera.move(this.gameObject.name == "BackwardButton");
            if (this.gameObject.name == "LeftRotationButton" || this.gameObject.name == "RightRotationButton")
                camera.rotateLeftRight(this.gameObject.name == "LeftRotationButton");
            if (this.gameObject.name == "DownRotationButton" || this.gameObject.name == "UpRotationButton")
                camera.rotateUpDown(this.gameObject.name == "DownRotationButton");
        }
       
    }

}
