using System.Collections;
using UnityEngine;

public class CoreController : MonoBehaviour
{
    private float coreInitialRadius;
    [SerializeField] private float coreMinRadius, coreMaxRadius;
    [SerializeField] private float coreGenerateSpeed;
    
    // Determines core grow (1) or shrink (-1)
    private int coreGenerateVector;

    private void Start()
    {
        transform.localScale = Vector3.one * coreInitialRadius;
        coreGenerateVector = 1;
    }

    private void Update()
    {
        CoreVector();
        CoreLinearGeneration();
    }

    private void CoreLinearGeneration()
    {
        transform.localScale += Vector3.one * coreGenerateVector * (coreGenerateSpeed * Time.deltaTime);
        Debug.Log(Time.deltaTime.ToString());
    }

    private void CoreVector()
    {
        if (transform.localScale.magnitude <= coreMinRadius)
        {
            coreGenerateVector = 1;
        }
        else if (transform.localScale.magnitude >= coreMaxRadius)
        {
            coreGenerateVector = -1;
        }
    }
}
