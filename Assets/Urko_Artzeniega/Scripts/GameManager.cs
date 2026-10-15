using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Objetivo")]
    [SerializeField] private int totalCoinsToWin = 3;
    private int currentCoins = 0;

    [Header("Referencias")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private GameObject exitDoor; // La puerta o zona final
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
    }

    private void Start()
    {
        UpdateUI();

        if (victoryPanel != null)
        {
            victoryPanel.SetActive(false);
        }
    }

    public void AddCoin()
    {
        currentCoins++;
        UpdateUI();

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
            // Ocultamos o desactivamos la puerta para permitir el paso
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