using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] public Text scoreText;

    [SerializeField] private Transform playerTransform;
    [SerializeField] private ProtectShieldController protectShield;

    private float vitalityPoint;

    public bool outOfProtectShield;
    private float decrementInterval;
    [SerializeField] private float outBuffer;


    private void Start()
    {
        vitalityPoint = 100f;

        outOfProtectShield = false;
        decrementInterval = 1f;
    }

    private void Update()
    {
        CheckOutOfBound();
    }

    // Assign Coroutine so that it only runs once in Update()
    private IEnumerator coroutine = null;

    public IEnumerator ScoreDecrementByShieldBound()
    {
        if (outOfProtectShield)
        {
            vitalityPoint--;
            scoreText.text = vitalityPoint.ToString();
            yield return new WaitForSeconds(decrementInterval);

            coroutine = null;
        }
    }

    // Check if Player is out of ProtectShield, if so start the Coroutine to decrement vitalityPoint
    private void CheckOutOfBound()
    {
        float playerDistance = Vector3.Distance(playerTransform.position, protectShield.protectShieldCenter);
        float protectShieldBound = protectShield.shapeRadius + outBuffer;

        if (playerDistance > protectShieldBound & coroutine == null)
        {
            outOfProtectShield = true;

            coroutine = ScoreDecrementByShieldBound();
            StartCoroutine(coroutine);
        }
        else
        {
            outOfProtectShield = false;
        }
    }
}
