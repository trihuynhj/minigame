using System.Collections;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private Transform gameArena;
    [SerializeField] private Transform coreTransform;

    [SerializeField] private GameObject entityPrefab;
    [SerializeField] private float spawnDistanceFromCenter;

    [SerializeField] private float TEST_spawnRate, TEST_speedRate;
    [SerializeField] private Vector2 TEST_sizeRate;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y) && coroutine_SpawnEntity == null)
        {
            coroutine_SpawnEntity = SpawnEntity();
            StartCoroutine(coroutine_SpawnEntity);
        }
        if (Input.GetKeyDown(KeyCode.U) && coroutine_SpawnEntity != null)
        {
            StopCoroutine(coroutine_SpawnEntity);
            coroutine_SpawnEntity = null;
        }
    }

    private IEnumerator SpawnEntity()
    {
        while (coroutine_SpawnEntity != null)
        {
            GameObject entity = Instantiate(entityPrefab, transform);

            // SET ENTITY's SPRITE & SPAWN POSITION
            // Must fix entity's position to Game Arena's center point (using GameArea's original position)        
            entity.transform.position = GenerateSpawnPosition();
            entity.transform.localScale = GenerateEntitySize();

            // Set Entity's Movement using Rigidbody2D
            Rigidbody2D entityRb = entity.GetComponent<Rigidbody2D>(); ;
            Vector3 directionToCore = coreTransform.position - entity.transform.position;
            entityRb.AddForce(directionToCore.normalized * GenerateEntitySpeed(), ForceMode2D.Impulse);

            // Plug in GameController script to Entity script
            Entity entityScript = entity.GetComponent<Entity>();
            entityScript.gameController = gameController;
            entityScript.isEnemy = GenerateEntityType();
            entityScript.attackStrength = (gameController.level + 1f) / 2f;

            yield return new WaitForSeconds(GenerateSpawnRate());
        }  
    }
    private IEnumerator coroutine_SpawnEntity = null;

    private float GenerateSpawnRate()
    {
        float rate = TEST_spawnRate + Random.value * (gameController.level + 1f);
        return rate;
    }

    private Vector3 GenerateSpawnPosition()
    {
        float randomRad = Random.Range(0f, 359f) * Mathf.Deg2Rad;
        float randomX = Mathf.Cos(randomRad) * spawnDistanceFromCenter + transform.position.x;
        float randomY = Mathf.Sin(randomRad) * spawnDistanceFromCenter + transform.position.y;
        Vector3 randomPoint = new Vector3(randomX, randomY, 0f);

        return randomPoint;
    }

    private bool GenerateEntityType()
    {
        bool isEnemy;
        int num = Random.Range(0, 99);
        if (num >= 10) { isEnemy = true; }
        else { isEnemy = false; }

        return isEnemy;
    }

    private Vector3 GenerateEntitySize()
    {
        Vector3 entitySize = Vector3.one * Random.Range(TEST_sizeRate.x, TEST_sizeRate.y) * (gameController.level + 1f);
        return entitySize;
    }

    private float GenerateEntitySpeed()
    {
        float entitySpeed = TEST_speedRate * (gameController.level + 1f);
        return entitySpeed;
    }
}
