using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // PLAYER
    [SerializeField] private Transform player;
    [SerializeField] private ProtectShieldController protectShieldController;

    // VITALITY BAR -> ON-GOING
    public int vitalityPoints;
    private int vitalityMaxPoints = 100;
    [SerializeField] private Text vitalityText;
    [SerializeField] private VitalityBar vitalityBar;

    // PROGRESS BAR
    [SerializeField] private Text progressText;
    [SerializeField] private ProgressBar progressBar;

    // LEVEL BAR
    [SerializeField] private Text levelText;

    // GAME PROGRESSION
    private float[] levels = new float[16]
    {
        30f,    // LVL 1
        50f,    // LVL 2
        70f,    // LVL 3
        100f,   // LVL 4
        200f,   // LVL 5
        300f,   // LVL 6
        500f,   // LVL 7
        600f,   // LVL 8
        800f,   // LVL 9
        1200f,  // LVL 10
        1500f,  // LVL 11
        2000f,  // LVL 12
        2000f,  // LVL 13
        3000f,  // LVL 14
        5000f,  // LVL 15
        5000f   // LVL 16
    };
    [SerializeField] public int level;
    [SerializeField] public float points, maxPoints;

    // PROTECTSHIELD's EFFECT
    [HideInInspector] public bool outOfProtectShield;
    [SerializeField] private float outBuffer, decrementInterval;

    // To trigger the UpdateCurrentPointByProtectShield only AFTER the shield has been rendered
    private bool shieldRenderDone = false;

    private void Start()
    {
        level = 0;
        outOfProtectShield = false;

        Invoke("CheckShieldRender", .6f);
    }

    private void Update()
    {
        // UPDATE GAME STATES
        maxPoints = levels[level];
        if (points <= 0f) { points = 0f; }
        UpdateLevel();
        UpdateVitalityPointsByProtectShield();

        // SET VITALITY BAR
        vitalityBar.SetPoint(vitalityPoints);
        vitalityBar.SetMinMaxPoints(vitalityMaxPoints);

        // SET PROGRESS BAR
        progressBar.SetPoint(points);
        progressBar.SetMinMaxPoints(maxPoints);

        // DISPLAY FOR TESTING
        vitalityText.text = vitalityPoints.ToString();
        progressText.text = points.ToString();
        levelText.text = level.ToString(); 
    }

    private void UpdateLevel()
    {
        if (points >= maxPoints)
        {
            points = 0f;
            level++;
        }
    }

    private void UpdateVitality()
    {
        if (vitalityPoints < 0f)
        {
            // IMPLEMENT GAME-OVER
        }
    }

    private void CheckShieldRender()
    {
        shieldRenderDone = true;
    }

    public IEnumerator CheckShieldBound()
    {
        if (outOfProtectShield)
        {
            // Update vitalityPoints, points and ProgressBar
            vitalityPoints--;
            points--;
            yield return new WaitForSeconds(decrementInterval);

            coroutine_CheckShieldBound = null;
        }
    }
    // Assign Coroutine so that it only runs once in Update()
    private IEnumerator coroutine_CheckShieldBound = null;

    // Check if Player is out of ProtectShield, if so start the Coroutine to decrement currentPoint
    private void UpdateVitalityPointsByProtectShield()
    {
        // Make sure to not update currentPoint before ProtectShield is rendered
        if (!shieldRenderDone) { return; }

        float playerDistance = Vector3.Distance(player.position, protectShieldController.transform.position);
        float protectShieldBound = protectShieldController.shieldRadius + outBuffer;

        if (playerDistance > protectShieldBound & coroutine_CheckShieldBound == null)
        {
            outOfProtectShield = true;

            coroutine_CheckShieldBound = CheckShieldBound();
            StartCoroutine(coroutine_CheckShieldBound);
        }
        else
        {
            outOfProtectShield = false;
        }
    }
}
