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
        if (Input.GetKeyDown(KeyCode.M)) { TickMovement(); }
    }

    private void TickMovement()
    {
        float designatedYPos = traverseDistanct * gameController.currentLevel;
        StartCoroutine(Traverse(designatedYPos));
    }

    private IEnumerator Traverse(float _designatedYPos)
    {
        while (transform.position.y < _designatedYPos)
        {
            transform.position += new Vector3(0, traverseSpeed, 0);
            yield return null;
        }
    }
}
