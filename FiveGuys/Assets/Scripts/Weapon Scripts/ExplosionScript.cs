using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    // Initialization
    public float fadeDuration = 1f;

    public string enemyTag = "Enemy";

    private Material material;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    { // Get material and color
        material = GetComponent<Renderer>().material;
        originalColor = material.color;

        // start fading out color
        StartCoroutine("FadeOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FadeOut()
    {
        float Timer = 0f;

        while (Timer < fadeDuration)
        { // set to new color with decreasing alpha as time passes
            float alpha = Mathf.Lerp(1f, 0f, Timer / fadeDuration);
            material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            Timer += Time.deltaTime;
            yield return null;
        }

        // destroy after fading fully.
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == enemyTag)
        { // damage enemy if touching
            other.GetComponent<EnemyFollow>().TakeDamage(1);
        }
    }
}
