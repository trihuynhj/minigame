using System.Collections;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Transform gameArena;
    [SerializeField] private Transform coreTransform;

    private int currentLevel;

    private int entityNumber;
    private bool entityIsEnemy;
    private string[] entityType;
    [SerializeField] private float entitySpeed;
    [SerializeField] private GameObject entityPrefab;
    private float entityRespawnTime;

    private float cornerToCenterDistance = 28f;

    private void Start()
    {
        // Reference the current level from GameController script
        currentLevel = gameController.gameLevel;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) & coroutine == null)
        {
            coroutine = SpawnEntity();
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator coroutine = null;

    private IEnumerator SpawnEntity()
    {
        GameObject entity = Instantiate(entityPrefab, transform);

        // SET ENTITY's SPRITE & SPAWN POSITION
        // Must fix entity's position to Game Arena's center point (using GameArea's original position)        
        entity.transform.position = RandomGenerateSpawnPosition();
        entity.transform.localScale = RandomGenerateEntitySize();

        // Set Entity's Movement using Rigidbody2D
        Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>(); ;
        Vector3 directionToCore = coreTransform.position - entity.transform.position;
        entityRb.AddForce(directionToCore.normalized * entitySpeed, ForceMode2D.Impulse);

        yield return null;

        coroutine = null;
    }

    private Vector2 RandomGenerateSpawnPosition()
    {
        // Declare default spawnPosition at arena's center
        Vector2 _spawnPosition = new Vector2(gameArena.position.x, gameArena.position.y);
        float distanceFromCenter = cornerToCenterDistance - currentLevel * Random.Range(1f, 5f);

        _spawnPosition.x = Random.Range(-distanceFromCenter, distanceFromCenter);
        _spawnPosition.y = Random.Range(-distanceFromCenter, distanceFromCenter);

        return _spawnPosition;
    }

    private Vector3 RandomGenerateEntitySize()
    {
        Vector3 _entitySize = Vector3.one;
        _entitySize += Vector3.one * Random.Range(1f, 100f) / 10f;

        return _entitySize;
    }
}
