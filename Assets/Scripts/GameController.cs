using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    [SerializeField] private Transform player;
    [SerializeField] private ProtectShieldController protectShieldController;

    // Level that determines game progression, in range of 0 to 16
    public int gameLevel;

    // Score that the Player gains/loses
    private float vitalityPoint;
    private float nextLevelPoint;

    public bool outOfProtectShield;
    private float decrementInterval;
    [SerializeField] private float outBuffer;


    private void Start()
    {
        gameLevel = 0;

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
        float playerDistance = Vector3.Distance(player.position, protectShieldController.protectShieldCenter);
        float protectShieldBound = protectShieldController.shapeRadius + outBuffer;

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
