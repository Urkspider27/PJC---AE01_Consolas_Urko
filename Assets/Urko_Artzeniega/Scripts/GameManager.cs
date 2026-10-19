using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Referencia al contrato del servicio (Desacoplado)
    public IVibrationService VibrationService { get; private set; }

    [Header("Objetivo")]
    [SerializeField] private int totalCoinsToWin = 3;
    private int currentCoins = 0;

    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI versionText;
    [SerializeField] private GameObject exitDoor;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Button restartButton;

    private void Awake()
    {
        if (Instance != null && Instance != UnityEngine.Object.FindAnyObjectByType<GameManager>())
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Si hay mando usa GamepadVibrationService, si no usa ConsoleVibrationService
        if (UnityEngine.InputSystem.Gamepad.current != null)
        {
            VibrationService = new GamepadVibrationService();
        }
        else
        {
            VibrationService = new ConsoleVibrationService();
        }
    }

    private void Start()
    {
        UpdateUI();

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }

        if (versionText != null)
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            versionText.text = "v1.0.0 - DEV BUILD";
#else
            versionText.text = "v1.0.0";
#endif
        }
    }

    public void AddCoin()
    {
        currentCoins++;
        UpdateUI();

        // Llamamos a la vibración al recoger moneda ( suave)
        VibrationService?.Vibrate(0.2f, 0.5f, 0.15f);

        if (currentCoins >= totalCoinsToWin)
        {
            OpenExit();
        }
    }

    private void UpdateUI()
    {
        if (coinText != null)
        {
            coinText.text = $"Monedas: {currentCoins} / {totalCoinsToWin}";
        }
    }

    private void OpenExit()
    {
        if (exitDoor != null)
        {
            // Desactiva la puerta para permitir el paso
            exitDoor.SetActive(false);
            Debug.Log("¡Todas las monedas recogidas! La puerta está abierta.");
        }
    }

    public void LevelCompleted()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }

        // Vibración más intensa al ganar
        VibrationService?.Vibrate(0.8f, 0.8f, 0.4f);

        // Selección automática del botón para control por Gamepad sin ratón
        if (EventSystem.current != null && restartButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(restartButton.gameObject);
        }
    }

    public void RestartLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}