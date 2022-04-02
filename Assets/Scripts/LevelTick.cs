using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTick : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    private int currentLevel;

    private float bottomWorldPosition = 0f;
    private float topWorldPosition = 1050f;
    private float traverseDistanct;
    [SerializeField] private float traverseSpeed;
    

    private void Start()
    {
        currentLevel = gameController.currentLevel;

        traverseDistanct = (topWorldPosition - bottomWorldPosition) / 16f;
    }

    private void Update()
    {
        //Debug.Log("TICK POSITION: " + transform.position.ToString());

        currentLevel = gameController.currentLevel;
        
        if (Input.GetKeyDown(KeyCode.M)) { TickMovement(); }
    }

    private void TickMovement()
    {
        float designatedYPos = traverseDistanct * currentLevel;
        Debug.Log("Current designatedYPos: " + designatedYPos.ToString());
        StartCoroutine(Traverse(designatedYPos));
    }

    private IEnumerator Traverse(float _designatedYPos)
    {
        while (transform.position.y < _designatedYPos)
        {
            transform.position += new Vector3(0, traverseSpeed, 0);
            Debug.Log("TICK POSITION: " + transform.position.ToString());
            yield return null;
        }
    }
}
