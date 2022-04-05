using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // PLAYER
    [SerializeField] private Transform player;
    [SerializeField] private ProtectShieldController protectShieldController;

    // PROGRESS BAR
    [SerializeField] private Text progressText;
    [SerializeField] private ProgressBar progressBar;

    // LEVEL BAR
    [SerializeField] private Text levelText;

    // GAME PROGRESSION (PUBLIC FIELDS)
    public int currentLevel, currentPoint, currentMaxPoint;

    // LEVEL BRACKETS (TOTAL OF 16 LEVELS EXCLUDING LEVEL ZERO)
    private int[] levelBrackets = new int[17]
    {
        200,    // LVL 0*
        400,    // LVL 1
        600,    // LVL 2
        900,    // LVL 3
        1200,   // LVL 4
        1500,   // LVL 5
        1800,   // LVL 6
        2000,   // LVL 7
        2500,   // LVL 8
        3000,   // LVL 9
        4000,   // LVL 10
        5500,   // LVL 11
        7000,   // LVL 12
        9000,   // LVL 13
        11000,  // LVL 14
        15000,  // LVL 15
        20000   // LVL 16
    };

    public bool outOfProtectShield;
    private float decrementInterval;
    [SerializeField] private float outBuffer;

    // To trigger the UpdateCurrentPointByProtectShield only AFTER the shield has been rendered
    private bool shieldRenderDone = false;

    private void Start()
    {
        currentLevel = 0;
        currentPoint = 100;
        currentMaxPoint = levelBrackets[currentLevel];

        // Set ProgressBar's initial value
        progressBar.SetMaxPoint(currentMaxPoint);
        progressBar.SetPoint(currentPoint);

        outOfProtectShield = false;
        decrementInterval = 1f;

        Invoke("CheckShieldRender", .6f);
    }

    private void Update()
    {
        UpdateCurrentPointByProtectShield();

        // SET PROGRESS BAR
        progressBar.SetPoint(currentPoint);
        progressBar.SetMaxPoint(currentMaxPoint);

        // DISPLAY FOR TESTING
        progressText.text = currentPoint.ToString();
        levelText.text = currentLevel.ToString();

        UpdateCurrentLevel();
    }

    private void UpdateCurrentLevel()
    {
        if (currentPoint < 200) { return; }

        for (int i = 0; i < levelBrackets.Length; i++)
        {
            if (currentPoint >= levelBrackets[i] && currentPoint <= levelBrackets[i + 1])
            {
                currentLevel = i + 1;
                currentMaxPoint = levelBrackets[currentLevel];
            }
        }
    }

    private void CheckShieldRender()
    {
        shieldRenderDone = true;
    }

    // Assign Coroutine so that it only runs once in Update()
    private IEnumerator coroutine = null;

    public IEnumerator CheckShieldBound()
    {
        if (outOfProtectShield)
        {
            // Update currentPoint and ProgressBar
            currentPoint--;
            yield return new WaitForSeconds(decrementInterval);

            coroutine = null;
        }
    }

    // Check if Player is out of ProtectShield, if so start the Coroutine to decrement currentPoint
    private void UpdateCurrentPointByProtectShield()
    {
        // Make sure to not update currentPoint before ProtectShield is rendered
        if (!shieldRenderDone) { return; }

        float playerDistance = Vector3.Distance(player.position, protectShieldController.shieldCenter);
        float protectShieldBound = protectShieldController.shieldRadius + outBuffer;

        if (playerDistance > protectShieldBound & coroutine == null)
        {
            outOfProtectShield = true;

            coroutine = CheckShieldBound();
            StartCoroutine(coroutine);
        }
        else
        {
            outOfProtectShield = false;
        }
    }
}
