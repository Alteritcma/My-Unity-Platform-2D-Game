using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject _LoaderCanvas;
    [SerializeField] private Slider _ProgressBar;
    public TextMeshProUGUI _ProgressText;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        _LoaderCanvas.SetActive(false);
    }

    public void QuitFromGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        var scene = SceneManager.GetActiveScene();
        LoadScene(scene.name);
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadASynchronously(sceneName));
    }

    IEnumerator LoadASynchronously(string sceneName)
    {
        yield return null;
        _LoaderCanvas.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _ProgressBar.value = progress;
            int progressInt = Mathf.RoundToInt(progress * 100f);
            Debug.Log(progressInt);
            _ProgressText.text = progressInt + "%";

            if (operation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                operation.allowSceneActivation = true;
                _LoaderCanvas.SetActive(false);
            }

            yield return null;
        }
    }
}
