using UnityEngine;

public class ConsoleVibrationService : IVibrationService
{
    public void Vibrate(float lowFrequency, float highFrequency, float duration)
    {
        Debug.Log($"(VIBRACIÓN SIMULADA) Baja Frecuencia: {lowFrequency} | Alta Frecuencia: {highFrequency} | Duración: {duration}");
    }
}