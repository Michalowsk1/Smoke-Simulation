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
            smokeClass.SpawnParticle();
        }
    }
}
