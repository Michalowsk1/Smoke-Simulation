using Unity.VisualScripting;
using UnityEngine;

public class SmokeClass : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject Smoke;
    Rigidbody2D rigidBody2D;
    SpriteRenderer spriteRenderer;

    [Header("Movement Variables")]
    public Vector2 windDir;
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

        windDir = new Vector2(1, 1);
        randRange = 1;
        randSpawnRange = 0.01f;
        alphaDecreaser = 0.01f; //will base this off movement speed later
        spreadDecrease = 0.01f;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (TickSystem.HasTickChanged())
        {
            ManageMovement();
            ManageTransparency();
            ManageSpread();
            ManageDeletion();
        }
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

    void ManageSpread()
    {
            GameObject newSmoke = Instantiate(Smoke, new Vector2(transform.position.x + randSpawnRange, transform.position.y + randSpawnRange), Quaternion.identity);
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
