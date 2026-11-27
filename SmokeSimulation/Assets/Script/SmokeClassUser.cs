using UnityEngine;

public class SmokeClassUser : SmokeClass
{
    public int AmmountOfSmokeNearby;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        AmmountOfSmokeNearby = 0;
        SetMinVelocities();
    }

    // Update is called once per frame
    void Update()
    {
        AmmountOfSmokeNearby = NearbySmokes();
        ManageMovement();
        ManageTransparency(spriteRenderer);
        ManageDeletion(spriteRenderer);

        if (TickSystem.hasTickChanged && ChildrenSpawned < MaxAmmountOfChildren)
        {
            ChildrenSpawned++;
            ManageSpread(spriteRenderer, NearbySmokes());
        }
    }

    int NearbySmokes()
    {
        int maskLayer = LayerMask.GetMask("TransparentFX");
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2, maskLayer);
        return hitColliders.Length;
    }
}
