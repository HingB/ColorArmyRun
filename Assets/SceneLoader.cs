using Agava.YandexGames;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    [SerializeField] private CaseMaterial[] _caseMaterial;
    [SerializeField] private bool _notSaveLevel = true;
    [SerializeField] private int _startLevelIndex = 1;
    private int _circle = 0;
    public static bool IsInstallis { get; private set; } = false;
    private void Start()
    {
        if (IsInstallis == false)
        {
            Instance = this;
            IsInstallis = true;
            Object.DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

        OnStart();
    }

    private void OnStart()
    {
#if UNITY_EDITOR
        if (_notSaveLevel)
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.SetInt("CurrentLevel", _startLevelIndex);
        }
#endif

        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
        }
        else
        {
            NextScene();
        }
    }

    public void NextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        LoadScene(nextSceneIndex);
    }

    public void LoadScene(int indexScene)
    {
        if (SceneManager.sceneCountInBuildSettings - 1 < indexScene)
        {
            indexScene = 1;
            //Debug.LogAssertion("Нет сцены с таким индексом");
            //return;
        }

        SceneManager.LoadScene(indexScene);
        SaveLevel(indexScene);
    }

    public void SaveLevel(int indexScene)
    {
        PlayerPrefs.SetInt("CurrentLevel", indexScene);
    }

    public void SaveColor(CasleSide side, Color color)
    {
        for (int i = 0; i < _caseMaterial.Length; i++)
        {
            if (_caseMaterial[i].Color == color)
            {
                PlayerPrefs.SetInt(side.ToString(), i);
                return;
            }
        }

        Debug.LogAssertion("Ошибка цвет не найдет в списке цветов");
    }

    public bool TryTakeColor(CasleSide casleSide) => PlayerPrefs.HasKey(casleSide.ToString());
    public Color TakeColor(CasleSide casleSide) => _caseMaterial[PlayerPrefs.GetInt(casleSide.ToString())].Color;

    public void RemoveColor()
    {
        for (int i = 0; i < (int)CasleSide.Count; i++)
        {
            PlayerPrefs.DeleteKey(((CasleSide)i).ToString());
        }
    }
}

public enum CasleSide
{
    First,
    Second,
    Third,
    Fourth,
    Count,
}
