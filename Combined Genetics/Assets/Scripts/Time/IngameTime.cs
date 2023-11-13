using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class IngameTime : MonoBehaviour
{
    [SerializeField] private float timeMultiplier;
    [Space]
    [SerializeField] private float startHour;
    [SerializeField] private TextMeshProUGUI timeText;

    [SerializeField] DateTime currentTime;

    [Space]
    [SerializeField] private Light sunLight;
    [SerializeField] private Light moonLight;
    [Space]
    [SerializeField] private float sunRiseHour;
    [SerializeField] private float sunSetHour;
    [Space]
    [SerializeField] private Color dayAmbientLight; 
    [SerializeField] private Color nightAmbientLight;
    [SerializeField] private AnimationCurve lightChangeCurve;
    [Space]
    [SerializeField] private float maxSunLight;
    [SerializeField] private float minSunLight;
    [Space]

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    void Start()
    {
        currentTime = DateTime.Now.Date + TimeSpan.FromHours(startHour);

        sunriseTime = TimeSpan.FromHours(sunRiseHour);
        sunsetTime = TimeSpan.FromHours(sunSetHour);
    }

    void Update()
    {
        UpdateLightSettings();
        UpdateTimeOfDay();
        RotateSun();
    }

    private void UpdateTimeOfDay()
    {
        currentTime = currentTime.AddSeconds(Time.deltaTime * timeMultiplier);
        if(timeText != null)
        {
            timeText.text = currentTime.ToString("HH:mm");
        }
    }

    private void UpdateLightSettings()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity = Mathf.Lerp(0, maxSunLight, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(minSunLight, 0, lightChangeCurve.Evaluate(dotProduct));

        //fog density and color
        RenderSettings.fogDensity = Mathf.Lerp(0.04f, 0.006f, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.fogColor = Color.Lerp(Color.black, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightAmbientLight, dayAmbientLight, lightChangeCurve.Evaluate(dotProduct));
    }

    private TimeSpan CalculateTimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan difference = toTime - fromTime;
        if(difference.TotalSeconds < 0)
        {
            difference += TimeSpan.FromHours(24);
        }
        return difference;
    }

    private void RotateSun()
    {
        float sunLightRotation;

        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            TimeSpan sunriseToSunsetDuration = CalculateTimeDifference(sunriseTime, sunsetTime);
            TimeSpan timeSinceSunRise = CalculateTimeDifference(sunriseTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunRise.TotalMinutes / sunriseToSunsetDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(0, 180, (float)percentage);
        }
        else
        {
            TimeSpan sunSetToRiseDuration = CalculateTimeDifference(sunsetTime, sunriseTime);
            TimeSpan timeSinceSunSet = CalculateTimeDifference(sunsetTime, currentTime.TimeOfDay);

            double percentage = timeSinceSunSet.TotalMinutes / sunSetToRiseDuration.TotalMinutes;

            sunLightRotation = Mathf.Lerp(180, 360, (float)percentage);
        }

        sunLight.transform.rotation = Quaternion.AngleAxis(sunLightRotation, Vector3.right);
    }
}
