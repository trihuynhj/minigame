using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private ProgressBar progressBar;

    [SerializeField] private Transform player;
    [SerializeField] private ProtectShieldController protectShieldController;

    // Fields that indicate game progression
    public int currentLevel, currentPoint, currentMaxPoint;

    // Points corresponding to levels, values are currently only placeholders
    private int[] pointBrackets = new int[16]
    {
        200,
        400,
        600,
        900,
        1200,
        1500,
        1800,
        2000,
        2500,
        3000,
        4000,
        5500,
        7000,
        9000,
        1100,
        15000
    };

    public bool outOfProtectShield;
    private float decrementInterval;
    [SerializeField] private float outBuffer;

    // To trigger the CheckOutOfBound only AFTER the shield has been rendered
    private bool shieldRenderDone = false;

    private void Start()
    {
        currentLevel = 0;
        currentPoint = 100;
        currentMaxPoint = pointBrackets[currentLevel];

        // Set ProgressBar's initial value
        progressBar.SetMaxPoint(currentMaxPoint);
        progressBar.SetPoint(currentPoint);

        outOfProtectShield = false;
        decrementInterval = 1f;

        Invoke("CheckShieldRender", .6f);
    }

    private void Update()
    {
        CheckOutOfBound();
        progressBar.SetPoint(currentPoint);
        scoreText.text = currentPoint.ToString();
    }

    private void CheckShieldRender()
    {
        shieldRenderDone = true;
    }

    // Assign Coroutine so that it only runs once in Update()
    private IEnumerator coroutine = null;

    public IEnumerator ScoreDecrementByShieldBound()
    {
        if (outOfProtectShield)
        {
            // Update currentPoint and ProgressBar
            currentPoint--;

            yield return new WaitForSeconds(decrementInterval);

            coroutine = null;
        }
    }

    // Check if Player is out of ProtectShield, if so start the Coroutine to decrement vitalityPoint
    private void CheckOutOfBound()
    {
        if (!shieldRenderDone) { return; }

        float playerDistance = Vector3.Distance(player.position, protectShieldController.shieldCenter);
        float protectShieldBound = protectShieldController.shieldRadius + outBuffer;

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
