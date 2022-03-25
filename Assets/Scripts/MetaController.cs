using UnityEngine;

public class MetaController : MonoBehaviour
{
    // GLOBAL FIELDS
    public const int WIDTH = 1920;
    public const int HEIGHT = 1080;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector3 screenToWorldPoint = Camera.main.ScreenToWorldPoint(new Vector3(WIDTH, HEIGHT, 1f));
        //Debug.Log(screenToWorldPoint.ToString());

        Vector3 mouseScreenPointPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log(mouseScreenPointPosition.ToString());
    }
}
