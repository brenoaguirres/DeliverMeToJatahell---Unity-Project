using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_Text progressText;

    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneAsync(sceneIndex));
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = $"Loading {progress * 100}%";

            yield return null;
        }
    }
}
