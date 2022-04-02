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
    [SerializeField] private float spawnDistanceFromCenter, entitySpawnInterval;

    private void Start()
    {
        // Reference the current level from GameController script
        currentLevel = gameController.currentLevel;
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
        while (coroutine != null)
        {
            GameObject entity = Instantiate(entityPrefab, transform);

            // SET ENTITY's SPRITE & SPAWN POSITION
            // Must fix entity's position to Game Arena's center point (using GameArea's original position)        
            entity.transform.position = GenerateSpawnPosition();
            entity.transform.localScale = GenerateEntitySize();

            // Set Entity's Movement using Rigidbody2D
            Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>(); ;
            Vector3 directionToCore = coreTransform.position - entity.transform.position;
            entityRb.AddForce(directionToCore.normalized * entitySpeed, ForceMode2D.Impulse);

            // Plug in GameController script to Entity script
            Entity entityScript = entity.GetComponent<Entity>();
            entityScript.gameController = gameController;

            yield return new WaitForSeconds(entitySpawnInterval);
        }  
    }

    private Vector2 GenerateSpawnPosition()
    {
        Vector3 onUnitSphere = Random.onUnitSphere;
        Vector2 randomPoint = new Vector2(onUnitSphere.x, onUnitSphere.y) * spawnDistanceFromCenter;
        randomPoint += new Vector2(gameArena.position.x, gameArena.position.y);

        return randomPoint;
    }

    private Vector3 GenerateEntitySize()
    {
        Vector3 _entitySize = Vector3.one * Random.Range(1f, 5f);

        return _entitySize;
    }
}
