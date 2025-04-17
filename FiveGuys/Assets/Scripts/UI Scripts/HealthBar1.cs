using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar1 : MonoBehaviour
{

    public Slider slider;

    public void SetMaxHealth(float lives)
    {
        slider.maxValue = lives;
        slider.value = lives;
    }

    public void SetHealth(float lives)
    {
        slider.value = lives;
    }

}
