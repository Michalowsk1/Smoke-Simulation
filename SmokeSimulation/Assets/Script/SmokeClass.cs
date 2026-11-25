using UnityEngine;

public class SmokeClass : MonoBehaviour
{
    [SerializeField] GameObject[] smokes;
    public Vector2 windDir;
    public float randRange;
    public float currAlpha;
    public float alphaDecreaser;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (GameObject g in smokes)
        {
            currAlpha = g.GetComponent<SpriteRenderer>().color.a - alphaDecreaser;
            Rigidbody2D rb = g.GetComponent<Rigidbody2D>();
            SpriteRenderer sp = g.GetComponent<SpriteRenderer>();
            sp.color = new Color(1, 1, 1, currAlpha);
            rb.linearVelocity = new Vector2(windDir.x + Random.Range(-randRange, randRange), windDir.y + Random.Range(-randRange, randRange));
        }
    }
}
