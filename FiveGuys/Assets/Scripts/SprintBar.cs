using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SprintBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxSprint(float stamina)
    {
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    public void SetSprint(float stamina)
    {
        slider.value = stamina;
    }

}
