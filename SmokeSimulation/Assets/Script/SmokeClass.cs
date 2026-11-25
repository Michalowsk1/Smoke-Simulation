using Unity.VisualScripting;
using UnityEngine;

public class SmokeClass : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject Smoke;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    [Header("Movement Variables")]
    public Vector2 randDir;
    public float randRange;

    [Header("Transparency Variables")]
    public float currAlpha;
    public float alphaDecreaser;

    [Header("Sreading Variables")]
    public float spreadDecrease;
    public float randSpawnRange;

    public int currentAmmountOfParticles;
    public int maxAmmountOfParticles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUpBodies();

        randRange = 0.01f; //which direction it moves

        randSpawnRange = 0.001f; //how far it spawns from the smoke particle it spawns from

        alphaDecreaser = 0.005f; //will base this off movement speed later

        spreadDecrease = 0.01f; //how much alpha is decreased from new smoke particle
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageMovement();
        ManageTransparency();
        ManageDeletion();

        if (TickSystem.HasTickChanged())
            ManageSpread();
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
        rigidBody2D.linearVelocity = new Vector2(rigidBody2D.linearVelocityX + (WindSystem.windInfluence.x), rigidBody2D.linearVelocityY + (WindSystem.windInfluence.y));
    }

    void ManageSpread()
    {
        GameObject newSmoke = Instantiate(Smoke, new Vector2(transform.position.x + randSpawnRange, transform.position.y + randSpawnRange), Quaternion.identity);

        randDir = new Vector2(Random.Range(-randRange, randRange), Random.Range(-randRange, randRange));
        Rigidbody2D newrb = newSmoke.GetComponent<Rigidbody2D>();
        newrb.linearVelocity = new Vector2((rigidBody2D.linearVelocityX + randDir.x), rigidBody2D.linearVelocityY + randDir.y);

        SpriteRenderer newSp = newSmoke.GetComponent<SpriteRenderer>();
        newSp.color = new Color(1, 1, 1, currAlpha - spreadDecrease);
    }

    void ManageDeletion()
    {
        if (spriteRenderer.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
