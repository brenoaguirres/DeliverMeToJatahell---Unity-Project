using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenPrefab;
    private string newGameLevelTag = "1-1";
    public float timeToWaitForClick = 0.5f;

    public IEnumerator PlayGame()
    {
        yield return new WaitForSeconds(timeToWaitForClick);
        GameObject loadingScreenInstance = Instantiate(loadingScreenPrefab);

        loadingScreenInstance.GetComponent<SceneLoader>().LoadScene(1);
    }

    public IEnumerator ExitGame()
    {
        yield return new WaitForSeconds(timeToWaitForClick);
        GameManager.QuitGame();
    }

    public void OnClickPlay()
    {
        GameObject loadingScreenInstance = Instantiate(loadingScreenPrefab);

        loadingScreenInstance.GetComponent<SceneLoader>().LoadScene(1);
        //StartCoroutine(PlayGame());
    }

    public void OnClickQuit()
    {
        StartCoroutine(ExitGame());
    }

}
