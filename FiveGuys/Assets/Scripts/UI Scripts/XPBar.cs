using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class XPBar : MonoBehaviour
{

    public Slider slider;

    public void SetMinXP(float xp)
    {
        slider.minValue = xp;
        slider.value = xp;
    }

    public void SetXP(float xp)
    {
        slider.value = xp;
    }

}
