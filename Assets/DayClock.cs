using UnityEngine;
using UnityEngine.UI;

public class DayClock : MonoBehaviour
{

    Slider DayProgressions;
    // Update is called once per frame
    private void Start()
    {
        DayProgressions = GetComponent<Slider>();
    }
    void Update()
    {
        DayProgressions.value = DayCycleManager.Instance.dayTimer/DayCycleManager.Instance.dayDuration;
    }
}
