using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("References")]
    public SpawnPointManager spawnManager;

    [Header("UI Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Death Settings")]
    [SerializeField] private float deathDelay = 2f;
    [SerializeField] private float minImpactVelocity = 3f;
    [SerializeField] private float maxSafeAngle = 150f;

    private Vector2 startPosition;
    private int coinCount = 0;
    private bool isDead = false;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1f;
    }

    private void Start()
    {
        if (spawnManager != null && spawnManager.player != null)
        {
            startPosition = spawnManager.player.position;
        }

        UpdateCoinUI();
        UpdateDistanceUI();
    }

    private void Update()
    {
        if (!isDead) UpdateDistanceUI();
    }

    // ----------------- Coin -----------------
    public void AddCoin(int amount)
    {
        coinCount += amount;
        UpdateCoinUI();
    }

    private void UpdateCoinUI()
    {
        if (coinText) coinText.text = $"Coins: {coinCount}";
    }

    // ----------------- Distance -----------------
    private void UpdateDistanceUI()
    {
        if (!distanceText || spawnManager == null || spawnManager.player == null) return;

        float distance = Mathf.Max(0, Mathf.Abs(spawnManager.player.position.x - startPosition.x));
        distanceText.text = $"{distance:F0}m";
    }

    // ----------------- Death Check -----------------
    public void CheckHeadCollision(Collider2D col)
    {
        if (!isDead && col.CompareTag("Ground") && ShouldDie())
            StartCoroutine(DieAfterDelay());
    }

    private bool ShouldDie()
    {
        if (spawnManager == null || spawnManager.playerRb == null) return false;

        float speed = spawnManager.playerRb.linearVelocity.magnitude;

        float angle = Mathf.Abs(spawnManager.playerRb.rotation % 360f);
        if (angle > 180f) angle = 360f - angle;

        return speed > minImpactVelocity || angle > maxSafeAngle;
    }

    private IEnumerator DieAfterDelay()
    {
        isDead = true;
        yield return new WaitForSeconds(deathDelay);
        GameOver();
    }

    // ----------------- Game Control -----------------
    public void ShowPausePanel(bool show)
    {
        SoundManager.PlaySound("Button");
        if (pausePanel) pausePanel.SetActive(show);
        Time.timeScale = show ? 0 : 1;
    }

    public void GameOver()
    {
        if (gameOverPanel) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
