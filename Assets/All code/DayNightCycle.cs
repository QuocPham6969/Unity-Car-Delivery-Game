using UnityEngine;
 // Light2D

public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    public UnityEngine.Rendering.Universal.Light2D globalLight;

    [Header("Day/Night Timing")]
    public float dayLengthInSeconds = 120f; // full cycle = 2 minutes
    [Range(0f, 1f)]
    public float timeOfDay = 0f;

    [Header("Lighting Control")]
    public Gradient lightColor;
    public AnimationCurve lightIntensity;

    void Update()
    {
        if (globalLight == null) return;

        // Progress time (loop 0 → 1)
        timeOfDay += Time.deltaTime / dayLengthInSeconds;
        if (timeOfDay > 1f) timeOfDay -= 1f;

        // Apply color & brightness from gradient/curve
        globalLight.color = lightColor.Evaluate(timeOfDay);
        globalLight.intensity = lightIntensity.Evaluate(timeOfDay);
    }
}
