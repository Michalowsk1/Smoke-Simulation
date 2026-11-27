using UnityEngine;

public class WindSystem : MonoBehaviour
{
    public static Vector2 windInfluence;
    public Vector2 windDir;
    public float windIntensity;
    void Start()
    {
        windDir = new Vector2(1, 1);
        windIntensity = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        windInfluence = windDir.normalized * windIntensity;
    }

    public static Vector2 GetWindPower()
    {
        return windInfluence;
    }
}
