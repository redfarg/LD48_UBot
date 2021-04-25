using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        this.slider = GetComponent<Slider>();

    }

    public void SetStartingValues(int maxValue) {
        this.slider.maxValue = maxValue;
        this.slider.value = this.slider.maxValue;
    }

    public void Increment(int incrementValue) {
        this.slider.value += incrementValue;
    }

    public void Decrement(int decrementValue) {
        this.slider.value -= decrementValue;
    }
}
