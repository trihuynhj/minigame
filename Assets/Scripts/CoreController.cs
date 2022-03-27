using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    private CircleCollider2D coreCollider;

    [SerializeField] private float coreInitialScale;
    [SerializeField] private float coreMinSize, coreMaxSize;
    [SerializeField] private float coreGenerateSpeed;
    
    // Determines core grow (1) or shrink (-1)
    private int coreGenerateVector;

    private void Start()
    {
        coreCollider = GetComponent<CircleCollider2D>();

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
        Debug.Log(Time.deltaTime.ToString());
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
