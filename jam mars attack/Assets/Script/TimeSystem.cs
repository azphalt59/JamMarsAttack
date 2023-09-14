using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    public static TimeSystem Instance;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI timePhaseText;

    [Header("Time Stats")]
    [SerializeField] private float timeCount;
    [SerializeField] private float timeSpeedMultiplier = 5f;
    [SerializeField, Tooltip("Starting time in hour")] private float timeStart = 8f;

    [Header("Phases Time")]
    public PhaseTime phaseTime;
    public PhaseTime phaseTimeCopy;
    public enum PhaseTime
    { Launch, Work, Sleep, Free, Call };
    [SerializeField] private List<string> timePhasesName;
    private string currentTimePhase;

    [SerializeField, Tooltip("Time when morning call start")] private float morningCallingStart;
    [SerializeField, Tooltip("Time when morning call end")] private float morningCallingEnd;

    [SerializeField, Tooltip("Time when morning work start")] private float morningWorkingStart;
    [SerializeField, Tooltip("Time when morning work end")] private float morningWorkingEnd;

    [SerializeField, Tooltip("Time when midday eat start")] private float middayEatingStart;
    [SerializeField, Tooltip("Time when midday eat end")] private float middayEatingEnd;

    [SerializeField, Tooltip("Time when freetime start")] private float freetimeStart;
    [SerializeField, Tooltip("Time when freetime end")] private float freetimeEnd;

    [SerializeField, Tooltip("Time when afternoon working start")] private float afternoonWorkingStart;
    [SerializeField, Tooltip("Time when afternoon working end")] private float afternoonWorkingEnd;

    [SerializeField, Tooltip("Time when evening eat start")] private float eveningEatingStart;
    [SerializeField, Tooltip("Time when evening eat end")] private float eveningEatingEnd;

    [SerializeField, Tooltip("Time when night call start")] private float nightCallStart;
    [SerializeField, Tooltip("Time when night call end")] private float nightCallEnd;

    [SerializeField, Tooltip("Time when sleep start")] private float sleepingStart;
    [SerializeField, Tooltip("Time when sleep end")] private float sleepingEnd;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        timeCount += timeStart * 60; 
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime * timeSpeedMultiplier;

        float minutes = Mathf.FloorToInt(timeCount % 60);
        float hours = Mathf.FloorToInt(timeCount / 60);
        hours %= 24;
        
        timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
        timePhaseText.text = currentTimePhase + " Time";

        //sleeping
        if (hours%24 <= sleepingEnd - 1 || hours % 24 >= sleepingStart)
        {
            currentTimePhase = timePhasesName[2];
            phaseTime = PhaseTime.Sleep;
        }
        // eating
        if(hours % 24 <= middayEatingEnd - 1 && hours % 24 >= middayEatingStart || hours % 24 >= eveningEatingStart && hours % 24 <= eveningEatingEnd - 1)
        {
            currentTimePhase = timePhasesName[0];
            phaseTime = PhaseTime.Launch;
        }
        // calling
        if(hours % 24 >= morningCallingStart && hours%24 <= morningCallingEnd - 1 || hours % 24 >= nightCallStart && hours % 24 <= nightCallEnd - 1)
        {
            currentTimePhase = timePhasesName[4];
            phaseTime = PhaseTime.Call;
        }
        // free time
        if (hours % 24 >= freetimeStart && hours % 24 <= freetimeEnd - 1)
        {
            currentTimePhase = timePhasesName[3];
            phaseTime = PhaseTime.Free;
        }
        // working
        if (hours % 24 >= morningWorkingStart && hours % 24 <= morningWorkingEnd-1 || hours % 24 >= afternoonWorkingStart && hours % 24 <= afternoonWorkingEnd - 1)
        {
            currentTimePhase = timePhasesName[1];
            phaseTime = PhaseTime.Work;
        }
        
    }
}
