using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Menu : MonoBehaviour
{
    [SerializeField] private bool _isBonusLevel;

    private SceneLoader _sceneLoader;
    private ChekPaint _chekPaint;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _chekPaint = FindObjectOfType<ChekPaint>();
#if VK_GAMES
        IntergrationMetric.Instance.OnLevelStart(SceneManager.GetActiveScene().buildIndex);
#endif
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        Time.timeScale = 1;
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
#if VK_GAMES
        IntergrationMetric.Instance.OnRestartLevel(SceneManager.GetActiveScene().buildIndex);
#endif
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        if (_isBonusLevel)
        {
            _sceneLoader.RemoveColor();
        }

        _sceneLoader.NextScene();
    }
}
