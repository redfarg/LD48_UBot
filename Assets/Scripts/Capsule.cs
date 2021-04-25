using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : Floater
{

    WinController winController;
    bool inPosition = false;

    new void Start()
    {
        winController = GameObject.FindObjectOfType<WinController>();
        base.Start();
    }

    new void Update()
    {
        if (!inPosition && transform.position.y >= Camera.main.ScreenToWorldPoint(new Vector2(0, 90)).y) {
            this.inPosition = true;
            this.speed = 0;
            this.currentSpeed = 0;
            winController.WinEvent.Invoke();
        }
        base.Update();
    }



}
