using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] protected float minSpeed;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float boostActiveSpeedReduction;

    internal float speed;
    internal float currentSpeed;
    BoosterController boosterController;

    protected void Start()
    {
        this.speed = Random.Range(this.minSpeed, this.maxSpeed);
        this.currentSpeed = this.speed;
        boosterController = GameObject.FindObjectOfType<BoosterController>();
        this.boosterController.BoosterStartEvent.AddListener(HandleBoosterStartEvent);
        this.boosterController.BoosterStopEvent.AddListener(HandleBoosterStopEvent);
    }

    private void HandleBoosterStartEvent()
    {
        this.currentSpeed = this.speed * this.boostActiveSpeedReduction;
    }

    private void HandleBoosterStopEvent()
    {
        if (this.boosterController.boosterFuel == 0)
        StartCoroutine(StopSlowdownAfterDelay());
        else this.currentSpeed = this.speed;
    }
    protected void Update()
    {
        transform.Translate(Vector3.up * this.currentSpeed * Time.deltaTime);
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    IEnumerator StopSlowdownAfterDelay()
    {
        yield return new WaitForSeconds(0.3f);
        this.currentSpeed = this.speed;
    }
}
