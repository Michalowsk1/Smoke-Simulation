using UnityEngine;

public class WindSystem : MonoBehaviour
{
    public static Vector2 windInfluence;
    public Vector2 windDir;
    public float windIntensity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        windInfluence = UIBehaviour.windUIArrowDir * UIBehaviour.windIntensityScalar;
    }
}
