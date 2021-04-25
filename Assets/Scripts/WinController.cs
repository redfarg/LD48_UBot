using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public UnityEvent WinEvent;
    public UnityEvent LooseEvent;

    [SerializeField] Text winText1;
    [SerializeField] Text winText2;
    [SerializeField] GameObject againButton;
    [SerializeField] Text gameOverText;

    void Start()
    {
        this.WinEvent.AddListener(HandleWinEvent);
        this.LooseEvent.AddListener(HandleLooseEvent);
        this.againButton.GetComponent<Button>().onClick.AddListener(HandleButtonEvent);
    }

    private void HandleButtonEvent()
    {
        SceneManager.LoadScene("Main");
    }

    private void HandleWinEvent()
    {
        StartCoroutine(ShowWinScreen());
    }

    private void HandleLooseEvent()
    {
        StartCoroutine(ShowLooseScreen());
    }

    IEnumerator ShowWinScreen() {
        yield return new WaitForSeconds(1);
        this.winText1.enabled = true;
        yield return new WaitForSeconds(2);
        this.winText2.enabled = true;
        yield return new WaitForSeconds(2);
        this.againButton.SetActive(true);
    }

    IEnumerator ShowLooseScreen()
    {
        yield return new WaitForSeconds(2);
        this.gameOverText.enabled = true;
        yield return new WaitForSeconds(2);
        this.againButton.SetActive(true);
    }

}
