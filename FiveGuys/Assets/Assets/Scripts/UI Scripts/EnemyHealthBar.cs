using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{

    public Slider slider;
    public Transform target;
    public Vector3 offset;

    public void SetMaxHealth(float lives)
    {
        slider.maxValue = lives;
        slider.value = lives;
    }

    public void SetHealth(float lives)
    {
        slider.value = lives;
    }


    void Update()
    {
         transform.position = target.position + offset;
        transform.rotation = Camera.main.transform.rotation;
    }
}
