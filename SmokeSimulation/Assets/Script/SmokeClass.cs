using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SmokeClass : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject Smoke;
    public SpriteRenderer spriteRenderer;

    [Header("Movement Variables")]
    public Vector2 randDir;
    public float randRange;
    public float xVel;
    public float yVel;
    public float minVel;
    public float velAdjuster;
    public float windEffectEnhancer;

    [Header("Transparency Variables")]
    float alphaDecreaser;
    public float currAlpha;
    public float alphaDecreaserIncrement;

    [Header("Sreading Variables")]
    public float spreadDecrease;
    public float randSpawnRange;
    public int ChildrenSpawned;
    public int MaxAmmountOfChildren;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void SetUpBodiesAndValues()
    {
        minVel = 0.001f;
        velAdjuster = 0.005f;


        randRange = 0.005f; //which direction it moves

        randSpawnRange = 0.001f; //how far it spawns from the smoke particle it spawns from

        alphaDecreaser = 0.0025f; //based off movement speed
        alphaDecreaserIncrement = 0.0075f;

        spreadDecrease = 0.0075f; //how much alpha is decreased from new smoke particle
        ChildrenSpawned = 0;
        MaxAmmountOfChildren = 10;
    }

    public void ManageTransparency(SpriteRenderer sp)
    {
        currAlpha = sp.color.a - alphaDecreaser;
        sp.color = new Color(1, 1, 1, currAlpha);

        alphaDecreaser = (alphaDecreaserIncrement * GetMagnitude(xVel, yVel) + Random.Range(0,0.001f));
    }

    public void ManageMovement()
    {
        float randomnessX = Random.Range(0, randRange);
        float randomnessY = Random.Range(0, randRange);

        xVel = xVel + WindSystem.windInfluence.x;
        yVel = yVel + WindSystem.windInfluence.y;

        transform.position = CalculateNewPos(new Vector2(randomnessX, randomnessY));
    }

    public void ManageSpread(SpriteRenderer sp , int ammountOfSmokeNearby)
    {
        if (sp.color.a > 0.15)
        {
            GameObject newSmoke = Instantiate(Smoke, new Vector2(transform.position.x + Random.Range(-randSpawnRange, randSpawnRange), transform.position.y + Random.Range(-randSpawnRange, randSpawnRange)), Quaternion.identity);
            if(ammountOfSmokeNearby <= 5)
                currAlpha = currAlpha - spreadDecrease;
            else
                currAlpha = currAlpha - (alphaDecreaser /  ammountOfSmokeNearby);

            SpriteRenderer newSp = newSmoke.GetComponent<SpriteRenderer>();
            newSp.color = new Color(1, 1, 1, currAlpha);

        }
    }

    public void ManageDeletion(SpriteRenderer sp)
    {
        if (sp.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    float GetMagnitude(float x, float y)
    {
        return Mathf.Sqrt((x * x) + (y * y));
    }

    public void SetMinVelocities() 
    {
        if(xVel < minVel)
            xVel = minVel;

        if(yVel < minVel)
            yVel = minVel;
    }

    public Vector3 CalculateNewPos(Vector2 random)
    {

        if(WindSystem.windInfluence.x < 0)
            transform.position = new Vector3(transform.position.x + xVel - random.x, transform.position.y);
        else
            transform.position = new Vector3(transform.position.x + xVel + random.x, transform.position.y);

        if (WindSystem.windInfluence.y < 0)
            transform.position = new Vector3(transform.position.x, transform.position.y + yVel - random.y);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y + yVel + random.y);

        return transform.position;


    }
}
