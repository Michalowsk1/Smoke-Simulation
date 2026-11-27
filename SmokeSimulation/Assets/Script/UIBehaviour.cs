using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject windArrowParent;
    [SerializeField] private GameObject windArrow;
    [SerializeField] private GameObject windArrowDirection;
    [SerializeField] private TextMeshProUGUI windDirectionText;
    [SerializeField] private TextMeshProUGUI windIntensityText;
    [SerializeField] private Slider directionSlider;
    [SerializeField] private Slider IntensitySlider;
    public static Vector2 windUIArrowDir;
    public static float windIntensityScalar;
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
        windArrowParent.transform.localEulerAngles = new Vector3(0,0, -directionSlider.value);
        windUIArrowDir = (windArrowDirection.transform.position - windArrow.transform.position).normalized;

        windIntensityScalar = IntensitySlider.value;

        windDirectionText.text = "(" + RoundToXPlace(2, windUIArrowDir.x) + ", " + RoundToXPlace(2, windUIArrowDir.y) + ")";
    }

    string RoundToXPlace(int decimalPlaces, float text)
    {
        string result = (Mathf.Round(text * Mathf.Pow(10, decimalPlaces)) / Mathf.Pow(10, decimalPlaces)).ToString();
        return result;
    }
}
