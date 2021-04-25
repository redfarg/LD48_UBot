using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthController : MonoBehaviour
{
    [SerializeField] Spawner spawner;
    [SerializeField] Text depthText;
    [SerializeField] GameObject backgroundWater;
    [SerializeField] float colorChangeGradient;

    [SerializeField] int depthLevelTwo;
    [SerializeField] int depthLevelThree;
    [SerializeField] int depthLevelFour;
    [SerializeField] int depthLevelFive;
    [SerializeField] int depthLevelSix;

    Dictionary<int, System.Action> depthSetups = new Dictionary<int, System.Action>();

    int depth = 0;
    int nextLevelDepth;
    int currentLevel = 1;

    [SerializeField] BoosterController boosterController;
    [SerializeField] WinController winController;

    bool boostActive = false;

    void Start()
    {
        this.nextLevelDepth = this.depthLevelTwo;
        this.spawner.EnterZoneOne();
        this.boosterController.BoosterStartEvent.AddListener(HandleBoosterStartEvent);
        this.boosterController.BoosterStopEvent.AddListener(HandleBoosterStopEvent);
        this.depthSetups.Add(2, SetupLevelTwo);
        this.depthSetups.Add(3, SetupLevelThree);
        this.depthSetups.Add(4, SetupLevelFour);
        this.depthSetups.Add(5, SetupLevelFive);
        this.depthSetups.Add(6, SetupLevelSix);

        this.winController.WinEvent.AddListener(HandleWinEvent);

        StartCoroutine(nameof(CountingUpDepth));
        StartCoroutine(nameof(CheckingForNextLevel));
    }

    private void HandleWinEvent()
    {
        StopCoroutine(nameof(CountingUpDepth));
        StopCoroutine(nameof(CheckingForNextLevel));
    }

    private void SetupLevelTwo()
    {
        this.nextLevelDepth = this.depthLevelThree;
        spawner.EnterZoneTwo();
        
    }
    private void SetupLevelThree()
    {
        this.nextLevelDepth = this.depthLevelFour;
        spawner.EnterZoneThree();
    }

    private void SetupLevelFour()
    {
        this.nextLevelDepth = this.depthLevelFive;
        spawner.EnterZoneFour();
    }

    private void SetupLevelFive()
    {
        this.nextLevelDepth = this.depthLevelSix;
        spawner.EnterZoneFive();
    }

    private void SetupLevelSix()
    {
        this.nextLevelDepth = 999999;
        spawner.EnterZoneSix();
    }

    private void HandleBoosterStartEvent()
    {
        this.boostActive = true;
    }
    private void HandleBoosterStopEvent()
    {
        this.boostActive = false;
    }

    IEnumerator CheckingForNextLevel()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);
            if (this.depth >= nextLevelDepth)
            {
                this.currentLevel += 1;
                this.depthSetups[this.currentLevel]();
            }
        }
    }
    IEnumerator CountingUpDepth()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            if (!this.boostActive) { 
                this.depth += 1;
                this.depthText.text = this.depth.ToString();
                if(this.depth % 20 == 0)
                {
                    float r = this.backgroundWater.GetComponent<SpriteRenderer>().color.r - this.colorChangeGradient;
                    float g = this.backgroundWater.GetComponent<SpriteRenderer>().color.g - this.colorChangeGradient;
                    float b = this.backgroundWater.GetComponent<SpriteRenderer>().color.b - this.colorChangeGradient;

                    this.backgroundWater.GetComponent<SpriteRenderer>().color = new Color(r, g, b);
                }
             }
        }
    }
}
