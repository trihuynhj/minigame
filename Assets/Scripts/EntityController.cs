using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private Transform gameArena;

    private int entityNumber;

    private bool entityIsEnemy;
    private string[] entityType;
    private float entitySize;
    private float entitySpeed;
    private float entitySpawnPosition;

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
        // Must fix entity's position to Game Arena's center point (using GameArea's original position)
        e.transform.position = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0f) + gameArena.position;

        EntityMovement entityMovement = e.GetComponent<EntityMovement>();
        entityMovement.arenaCenter = transform;
    }
}
