using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject windArrowParent;
    [SerializeField] private GameObject windArrow;
    [SerializeField] private GameObject windArrowDirection;
    [SerializeField] private Slider slider;
    public static Vector2 windUIArrowDir;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CalculateWindVariables();
    }

    void CalculateWindVariables()
    {
        windArrowParent.transform.localEulerAngles = new Vector3(0,0, slider.value);
        windUIArrowDir = (windArrowDirection.transform.position - windArrow.transform.position).normalized;
    }
}
