using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private float sizeDiff, generateSpeed;
    private float coreMin, coreMax;
    
    // Determines core grow (1) or shrink (-1)
    private int generateVector;

    private void Start()
    {
        generateVector = 1;
    }

    private void Update()
    {
        GenerateVector();
        GenerateSize();

        LinearGenerateShape();
    }

    private void LinearGenerateShape()
    {
        float speed = (generateSpeed + (gameController.level + 1f) / 10f) * Time.deltaTime;
        transform.localScale += Vector3.one * generateVector * speed;
    }

    private void GenerateSize()
    {
        float index = gameController.level;
        if (index < 7) { sizeDiff = .25f; }
        else if (index < 13) { sizeDiff = .35f; }
        else { sizeDiff = .45f; }

        coreMin = (index + 2f) / 2f;
        coreMax = coreMin * (1f + sizeDiff);
    }

    private void GenerateVector()
    {
        if (transform.localScale.x >= coreMax)
        {
            generateVector = -1;
        }
        else if (transform.localScale.x <= coreMin)
        {
            generateVector = 1;
        }
        else { return; }
    }
}
