using System.Collections.Generic;
using System.Net;
using UnityEngine;

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

    [Header("GPUInstancing")]
    public Mesh mesh;
    public Material material;

    public struct InstanceData
    {
        public Vector2 position;
        public Color color;
    }

    List<InstanceData> instances = new List<InstanceData>();
    List<Matrix4x4> matrices = new List<Matrix4x4>();
    RenderParams rp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetUpGPUInstancing();
        SetUpBodiesAndValues();
        SetMinVelocities();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ManageMovement();
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
    }

    public void SetUpGPUInstancing()
    {
        GameObject primite = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        mesh = primite.GetComponent<MeshFilter>().sharedMesh;
        Destroy( primite );

        material.enableInstancing = true;

        rp = new RenderParams(material);
        rp.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        rp.receiveShadows = false;
    }

    public void SpawnParticle()
    {
        Vector3 worldPos;

        worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = 0;
        instances.Add(new InstanceData { position = worldPos, color = new Color(1,1,1,1)});
    }



    public float ManageTransparency(float alpha)
    {
        alphaDecreaser = alphaDecreaserIncrement * GetMagnitude(xVel, yVel) + Random.Range(0,0.01f);

        alpha = alpha - alphaDecreaser;
        return alpha;
    }

    public void ManageMovement()
    {
        xVel = xVel + (Time.deltaTime * velAdjuster);
        yVel = yVel + (Time.deltaTime * velAdjuster);
        float randomnessX = Random.Range(-randRange, randRange);
        float randomnessY = Random.Range(-randRange, randRange);

        matrices.Clear();
        int currentParticleCount = instances.Count;
        for(int i = 0; i < currentParticleCount; i++)
        {
            InstanceData data = instances[i];
            data.position += new Vector2(xVel + WindSystem.windInfluence.x + randomnessX, yVel + WindSystem.windInfluence.y + randomnessY);
            data.color.a = ManageTransparency(data.color.a);

            if (TickSystem.HasTickChanged())
                ManageSpread(data.position, data.color.a);

            if (data.color.a <= 0)
            {
                instances.RemoveAt(i);
                i--;
                currentParticleCount--;
                continue;
            }

            instances[i] = data;

            matrices.Add(Matrix4x4.TRS(data.position, Quaternion.identity, Vector3.one * 0.035f));
        }

        if (matrices.Count > 0)
            Graphics.RenderMeshInstanced(rp, mesh, 0, matrices);
    }

    public void ManageRendering(InstanceData data)
    {
        matrices.Add(Matrix4x4.TRS(data.position, Quaternion.identity, Vector3.one * 0.035f));

        Graphics.RenderMeshInstanced(rp, mesh, 0, matrices);
    }

    public void ManageSpread(Vector2 pos, float alpha)
    {
        if (alpha > 0.15)
        {
            float randomXpos = pos.x + Random.Range(-randSpawnRange, randSpawnRange);
            float randomYpos = pos.y + Random.Range(-randSpawnRange, randSpawnRange);
            currAlpha = currAlpha - 0.01f;

            instances.Add(new InstanceData { position = new Vector2(randomXpos, randomYpos), color = new Color(1, 0, 1, currAlpha)});
        }
    }

    public void ManageDeletion(float alpha, int i)
    {
        if (alpha <= 0)
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
}
