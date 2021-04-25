using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swimmer : Floater
{
    private float speedX;
    Vector3 sideDirection;

    new void Start()
    {
        this.speedX = Random.Range(0, this.maxSpeed / 3);

        Vector3 screenPos = Camera.main.WorldToScreenPoint(GetComponent<Transform>().position);
        if (screenPos.x < Screen.width / 2) { 
            this.sideDirection = Vector3.right;
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            this.sideDirection = Vector3.left;
        }

        base.Start();
    }

    new void Update()
    {
        transform.Translate(this.sideDirection * this.speedX * Time.deltaTime);
        base.Update();
    }

}
