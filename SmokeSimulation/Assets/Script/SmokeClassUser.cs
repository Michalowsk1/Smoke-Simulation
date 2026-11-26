using UnityEngine;

public class SmokeClassUser : SmokeClass
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //SetUpBodiesAndValues();
        SetMinVelocities();
    }

    // Update is called once per frame
    void Update()
    {
        ManageMovement();
        ManageTransparency(spriteRenderer);
        ManageDeletion(spriteRenderer);

        if (TickSystem.hasTickChanged && ChildrenSpawned < MaxAmmountOfChildren)
        {
            ChildrenSpawned++;
            ManageSpread(spriteRenderer);
        }
    }
}
