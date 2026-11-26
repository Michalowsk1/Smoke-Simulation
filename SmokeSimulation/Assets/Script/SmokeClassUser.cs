using UnityEngine;

public class SmokeClassUser : SmokeClass
{
    public int kidSpawned;
    public int maxKidsAmmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMinVelocities();
        SetUpBodiesAndValues();

        kidSpawned = 0;
        maxKidsAmmount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();
        ManageTransparency();
        ManageDeletion();

        if (TickSystem.hasTickChanged && kidSpawned < maxKidsAmmount)
        {
            kidSpawned++;
            ManageSpread();
        }
    }
}
