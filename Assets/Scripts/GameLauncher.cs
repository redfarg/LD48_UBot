using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLauncher : MonoBehaviour
{

    [SerializeField] GameObject startButton;


    void Start()
    {
        this.startButton.GetComponent<Button>().onClick.AddListener(HandleButtonEvent);
    }

    private void HandleButtonEvent()
    {
        SceneManager.LoadScene("Main");
    }
}
