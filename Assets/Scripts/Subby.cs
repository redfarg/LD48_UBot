using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Subby : MonoBehaviour
{
    [SerializeField] float acceleration;
    [SerializeField] Sprite destructionSprite;
    [SerializeField] ParticleSystem destructionParticles;

    [SerializeField] BoosterController boosterController;
    [SerializeField] WinController winController;

    bool isDestroyed = false;

    void Update()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    
        if (!this.isDestroyed && (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)))
            transform.Translate(Vector3.right * acceleration * Time.deltaTime);

        if (!this.isDestroyed && (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)))
            transform.Translate(Vector3.left * acceleration * Time.deltaTime);

        if ((this.boosterController.boosterFuel != 0) && !this.isDestroyed && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) {
            this.boosterController.BoosterStartEvent.Invoke();
        }

        if (!this.isDestroyed && (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))) {
            this.boosterController.BoosterStopEvent.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.boosterController.BoosterStartEvent.Invoke();
        this.boosterController.PlayerDestroyedEvent.Invoke();
        this.isDestroyed = true;
        this.acceleration = 0;
        GetComponent<SpriteRenderer>().sprite = this.destructionSprite;
        this.destructionParticles.Play();
        this.winController.LooseEvent.Invoke();
    }

}
