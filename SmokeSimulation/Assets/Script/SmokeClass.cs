using UnityEngine;

public class SmokeClass : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;
    public Vector2 windDir;
    public float randRange;
    public float currAlpha;
    public float alphaDecreaser;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUpBodies();

        windDir = new Vector2(1, 1);
        randRange = 1;
        alphaDecreaser = 0.01f; //will base this off movement speed later
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        ManageMovement();

        ManageTransparency();

    }

    void SetUpBodies()
    {
        rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void ManageTransparency()
    {
        currAlpha = gameObject.GetComponent<SpriteRenderer>().color.a - alphaDecreaser;
        spriteRenderer.color = new Color(1, 1, 1, currAlpha);
    }

    void ManageMovement()
    {
        rigidBody2D.linearVelocity = new Vector2(windDir.x + Random.Range(-randRange, randRange), windDir.y + Random.Range(-randRange, randRange));
    }
}
