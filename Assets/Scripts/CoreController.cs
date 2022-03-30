using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    [SerializeField] private float coreInitialScale;
    [SerializeField] private float coreMinSize, coreMaxSize;
    [SerializeField] private float coreGenerateSpeed;
    
    // Determines core grow (1) or shrink (-1)
    private int coreGenerateVector;

    private void Start()
    {
        transform.localScale = Vector3.one * coreInitialScale;
        coreGenerateVector = 1;
    }

    private void Update()
    {
        LinearGenerateShape();
        GenerateVector();
    }

    private void LinearGenerateShape()
    {
        transform.localScale += Vector3.one * coreGenerateVector * (coreGenerateSpeed * Time.deltaTime);
    }

    private void GenerateVector()
    {
        if (transform.localScale.x >= coreMaxSize)
        {
            coreGenerateVector = -1;
        }
        else if (transform.localScale.x <= coreMinSize)
        {
            coreGenerateVector = 1;
        }
        else { return; }
    }
}
