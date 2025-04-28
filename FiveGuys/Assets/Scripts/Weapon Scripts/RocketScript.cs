using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    // Initialization of variables
    private Vector3 mouseWorldPosition;
    private Vector3 movement;

    private Rigidbody rb;

    public GameObject explosionPrefab;

    public float rocketSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // set rigidbody component
        rb = GetComponent<Rigidbody>();

        try
        { // cast ray at mouse positiom
            Vector3 mouseScreenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(mouseScreenPosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            { // turn where ray hit into world point
                mouseWorldPosition = hit.point + Vector3.up;
            }

            // create vector3 to dictate direction and speed
            movement = Vector3.Normalize(mouseWorldPosition - transform.position);
            movement *= Time.deltaTime * rocketSpeed;

            // Eplode if not hitting anything
            Invoke("Explode", 3f);
        }
        catch (System.Exception e)
        { // catch exception
            Debug.LogException(e);
            throw;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    { // move, speeding up over time
        rb.MovePosition(transform.position + movement);
        movement *= 1 + Time.deltaTime;
    }

    private void OnTriggerEnter (Collider other)
    { // Explode when hitting something that's not player
        if (other.tag != "Player")
        {
            Debug.Log(other);
            Explode();
        }
    }

    void Explode()
    { // destory self and create explosion
        Destroy(gameObject);
        Debug.Log("Eplosde");
        Instantiate(explosionPrefab, transform.position, transform.rotation);
    }
}
