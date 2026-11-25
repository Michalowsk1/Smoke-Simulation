using UnityEngine;

public class TickSystem : MonoBehaviour
{
    public static int currentTick;
    public static int changedTick;

    public float timeForTickInSeconds;
    private float currentTime;

    void Start()
    {
        timeForTickInSeconds = 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
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

    public static bool HasTickChanged()
    {
        if (changedTick == currentTick)
            return false;

        else
        {
            changedTick = currentTick;
            return true;
        }
    }
}
