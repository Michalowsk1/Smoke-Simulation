using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public static bool hasTickChanged;

    public static int currentTick;
    public static int changedTick;

    public float timeForTickInSeconds;
    private float currentTime;

    void Start()
    {
        timeForTickInSeconds = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        HasTickChanged();
    }

    public float Timer()
    {
        currentTime = currentTime + Time.deltaTime;

        if(currentTime >= timeForTickInSeconds)
        {
            currentTick++;
            currentTime = 0;
        }
        return currentTick;
    }

    public void HasTickChanged()
    {
        if (changedTick == currentTick)
            hasTickChanged = false;

        else
        {
            changedTick = currentTick;
            hasTickChanged = true;
        }
    }
}
