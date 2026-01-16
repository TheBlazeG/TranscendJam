using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonMenuUI : MonoBehaviour
{
    private string nextScene;
    public void StartNewGame()
    {
        nextScene = "SampleScene";
        StartCoroutine(nameof(ChangeScene));
    }

    public void SettingsMenu()
    {
        nextScene = "Settings";
        StartCoroutine(nameof(ChangeScene));
    }

    public void CreditsMenu()
    {
        nextScene = "Credits";
        StartCoroutine(nameof(ChangeScene));
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
    public void BackMenu()
    {
        nextScene = "Menu";
        StartCoroutine(nameof(ChangeScene));
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(nextScene);
    }
}