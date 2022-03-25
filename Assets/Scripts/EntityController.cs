using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    private int entityNumber;

    public bool entityIsEnemy;
    public string[] entityType;
    public float entitySize;
    public float entitySpeed;
    public float entitySpawnPosition;

    [SerializeField] private GameObject entityPrefab;
    private float respawnTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnEntity();
        }
    }

    private void SpawnEntity()
    {
        GameObject e = Instantiate(entityPrefab, transform);
        e.transform.position = new Vector2(Random.Range(-20f, 20f), Random.Range(-20f, 20f));

        EntityMovement entityMovement = e.GetComponent<EntityMovement>();
        entityMovement.arenaCenter = transform;
    }
}
