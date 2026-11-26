using UnityEngine;

public class SmokeClassUser : SmokeClass
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMinVelocities();
        SetUpBodiesAndValues();
    }

    // Update is called once per frame
    void Update()
    {
        //ManageMovement();
        ManageTransparency();
        //ManageDeletion();

        //if (TickSystem.HasTickChanged())
            //ManageSpread();
    }
}
