using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Transform coreCenter;

    private Rigidbody2D rb;
    public float speed;


    // Start is called before the first frame update
    void Start()
    {
        // Offset the Z axis so that Entity is on same layer with Core
        transform.position = new Vector3(transform.position.x, transform.position.y, 8f);

        rb = this.GetComponent<Rigidbody2D>();
        LinearMovement();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, coreCenter.position) < .1f)
        {
            Destroy(gameObject);
        }
    }

    private void LinearMovement()
    {
        Vector3 _destination = coreCenter.position - transform.position;
            rb.velocity = _destination.normalized * speed;
    }
}
