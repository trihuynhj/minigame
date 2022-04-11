using System.Collections;
using UnityEngine;

public class LevelTick : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    private float bottomWorldPosition = 0f;
    private float topWorldPosition = 1050f;
    private float traverseDistanct;
    [SerializeField] private float traverseSpeed;
    

    private void Start()
    {
        traverseDistanct = (topWorldPosition - bottomWorldPosition) / 16f;
    }

    private void Update()
    {
        TickMovement();
    }

    private void TickMovement()
    {
        float designatedYPos = traverseDistanct * gameController.currentLevel;

        if (coroutine_Traverse == null) 
        {
            coroutine_Traverse = Traverse(designatedYPos);
            StartCoroutine(coroutine_Traverse); 
        }
    }

    private IEnumerator Traverse(float _designatedYPos)
    {
        // This only accounts for the tick moving upward, no downward movement
        // Because there is no regression in the game, Player loses [Game-Over]
        // once vitalityPoint has run out, regardless of the level they were at.
        while (transform.position.y < _designatedYPos)
        {
            transform.position += new Vector3(0, traverseSpeed, 0);
            yield return null;
        }

        StopCoroutine(coroutine_Traverse);
        coroutine_Traverse = null;
    }
    private IEnumerator coroutine_Traverse = null;
}
