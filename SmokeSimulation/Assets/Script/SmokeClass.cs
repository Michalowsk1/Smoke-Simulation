using UnityEngine;
using System.Collections.Generic;

public class SmokeClass : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject Smoke;
    SpriteRenderer spriteRenderer;

    [Header("Movement Variables")]
    public Vector2 randDir;
    public float randRange;
    public float xVel;
    public float yVel;
    public float minVel;
    public float velAdjuster;

    [Header("Transparency Variables")]
    public float currAlpha;
    public float alphaDecreaser;
    public float alphaDecreaserIncrement;

    [Header("Sreading Variables")]
    public float spreadDecrease;
    public float randSpawnRange;


    public struct Particle
    {
        public Vector2 particlePos;
        public Vector2 particleVel;
        public Vector4 particleColor;
    }
    [Header("Compute Shader")]
    private Particle[] data;
    public ComputeShader computeShader;
    public ComputeBuffer computeBuffer;
    public List<GameObject> particleList = new List<GameObject> ();
    public Renderer[] renderers;
    int kernel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUpBodiesAndValues();
        SetMinVelocities();
        StartComputeShader();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateParticlePosition();
    }

    public void SetUpBodiesAndValues()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        minVel = 0.001f;
        velAdjuster = 0.035f;


        randRange = 0.005f; //which direction it moves

        randSpawnRange = 0.001f; //how far it spawns from the smoke particle it spawns from

        currAlpha = 1;
        alphaDecreaser = 0.0025f; //based off movement speed
        alphaDecreaserIncrement = 0.0075f;

        spreadDecrease = 0.0075f; //how much alpha is decreased from new smoke particle
    }

    public void CreateParticle(Vector3 pos)
    {
        if (particleList.Count >= data.Length || data == null || computeBuffer == null)
            return;

        pos.z = 0;
        GameObject smoke = Instantiate(Smoke, pos, Quaternion.identity);

        particleList.Add(smoke);
        
        Particle particle = new Particle();
        particle.particlePos = smoke.transform.position;
        particle.particleVel = new Vector2(xVel, yVel);
        particle.particleColor = new Color(1, 1, 1, 1);

        int index = particleList.Count - 1;

        data[index] = particle;

        computeBuffer.SetData(data, index, index, 1);

        computeShader.SetInt("_particleCount", particleList.Count);


    }

    public void StartComputeShader()
    {
        data = new Particle[262144];
        renderers = new SpriteRenderer[data.Length];

        computeBuffer = new ComputeBuffer(data.Length, sizeof(float) * 8);
        computeBuffer.SetData(data);
        kernel = computeShader.FindKernel("CSMain");
        computeShader.SetBuffer(kernel, "particles", computeBuffer);
        computeShader.SetInt("_particleCount", 0);

    }

    public void UpdateParticlePosition()
    {

        if (particleList.Count == 0)
            return;

        int groups = Mathf.CeilToInt(data.Length / 64f);
        computeShader.Dispatch(kernel, groups, 1, 1);
        computeBuffer.GetData(data);

        for(int i = 0; i < particleList.Count; i++)
        {
            if (particleList[i] == null) continue;

            particleList[i].transform.position = data[i].particlePos;
        }
    }

    public void ManageTransparency()
    {
        currAlpha = gameObject.GetComponent<SpriteRenderer>().color.a - alphaDecreaser;
        spriteRenderer.color = new Color(1, 1, 1, currAlpha);
        alphaDecreaser = alphaDecreaserIncrement * GetMagnitude(xVel, yVel) + Random.Range(0, 0.01f);
    }

    public void ManageMovement()
    {
        xVel = xVel + (Time.deltaTime * velAdjuster);
        yVel = yVel + (Time.deltaTime * velAdjuster);
        float randomnessX = Random.Range(-randRange, randRange);
        float randomnessY = Random.Range(-randRange, randRange);
        transform.position = new Vector2(transform.position.x + ((xVel + WindSystem.windInfluence.x) + randomnessX), transform.position.y + ((yVel + WindSystem.windInfluence.y) + randomnessY));
    }

    public void ManageSpread()
    {
        if (currAlpha > 0.15)
        {
            currAlpha = currAlpha - 0.01f;
            Vector3 position = new Vector3(transform.position.x + Random.Range(-randSpawnRange, randSpawnRange), transform.position.y + Random.Range(-randSpawnRange, randSpawnRange), 0);
            CreateParticle(position);
        }
    }

    public void ManageDeletion()
    {
        if (spriteRenderer.color.a <= 0)
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

    private void OnDestroy()
    {
        computeBuffer?.Dispose();
    }
}
