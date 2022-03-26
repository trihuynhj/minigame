using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Transform arenaCenter;

    private Rigidbody2D rb;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        LinearMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(rb.position, arenaCenter.position) < 0.1f)
        {
            Destroy(gameObject);
        }

        float distanceFromCenter = Vector3.Distance(arenaCenter.position, transform.position);
        Debug.Log(distanceFromCenter.ToString());
    }

    private void LinearMovement()
    {
        Vector3 destination = arenaCenter.position - transform.position;
        rb.velocity = destination.normalized * speed;
    }
}
