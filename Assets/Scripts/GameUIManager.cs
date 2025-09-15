using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public static GameUIManager Instance;

    [Header("Panels")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;

    [Header("Fuel UI")]
    [SerializeField, Range(0.1f, 10f)] private float fuelDrainSpeed = 1f;
    [SerializeField] private Image fuelImage;
    [SerializeField] private Gradient fuelGradient;
    [SerializeField] private float maxFuelAmount = 100f;
    private float currentFuel;
    private Transform car;
    private float lastCarX;


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        currentFuel = maxFuelAmount;
        UpdateFuelUI();
        if (car != null)
            lastCarX = car.position.x;
    }

    private void Update()
    {
        HandleFuel();
    }
    public void SetCar(Transform carTransform)
    {
        car = carTransform;
        if (car != null)
            lastCarX = car.position.x;
    }


    private void HandleFuel()
    {

        if (car == null) {
            Debug.Log("LOI");
                return;
        }

        // Tính quãng đường di chuyển theo trục X
        float distanceMoved = Mathf.Abs(car.position.x - lastCarX);
        lastCarX = car.position.x;

        if (distanceMoved > 0.001f) // chỉ trừ khi có di chuyển
        {
            currentFuel -= distanceMoved * fuelDrainSpeed;
            currentFuel = Mathf.Clamp(currentFuel, 0f, maxFuelAmount);
            UpdateFuelUI();

        }

        if (currentFuel <= 0f)
        {
            if (GameManager.instance != null)
                GameManager.instance.GameOver();
        }
    }


    private void UpdateFuelUI()
    {
        if (fuelImage != null && fuelGradient != null)
        {
            fuelImage.fillAmount = currentFuel / maxFuelAmount;
            fuelImage.color = fuelGradient.Evaluate(fuelImage.fillAmount);
        }
    }

    public void FuelFull()
    {
        currentFuel = maxFuelAmount;
        UpdateFuelUI();
    }

    public void ShowPausePanel()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (gameplayPanel != null) gameplayPanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ShowWin()
    {
        winPanel.SetActive(true);
        gameplayPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        Time.timeScale = 0f; // dừng game
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMainMenu(int sceneIndex)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Dừng game trong Editor
#else
    Application.Quit(); // Thoát game khi build
#endif
    }

}
