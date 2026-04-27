using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManagerMainMenuScript : MonoBehaviour
{
    [SerializeField]
    private GameObject blackScreen;
    private Animator blackScreenAnimator;
    private void Start()
    {
        blackScreenAnimator = blackScreen.GetComponentInChildren<Animator>();
        StartCoroutine(DisableBlackScreen());
    }

    // Function called by buttons
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }
    public void QuitGame()
    {
        StartCoroutine(Quit());
    }

    // Scene Transition Coroutine
    private IEnumerator StartGameCoroutine()
    {
        FadeInBlackScreen();
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("LVL1");
    }
    private IEnumerator Quit()
    {
        FadeInBlackScreen();
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    // UI
    private void FadeInBlackScreen()
    {
        blackScreen.SetActive(true);
        blackScreenAnimator.Play("BlackScreenFadeIn");
    }
    private IEnumerator DisableBlackScreen()
    {
        yield return new WaitForSeconds(1f);
        blackScreen.SetActive(false);
    }
}
