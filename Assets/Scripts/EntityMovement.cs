using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public Transform coreCenter;

    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        LinearMovement();
    }

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
