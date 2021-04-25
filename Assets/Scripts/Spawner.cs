using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject bubblePrefab;
    Sprite[] bubbleSprites;
    [SerializeField] float bubbleSpawnTimeMin;
    [SerializeField] float bubbleSpawnTimeMax;


    public GameObject sharkPrefab;
    [SerializeField] float sharkSpawnTimeMin;
    [SerializeField] float sharkSpawnTimeMax;

    public GameObject jellyGreenPrefab;
    [SerializeField] float jellyGreenSpawnTimeMin;
    [SerializeField] float jellyGreenSpawnTimeMax;


    public GameObject jellyPurplePrefab;
    [SerializeField] float jellyPurpleSpawnTimeMin;
    [SerializeField] float jellyPurpleSpawnTimeMax;


    public GameObject anglerPrefab;
    [SerializeField] float anglerSpawnTimeMin;
    [SerializeField] float anglerSpawnTimeMax;

    public GameObject wormPrefab;
    [SerializeField] float wormSpawnTimeMin;
    [SerializeField] float wormSpawnTimeMax;

    public GameObject capsulePrefab;

    BoosterController boosterController;
    bool spawnerActive;

    List<Coroutine> activeCreatureSpawners = new List<Coroutine>();
    List<Coroutine> activeBubbleSpawners = new List<Coroutine>();

    void Start()
    {
        this.bubbleSprites = Resources.LoadAll<Sprite>("BubbleVariants");
        this.spawnerActive = true;

        this.activeBubbleSpawners.Add(StartCoroutine(BubbleSpawning()));
        this.activeBubbleSpawners.Add(StartCoroutine(BubbleSpawning()));

        boosterController = GameObject.FindObjectOfType<BoosterController>();
        this.boosterController.BoosterStartEvent.AddListener(HandleBoosterStartEvent);
        this.boosterController.BoosterStopEvent.AddListener(HandleBoosterStopEvent);
    }

    void ResetSpawners(List<Coroutine> spawnerList)
    {
        foreach (Coroutine co in spawnerList)
        {
            StopCoroutine(co);
        }
        spawnerList = new List<Coroutine>();
    }

    public void EnterZoneOne() {
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.sharkPrefab, this.sharkSpawnTimeMin, this.sharkSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.sharkPrefab, this.sharkSpawnTimeMin, this.sharkSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.sharkPrefab, this.sharkSpawnTimeMin, this.sharkSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.sharkPrefab, this.sharkSpawnTimeMin, this.sharkSpawnTimeMax)));
    }

    public void EnterZoneTwo()
    {
        ResetSpawners(this.activeCreatureSpawners);
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.sharkPrefab, this.sharkSpawnTimeMin, this.sharkSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.sharkPrefab, this.sharkSpawnTimeMin, this.sharkSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.sharkPrefab, this.sharkSpawnTimeMin, this.sharkSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyGreenPrefab, this.jellyGreenSpawnTimeMin, this.jellyGreenSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyGreenPrefab, this.jellyGreenSpawnTimeMin, this.jellyGreenSpawnTimeMax)));
    }

    public void EnterZoneThree()
    {
        ResetSpawners(this.activeCreatureSpawners);
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyPurplePrefab, this.jellyPurpleSpawnTimeMin, this.jellyPurpleSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyPurplePrefab, this.jellyPurpleSpawnTimeMin, this.jellyPurpleSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyPurplePrefab, this.jellyPurpleSpawnTimeMin, this.jellyPurpleSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyPurplePrefab, this.jellyPurpleSpawnTimeMin, this.jellyPurpleSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyPurplePrefab, this.jellyPurpleSpawnTimeMin, this.jellyPurpleSpawnTimeMax)));
    }

    public void EnterZoneFour()
    {
        ResetSpawners(this.activeCreatureSpawners);
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.jellyPurplePrefab, this.jellyPurpleSpawnTimeMin, this.jellyPurpleSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.anglerPrefab, this.anglerSpawnTimeMin, this.anglerSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.anglerPrefab, this.anglerSpawnTimeMin, this.anglerSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.anglerPrefab, this.anglerSpawnTimeMin, this.anglerSpawnTimeMax)));
    }

    public void EnterZoneFive()
    {
        ResetSpawners(this.activeCreatureSpawners);
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.wormPrefab, this.wormSpawnTimeMin, this.wormSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.wormPrefab, this.wormSpawnTimeMin, this.wormSpawnTimeMax)));
        this.activeCreatureSpawners.Add(StartCoroutine(CreatureSpawning(this.wormPrefab, this.wormSpawnTimeMin, this.wormSpawnTimeMax)));
    }

    public void EnterZoneSix()
    {
        ResetSpawners(this.activeCreatureSpawners);
        ResetSpawners(this.activeBubbleSpawners);
        SpawnBelowOnPosition(this.capsulePrefab, 0.45f, -20);
    }

    private void HandleBoosterStartEvent()
    {
        this.spawnerActive = false;
    }

    private void HandleBoosterStopEvent()
    {
        this.spawnerActive = true;
    }

    IEnumerator BubbleSpawning() {
        while (true) {
            if (this.spawnerActive) {
                GameObject spawnedBubble = SpawnFromBelow(this.bubblePrefab);
                int idx = Random.Range(0, this.bubbleSprites.Length);
                spawnedBubble.GetComponent<SpriteRenderer>().sprite = bubbleSprites[idx];
            }
            yield return new WaitForSeconds(Random.Range(this.bubbleSpawnTimeMin, this.bubbleSpawnTimeMax)); 
            
        }
    }

    IEnumerator CreatureSpawning(GameObject prefab, float spawnTimeMin, float spawnTimeMax) {
        while (true) {
            if (this.spawnerActive) {
                SpawnFromBelow(prefab);
            }
            yield return new WaitForSeconds(Random.Range(spawnTimeMin, spawnTimeMax));
        }
    }


    private GameObject SpawnFromBelow(GameObject prefab) {
        float spawnX = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x,
                Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        Vector2 spawnPosition = new Vector2(spawnX, Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y);
        return Instantiate(prefab, spawnPosition, Quaternion.identity);
    }

    private GameObject SpawnBelowOnPosition(GameObject prefab, float widthPercent, int yValue) {
        float spawnX = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width * widthPercent, 0)).x;
        Vector2 spawnPosition = new Vector2(spawnX, Camera.main.ScreenToWorldPoint(new Vector2(0, yValue)).y);
        return Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
