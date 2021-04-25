using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bubbles : Floater
{
    new private void Start()
    {
        if(Random.Range(0, 2) == 0) GetComponent<SpriteRenderer>().flipX = true;
        base.Start();
    }
}