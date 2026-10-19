using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleEventsManager : MonoBehaviour
{
    [Header("UI de Desconexión")]
    [SerializeField] private GameObject disconnectPanel;

    private bool isPausedByDevice = false;
    private bool isInitialized = false;

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void Start()
    {
        if (disconnectPanel != null)
        {
            disconnectPanel.SetActive(false);
        }

        // Asegurar que el tiempo empiece normal al arrancar
        Time.timeScale = 1f;

        // Marcamos que el juego ya ha iniciado correctamente
        Invoke(nameof(EnableFocusCheck), 0.2f);
    }

    private void EnableFocusCheck()
    {
        isInitialized = true;
    }

    // Gestión de Pérdida de Foco
    private void OnApplicationFocus(bool hasFocus)
    {
        // Ignoramos el cambio de foco si el juego acaba de arrancar
        if (!isInitialized) return;

        if (!hasFocus)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    // Gestión de Desconexión/Reconexión de Mando
    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad)
        {
            if (change == InputDeviceChange.Disconnected)
            {
                ShowDisconnectMenu(true);
            }
            else if (change == InputDeviceChange.Reconnected)
            {
                ShowDisconnectMenu(false);
            }
        }
    }

    private void ShowDisconnectMenu(bool disconnected)
    {
        isPausedByDevice = disconnected;

        if (disconnectPanel != null)
        {
            disconnectPanel.SetActive(disconnected);
        }

        Time.timeScale = disconnected ? 0f : 1f;
    }

    private void PauseGame()
    {
        if (!isPausedByDevice)
        {
            Time.timeScale = 0f;
        }
    }

    private void ResumeGame()
    {
        // Reanudamos el tiempo si no estamos pausados por desconexión de mando
        if (!isPausedByDevice)
        {
            Time.timeScale = 1f;
        }
    }
}