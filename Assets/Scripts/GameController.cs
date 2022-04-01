using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [SerializeField] private Transform player;
    [SerializeField] private ProtectShieldController protectShieldController;

    // Fields that indicate game progression
    public int currentLevel;
    public float currentPoint;

    // Points corresponding to levels, values are currently only placeholders
    private float[] pointBrackets = new float[16]
    {
        100f,
        200f,
        300f,
        400f,
        500f,
        600f,
        700f,
        800f,
        900f,
        1000f,
        1100f,
        1200f,
        1300f,
        1400f,
        1500f,
        1600f
    };

    public bool outOfProtectShield;
    private float decrementInterval;
    [SerializeField] private float outBuffer;

    // To trigger the CheckOutOfBound only AFTER the shield has been rendered
    private bool shieldRenderDone = false;

    private void Start()
    {
        currentLevel = 0;

        currentPoint = 100f;

        outOfProtectShield = false;
        decrementInterval = 1f;

        Invoke("CheckShieldRender", .6f);
    }

    private void Update()
    {
        CheckOutOfBound();

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
