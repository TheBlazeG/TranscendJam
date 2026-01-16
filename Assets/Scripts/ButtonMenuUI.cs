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
        nextScene = "Escena 1";
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

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(0.15f);
        SceneManager.LoadScene(nextScene);
    }
}