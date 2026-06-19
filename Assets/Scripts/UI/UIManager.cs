using UnityEngine;
using UnityEngine.UI;
using InfiniteRunner.Core;

namespace InfiniteRunner.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("UI Button References")]
        [Tooltip("Button to start the game.")]
        [SerializeField] private Button startButton;

        [Tooltip("Button to pause/stop the game.")]
        [SerializeField] private Button stopButton;

        [Tooltip("Button to restart the game.")]
        [SerializeField] private Button restartButton;

        private void Start()
        {
            // Register click listeners programmatically to prevent setup errors
            if (startButton != null)
                startButton.onClick.AddListener(OnStartClicked);

            if (stopButton != null)
                stopButton.onClick.AddListener(OnStopClicked);

            if (restartButton != null)
                restartButton.onClick.AddListener(OnRestartClicked);

            // Subscribe to GameManager state changes to update button visibility
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged += UpdateUI;
                UpdateUI(GameManager.Instance.CurrentState);
            }
            else
            {
                Debug.LogWarning("UIManager: GameManager instance not found. Make sure GameManager is present in the scene.");
            }
        }

        private void OnDestroy()
        {
            // Clean up listeners
            if (startButton != null)
                startButton.onClick.RemoveListener(OnStartClicked);

            if (stopButton != null)
                stopButton.onClick.RemoveListener(OnStopClicked);

            if (restartButton != null)
                restartButton.onClick.RemoveListener(OnRestartClicked);

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnStateChanged -= UpdateUI;
            }
        }

        /// <summary>
        /// Updates the visibility of the Start/Stop buttons based on the current game state.
        /// </summary>
        private void UpdateUI(GameState state)
        {
            if (state == GameState.Running)
            {
                if (startButton != null) startButton.gameObject.SetActive(false);
                if (stopButton != null) stopButton.gameObject.SetActive(true);
            }
            else // GameState.Stopped
            {
                if (startButton != null) startButton.gameObject.SetActive(true);
                if (stopButton != null) stopButton.gameObject.SetActive(false);
            }

            // Restart button is always visible
            if (restartButton != null) restartButton.gameObject.SetActive(true);
        }

        // Button action handlers
        private void OnStartClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StartGame();
            }
        }

        private void OnStopClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.StopGame();
            }
        }

        private void OnRestartClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RestartGame();
            }
        }
    }
}
