using UnityEngine;

public class SmokeSpawner : MonoBehaviour
{
    [SerializeField] private SmokeClass smokeClass;
    void Start()
    {
        
    }

    void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            smokeClass.CreateParticle(pos);
        }
    }
}
