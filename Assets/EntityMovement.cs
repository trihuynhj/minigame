using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private Transform arenaCenter;

    private Rigidbody2D rb;
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        speed = .5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LinearMovement();
        }

        if (Vector3.Distance(rb.position, arenaCenter.position) < 0.1f)
        {
            Object.Destroy(gameObject);
        }
    }

    private void LinearMovement()
    {
        Vector3 destination = arenaCenter.position - transform.position;
        rb.velocity = destination * speed;
    }
}
