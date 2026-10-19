using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadVibrationService : IVibrationService
{
    public void Vibrate(float lowFrequency, float highFrequency, float duration)
    {
        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(lowFrequency, highFrequency);

            // Usamos la instancia GameManager para ejecutar la corrutina que apaga la vibraciom
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartCoroutine(StopVibrationAfter(duration));
            }
        }
    }

    private IEnumerator StopVibrationAfter(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (Gamepad.current != null)
        {
            Gamepad.current.SetMotorSpeeds(0f, 0f);
        }
    }
}