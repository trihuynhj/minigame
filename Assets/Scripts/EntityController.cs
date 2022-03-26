using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Transform gameArena;
    [SerializeField] private Transform coreCenter;

    private int currentLevel;

    private int entityNumber;

    private bool entityIsEnemy;
    private string[] entityType;
    [SerializeField] private float entitySpeed;

    [SerializeField] private GameObject entityPrefab;
    private float respawnTime;

    private float cornerToCenterDistance = 28f;

    // Start is called before the first frame update
    void Start()
    {
        // Reference the current level from GameController script
        currentLevel = gameController.gameLevel;
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

        // SET ENTITY's SPRITE & SPAWN POSITION
        // Must fix entity's position to Game Arena's center point (using GameArea's original position)        
        e.transform.position = GenerateSpawnPosition();
        e.transform.localScale = GenerateEntitySize();

        // SET ENTITY's MOVEMENT
        EntityMovement entityMovement = e.GetComponent<EntityMovement>();
        entityMovement.coreCenter = coreCenter;
        entityMovement.speed = entitySpeed;
    }

    private Vector2 GenerateSpawnPosition()
    {
        // Declare default spawnPosition at arena's center
        Vector2 _spawnPosition = new Vector2(gameArena.position.x, gameArena.position.y);
        float distanceFromCenter = cornerToCenterDistance - currentLevel * Random.Range(1f, 5f);

        _spawnPosition.x = Random.Range(-distanceFromCenter, distanceFromCenter);
        _spawnPosition.y = Random.Range(-distanceFromCenter, distanceFromCenter);

        return _spawnPosition;
    }

    private Vector3 GenerateEntitySize()
    {
        Vector3 _entitySize = Vector3.one;
        float _sizeScaled = currentLevel * Random.Range(1f, 5f);

        _entitySize += Vector3.one * _sizeScaled;

        return _entitySize;
    }
}
