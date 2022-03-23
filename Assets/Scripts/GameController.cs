using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] public Text scoreText;

    public bool outOfProtectShield;

    private float score;
    private float timeToDecrementScore;

    private void Start()
    {
        outOfProtectShield = false;

        score = 100f;
        timeToDecrementScore = 1f;
    }

    private void Update()
    {
        
    }

    public IEnumerator ScoreCheckOutOfShield()
    {
        while (outOfProtectShield)
        {
            score--;
            scoreText.text = score.ToString();
            yield return new WaitForSeconds(timeToDecrementScore);
        }
    }
}
