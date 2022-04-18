using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // PLAYER
    [SerializeField] private Transform player;
    [SerializeField] private ProtectShieldController protectShieldController;

    // VITALITY BAR -> ON-GOING
    public int vitalityPoint;
    private int vitalityMinPoint = 0;
    private int vitalityMaxPoint = 100;
    [SerializeField] private Text vitalityText;
    [SerializeField] private VitalityBar vitalityBar;

    // PROGRESS BAR
    [SerializeField] private Text progressText;
    [SerializeField] private ProgressBar progressBar;

    // LEVEL BAR
    [SerializeField] private Text levelText;

    // GAME PROGRESSION (PUBLIC FIELDS)
    public int currentLevel, currentPoint;
    private int currentMinPoint, currentMaxPoint;
    private int maxlevel = 16;

    // LEVEL BRACKETS (TOTAL OF 16 LEVELS EXCLUDING LEVEL ZERO)
    private int[] levelBrackets = new int[17]
    {
        30,     // LVL 0*   [30 points] -> points to pass the current level
        80,     // LVL 1    [50 points]
        150,    // LVL 2    [70 points]
        250,    // LVL 3    [100 points]
        350,    // LVL 4    [200 points]
        550,    // LVL 5    [300 points]
        850,    // LVL 6    [500 points]
        1350,   // LVL 7    [600 points]
        1950,   // LVL 8    [800 points]
        2750,   // LVL 9    [1200 points]
        3950,   // LVL 10   [1500 points]
        5450,   // LVL 11   [1500 points]
        6950,   // LVL 12   [2000 points]
        8950,   // LVL 13   [2000 points]
        10950,  // LVL 14   [3000 points]
        13950,  // LVL 15   [5000 points]
        18950   // LVL 16
    };

    // PROTECTSHIELD's EFFECT
    [HideInInspector] public bool outOfProtectShield;
    [SerializeField] private float outBuffer, decrementInterval;

    // To trigger the UpdateCurrentPointByProtectShield only AFTER the shield has been rendered
    private bool shieldRenderDone = false;

    private void Start()
    {
        // Set ProgressBar's initial value
        progressBar.SetMinMaxPoints(currentMinPoint, currentMaxPoint);
        progressBar.SetPoint(currentPoint);

        outOfProtectShield = false;

        Invoke("CheckShieldRender", .6f);
    }

    private void Update()
    {
        // Limit Level to 16
        if (currentLevel > maxlevel) { currentLevel = maxlevel; }

        // UPDATE GAME STATES
        UpdateCurrentLevel();
        UpdateCurrentPointByProtectShield();

        // SET VITALITY BAR

        // SET PROGRESS BAR
        progressBar.SetPoint(currentPoint);
        progressBar.SetMinMaxPoints(currentMinPoint, currentMaxPoint);

        // DISPLAY FOR TESTING
        progressText.text = currentPoint.ToString();
        levelText.text = currentLevel.ToString(); 
    }

    private void UpdateCurrentLevel()
    {
        // Cover edge case (Start of game, currentLevel = 0)
        if (currentPoint < 30) 
        {
            currentLevel = 0;
            currentMaxPoint = levelBrackets[currentLevel];
            return;
        }

        for (int i = 0; i < levelBrackets.Length; i++)
        {
            if (currentPoint >= levelBrackets[i] && currentPoint <= levelBrackets[i + 1])
            {
                currentLevel = i + 1;
                currentMinPoint = levelBrackets[currentLevel - 1];
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

        float playerDistance = Vector3.Distance(player.position, protectShieldController.transform.position);
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
