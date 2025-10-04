using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    string flyingGameScene = "SampleScene";
    string birdGameScene = "Challenge 3";
    string sumoGameScene = "Challenge 5";
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void StartFlyingGame()
    {
        SceneManager.LoadScene(flyingGameScene);
    }

    public void StartBirdGame() {
        SceneManager.LoadScene(birdGameScene);
    }

    public void StartSumoGame() {
        SceneManager.LoadScene(sumoGameScene);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
