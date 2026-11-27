using UnityEngine;

public class WindSystem : MonoBehaviour
{
    public static Vector2 windInfluence;
    public Vector2 windDir;
    public float windIntensity;
    void Start()
    {
        windDir = new Vector2(1, 1);
        windIntensity = 0.0001f;
    }

    // Update is called once per frame
    void Update()
    {
        windInfluence = UIBehaviour.windUIArrowDir * windIntensity;
    }
}
