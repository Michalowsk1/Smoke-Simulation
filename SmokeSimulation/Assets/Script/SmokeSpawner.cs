using UnityEngine;

public class SmokeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Smoke;
    private Vector3 worldPos;
    void Start()
    {
        
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPos.z = 0;
            Instantiate(Smoke, worldPos, Quaternion.identity);
        }
    }
}
