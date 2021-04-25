using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BoosterController : MonoBehaviour
{
    public UnityEvent BoosterStartEvent;
    public UnityEvent BoosterStopEvent;
    public UnityEvent PlayerDestroyedEvent;

    [SerializeField] int maxFuel;
    [SerializeField] FuelBar fuelBar;

    [SerializeField] ParticleSystem boosterParticles;

    public int boosterFuel;
    bool boostActive = false;
    bool playerAlive = true;

    private void Awake()
    {
        this.BoosterStartEvent.AddListener(HandleBoosterStartEvent);
        this.BoosterStopEvent.AddListener(HandleBoosterStopEvent);
        this.PlayerDestroyedEvent.AddListener(HandlePlayerDestroyedEvent);
        this.boosterFuel = this.maxFuel;
        this.fuelBar.SetStartingValues(this.maxFuel);
        StartCoroutine(FuelRecharging());
    }

    private void HandleBoosterStartEvent()
    {
        this.boostActive = true;
        this.boosterParticles.Play();
        StartCoroutine(FuelDepleting());
    }
    private void HandleBoosterStopEvent()
    {
        this.boostActive = false;
        this.boosterParticles.Stop();
    }

    private void HandlePlayerDestroyedEvent()
    {
        this.playerAlive = false;
        this.boosterParticles.Stop();
    }

    void Update()
    {
        if (this.boosterFuel == 0 && this.playerAlive) { 
            BoosterStopEvent.Invoke(); }
    }

    IEnumerator FuelRecharging()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            if (!this.boostActive && this.boosterFuel < this.maxFuel)
            {
                this.boosterFuel += 1;
                this.fuelBar.Increment(1);
            }
        }
    }
    IEnumerator FuelDepleting()
    {
        while (this.boostActive)
        {
            if (this.boosterFuel >= 0)
            {
                this.boosterFuel -= 1;
                this.fuelBar.Decrement(1);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
